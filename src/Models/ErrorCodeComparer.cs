// Copyright (c) FireGiant.  All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ErrorCode.Models
{
    public class ErrorCodeComparer : IComparer<ErrorCode>
    {
        public int Compare(ErrorCode errorCodeX, ErrorCode errorCodeY)
        {
            var x = errorCodeX.Code;
            var y = errorCodeY.Code;

            if (x.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                if (y.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    return x.CompareTo(y);
                }

                return 1;
            }
            if (y.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                return -1;
            }
            uint xi = 0;
            uint yi = 0;

            if (!UInt32.TryParse(x, out xi))
            {
                xi = (uint)errorCodeX.NumericCode;
            }

            if (!UInt32.TryParse(y, out yi))
            {
                yi = (uint)errorCodeY.NumericCode;
            }

            return xi.CompareTo(yi);
        }
    }
}