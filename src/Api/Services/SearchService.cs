// Copyright (c) FireGiant.  All Rights Reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ErrorCode.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TinyWebStack;

namespace ErrorCode.Api.Services
{
    public class SearchService : ISearchService
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() };

        private static readonly Regex RemoveInsignificantDigitsRegex = new Regex(@"^(-|0[xX])?(?<remove>0+)\d+", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Singleline);

        private readonly object LoadLock = new object();

        public SearchService(IServerUtility utility, string searchDataPath)
        {
            this.LoadData = LoadJsonDataFromDisk;

            this.SearchDataPath = searchDataPath;

            this.ServerUtility = utility;
        }

        public Func<string, IEnumerable<SearchDatum>> LoadData { private get; set; }

        private string SearchDataPath { get; }

        private IServerUtility ServerUtility { get; }

        private ISet<string> SearchAreas { get; set; }

        private ILookup<string, SearchDatum> SearchData { get; set; }

        public IEnumerable<SearchDatum> Search(string query)
        {
            if (this.SearchData == null)
            {
                lock (LoadLock)
                {
                    if (this.SearchData == null)
                    {
                        var searchDataPath = this.ServerUtility != null ? this.ServerUtility.MapPath(this.SearchDataPath) : this.SearchDataPath;

                        var data = this.LoadData(searchDataPath);

                        this.SearchAreas = new HashSet<string>(data.Select(d => String.IsNullOrEmpty(d.Area) ? String.Empty : d.Area), StringComparer.OrdinalIgnoreCase);

                        this.SearchData = data.ToLookup(d => RemoveInsignificantDigitsRegex.Replace(d.Code, RemoveInsignificantDigits), StringComparer.OrdinalIgnoreCase);
                    }
                }
            }

            var areas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var code = String.Empty;

            var tokens = query.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                if (this.SearchAreas.Contains(token))
                {
                    areas.Add(token);
                }
                else
                {
                    code = RemoveInsignificantDigitsRegex.Replace(token.ToUpperInvariant(), RemoveInsignificantDigits);
                }
            }

            var results = this.SearchData[code];

            if (areas.Any())
            {
                results = results.Where(c => areas.Contains(c.Area));
            }

            return results;
        }

        private static string RemoveInsignificantDigits(Match m)
        {
            var group = m.Groups[1];
            var end = group.Index + group.Length;

            var beginning = (group.Index == 0) ? String.Empty : m.Value.Substring(0, group.Index);
            var ending = (m.Value.Length > end) ? m.Value.Substring(end) : String.Empty;

            return beginning + ending;
        }

        private static IEnumerable<SearchDatum> LoadJsonDataFromDisk(string searchDataPath)
        {
            var json = File.ReadAllText(searchDataPath);

            return JsonConvert.DeserializeObject<SearchDatum[]>(json, JsonSettings);
        }
    }
}