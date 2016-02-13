// Copyright (c) FireGiant.  All Rights Reserved.

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TinyWebStack;

namespace ErrorCode.Api.ContentHandler
{
    public class JsonContentWriter : IContentTypeWriter
    {
        private static readonly JsonSerializerSettings JsonIgnoreNullSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.None };

        public JsonContentWriter(string contentType)
        {
            this.ContentType = contentType;
        }

        public string ContentType { get; }

        public void Write(TextWriter writer, object data)
        {
            var json = JsonConvert.SerializeObject(data, JsonIgnoreNullSettings);

            writer.Write(json);
        }
    }
}