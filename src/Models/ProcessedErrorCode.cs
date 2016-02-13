// Copyright (c) FireGiant.  All Rights Reserved.

namespace ErrorCode.Models
{
    public class ProcessedErrorCode
    {
        public string Area { get; set; }

        public string AreaUrl { get; set; }

        public string Source { get; set; }

        public string SourceUrl { get; set; }

        public string Code { get; set; }

        public string CodeUrl { get; set; }

        public int NumericCode { get; set; }

        public string Define { get; set; }

        public string DefineUrl { get; set; }

        public string Message { get; set; }

        public string Remarks { get; set; }

        public string[] Aka { get; set; }

        public string[] AkaUrl { get; set; }

        public string OriginalCode { get; set; }

        public string OriginalCodeUrl { get; set; }
    }
}