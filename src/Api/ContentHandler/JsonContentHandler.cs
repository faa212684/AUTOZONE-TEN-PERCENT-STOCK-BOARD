// Copyright (c) FireGiant.  All Rights Reserved.

using System;
using TinyWebStack;

namespace ErrorCode.Api.ContentHandler
{
    [ContentType("application/javascript")]
    [ContentType("application/x-javascript")]
    [ContentType("application/json")]
    [ContentType("text/javascript")]
    [ContentType("text/x-javascript")]
    [ContentType("text/json")]
    //[ContentType("text/html")] // TODO: we really shouldn't be taking over HTML here but it is helpful for debugging.
    internal class JsonContentHandler : IContentTypeHandler
    {
        public bool TryGetWriter(string contentType, Type handlerType, Type dataType, out IContentTypeWriter writer)
        {
            contentType = (contentType.Contains("json") || contentType.Contains("javascript")) ? contentType : "application/json";

            writer = new JsonContentWriter(contentType);

            return true;
        }
    }
}