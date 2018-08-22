﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace zoro.one.httpserver
{


    public class httpserver
    {
        public httpserver()
        {
            onHttpEvents = new System.Collections.Concurrent.ConcurrentDictionary<string, IController>();
        }
        private IWebHost host;
        public System.Collections.Concurrent.ConcurrentDictionary<string, IController> onHttpEvents;
        onProcessHttp onHttp404;



        public void Start(int port, int portForHttps = 0, string pfxpath = null, string password = null)
        {
            host = new WebHostBuilder().UseKestrel((options) =>
            {
                options.Listen(IPAddress.Any, port, listenOptions =>
                  {

                  });
                if (portForHttps != 0)
                {
                    options.Listen(IPAddress.Any, portForHttps, listenOptions =>
                      {
                          //if (!string.IsNullOrEmpty(sslCert))
                          //if (useHttps)
                          listenOptions.UseHttps(pfxpath, password);
                          //sslCert, password);
                      });
                }
            }
            )
            .Configure(app =>
            {
                //app.UseResponseCompression();
                app.Run(ProcessAsync);
            })
            //.ConfigureServices(services =>
            //{
            //    services.AddResponseCompression(options =>
            //    {
            //        // options.EnableForHttps = false;
            //        options.Providers.Add<GzipCompressionProvider>();
            //        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json-rpc" });
            //    });

            //    services.Configure<GzipCompressionProviderOptions>(options =>
            //    {
            //        options.Level = CompressionLevel.Fastest;
            //    });
            //})
            .Build();

            host.Start();
        }

        public void AddJsonRPC(string path, string method, JSONRPCController.ActionRPC action)
        {
            if (onHttpEvents.ContainsKey(path) == false)
            {
                onHttpEvents[path] = new JSONRPCController();
            }
            var jsonc = onHttpEvents[path] as JSONRPCController;
            jsonc.AddAction(method, action);
        }
        public void SetJsonRPCFail(string path, JSONRPCController.ActionRPCFail action)
        {
            if (onHttpEvents.ContainsKey(path) == false)
            {
                onHttpEvents[path] = new JSONRPCController();
            }
            var jsonc = onHttpEvents[path] as JSONRPCController;
            jsonc.SetFailAction(action);
        }
        public void SetHttpAction(string path, onProcessHttp httpaction)
        {
            onHttpEvents[path] = new ActionController(httpaction);
        }
        public void SetHttpController(string path, IController controller)
        {
            onHttpEvents[path] = controller;
        }
        public void SetFailAction(onProcessHttp httpaction)
        {
            onHttp404 = httpaction;
        }
        public delegate Task onProcessHttp(HttpContext context);


        private async Task ProcessAsync(HttpContext context)
        {
            try
            {
                var path = context.Request.Path.Value;
                if (onHttpEvents.TryGetValue(path.ToLower(), out IController controller))
                {
                    await controller.ProcessAsync(context);
                }
                else
                {
                    await onHttp404(context);
                }
            }
            catch
            {

            }
        }
    }
}
