// Copyright (c) FireGiant.  All Rights Reserved.

using System;
using System.Web;
using System.Web.Configuration;
using ErrorCode.Api;
using ErrorCode.Api.Services;
using TinyWebStack;

[assembly: PreApplicationStartMethod(typeof(Application), "Start")]

namespace ErrorCode.Api
{
    public static class Application
    {
        public static void Start()
        {
            Container.Current.Register<IMailService>(c => new MailService
            {
                Server = WebConfigurationManager.AppSettings["mail.server"],
                Port = WebConfigurationManager.AppSettings["mail.port"] != null ? Int32.Parse(WebConfigurationManager.AppSettings["mail.port"]) : 587,
                Username = WebConfigurationManager.AppSettings["mail.username"],
                Password = WebConfigurationManager.AppSettings["mail.password"],
                RequireSsl = WebConfigurationManager.AppSettings["mail.ssl"] != null ? Boolean.Parse(WebConfigurationManager.AppSettings["mail.ssl"]) : false
            }, Lifetime.Application);

            Container.Current.Register<ISearchService>(c => new SearchService(c.Resolve<IServerUtility>(), "~/api/search.json"), Lifetime.Application);

            Routing.RegisterRoutes();

            ContentHandling.ConsiderWildCardContentTypeAs = "application/json";

            ContentHandling.RegisterContentTypes();
        }
    }
}