// Copyright (c) FireGiant.  All Rights Reserved.

namespace ErrorCode.Models
{
    public class GroupedErrorCode
    {
        public string Area { get; set; }

        public string AreaUrl { get; set; }

        public string Source { get; set; }

        public string SourceUrl { get; set; }

        public string[] Items { get; set; }

        public string[] ItemUrls { get; set; }
    }
}