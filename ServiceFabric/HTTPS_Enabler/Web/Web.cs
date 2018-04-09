using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Web
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class Web : StatelessService
    {
        public Web(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                ////Http
                //new ServiceInstanceListener(serviceContext =>
                //    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                //    {
                //        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                //        return new WebHostBuilder()
                //                    .UseKestrel()
                //                    .ConfigureServices(
                //                        services => services
                //                            .AddSingleton<StatelessServiceContext>(serviceContext))
                //                    .UseContentRoot(Directory.GetCurrentDirectory())
                //                    .UseStartup<Startup>()
                //                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseReverseProxyIntegration)
                //                    .UseUrls(url)
                //                    .Build();
                //    }),"ServiceEndpoint"),

                //Https
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpointHttps", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        var cert = GetCertificateFromStore(serviceContext);
                        var port =int.Parse( url.Substring(url.IndexOf("+:") + 2));

                        return new WebHostBuilder()
                            .UseKestrel(op=>{
                                //Get current Ip of PC
                                var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(i=>i.AddressFamily == AddressFamily.InterNetwork)??IPAddress.Loopback;
                                op.Listen(ip, port, listenConfig=>listenConfig.UseHttps(cert));
                                })
                            .ConfigureServices(
                                services => services
                                    .AddSingleton(serviceContext))
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseStartup<Startup>()
                            .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseReverseProxyIntegration)
                            .UseUrls(url)
                            .Build();
                    }),"ServiceEndpointHttps")
            };
        }


        private X509Certificate2 GetCertificateFromStore(StatelessServiceContext serviceContext)
        {
            var config = serviceContext.CodePackageActivationContext.GetConfigurationPackageObject("Config");

            //Load Certificate from Store by Thumbprint
            //Uncomment this if you want to load cert from Computer store.
            //var thumbprint = config.Settings.Sections["MyConfigSection"].Parameters["CertThumbprint"].Value;

            //var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //try
            //{
            //    store.Open(OpenFlags.ReadOnly);
            //    var certCollection = store.Certificates;
            //    var currentCerts = certCollection.Find(X509FindType.FindByThumbprint, thumbprint, false);
            //    return currentCerts.Count == 0 ? null : currentCerts[0];
            //}
            //finally
            //{
            //    store.Close();
            //}

            //Load Certificate from file
            return new X509Certificate2(Path.Combine(config.Path, "localhost.pfx"), "localhost");
        }
    }
}
