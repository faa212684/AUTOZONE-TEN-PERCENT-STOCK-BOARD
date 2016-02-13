// Copyright (c) FireGiant.  All Rights Reserved.

using System.Collections.Generic;
using ErrorCode.Api.Services;
using ErrorCode.Models;
using TinyWebStack;

namespace ErrorCode.Api.Handlers
{
    [Route("api/search")]
    public class SearchHandler
    {
        public SearchHandler(ISearchService search)
        {
            this.SearchService = search;
        }

        public string Q { private get; set; }

        public IEnumerable<SearchDatum> Output { get; private set; }

        private ISearchService SearchService { get; }

        public Status Get()
        {
            this.Output = this.SearchService.Search(this.Q);

            return Status.OK;
        }
    }
}