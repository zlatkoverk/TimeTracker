using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared.ApiMessages
{
    public class Response
    {
        public bool Error { get; set; }
        public string Message { get; set; }

        public Response(string message, bool error)
        {
            Message = message;
            Error = error;
        }

        public Response()
        {
        }
    }
}
