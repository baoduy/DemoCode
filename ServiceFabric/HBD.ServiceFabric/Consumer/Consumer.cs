using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using HBD.Sb.Interface;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Consumer
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Consumer : StatelessService
    {
        public Consumer(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            var sv = ServiceProxy.Create<IHbdService>(new Uri("fabric:/HBD.ServiceFabric/Stateless"));

            //while (true)
            //{
            //    cancellationToken.ThrowIfCancellationRequested();

            //    var s = (await sv.GetAsync()).FirstOrDefault();

            //    if (s == null)
            //    {
            //        ServiceEventSource.Current.ServiceMessage(this.Context, "Cannot Get items");
            //    }
            //    else ServiceEventSource.Current.ServiceMessage(this.Context, "{0} {1} {2}", s.GetType().Name, s.Id, s.Name);

            //    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            //}
        }
    }
}
