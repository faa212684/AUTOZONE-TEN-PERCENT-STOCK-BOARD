// Copyright (c) FireGiant.  All Rights Reserved.

namespace ErrorCode.Api.Services
{
    public interface IMailService
    {
        string Server { get; set; }

        int Port { get; set; }

        string Username { get; set; }

        string Password { get; set; }

        bool RequireSsl { get; set; }

        void Mail(string from, string to, string subject, string message);
    }
}