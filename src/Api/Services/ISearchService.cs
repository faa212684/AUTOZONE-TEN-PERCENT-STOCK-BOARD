// Copyright (c) FireGiant.  All Rights Reserved.

using System.Collections.Generic;
using ErrorCode.Models;

namespace ErrorCode.Api.Services
{
    public interface ISearchService
    {
        IEnumerable<SearchDatum> Search(string query);
    }
}