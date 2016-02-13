// Copyright (c) FireGiant.  All Rights Reserved.

namespace ErrorCode.Models
{
    public class ErrorCode
    {
        public string Area { get; set; }

        public string Source { get; set; }

        public string Code { get; set; }

        public int NumericCode { get; set; }

        public string Define { get; set; }

        public string Message { get; set; }

        public string[] Aka { get; set; }

        public string Remarks { get; set; }
    }
}