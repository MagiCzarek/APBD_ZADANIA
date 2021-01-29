using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw3.Middleware
{
    public class Logging
    {
        private readonly RequestDelegate requestDelegate;

        public Logging(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Middleware");

            if (context.Request != null)
            {
                string path = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request.QueryString.ToString();
                string body = "";

                using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                    
                }
                File.AppendAllText("requestsLog.txt", "requestsLog: " + "\n" + path + "\n" + method + "\n" + queryString + "\n" + body);
            }

            if (requestDelegate != null)
            {
                await requestDelegate(context);
            }
        }
    }
}
