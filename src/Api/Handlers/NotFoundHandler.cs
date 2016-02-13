// Copyright (c) FireGiant.  All Rights Reserved.

using System;
using TinyWebStack;
using TinyWebStack.Models;

namespace ErrorCode.Api.Handlers
{
    [Route("customerrors/404")]
    public class NotFoundHandler
    {
        //private static Logger Log = LogManager.GetLogger("notfound");

        public string _RawQueryString { private get; set; }

        public FileTransmission Output { get; private set; }

        public IRequest Request { private get; set; }

        public Status Get()
        {
            // Usually the error information is passed via the query string so
            // try to process that raw value.
            //
            string[] errorAndUrl = null;
            if (!String.IsNullOrEmpty(this._RawQueryString))
            {
                errorAndUrl = this._RawQueryString.Split(new[] { ';' }, 2);
            }

            // If the query string was not provided nor formatted properly try to use the
            // referrer.
            //
            if (errorAndUrl == null || errorAndUrl.Length != 2)
            {
                errorAndUrl = new[] { "404", this.Request.Referrer == null ? "Unknown" : this.Request.Referrer.AbsoluteUri };
            }

            //Log.Info("url: {0}  referrer: {1}", this.Request.Url.AbsoluteUri, errorAndUrl[1]);

            var errorPage = String.Format("/customerrors/page/{0}/index.html", errorAndUrl[0]);

            this.Output = new FileTransmission("text/html", errorPage);

            return Status.NotFound;
        }
    }
}