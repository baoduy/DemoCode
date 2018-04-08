using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using HBD.DataContacts;
using HBD.Sb.Core;
using HBD.Sb.Interface;

namespace Stateless
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Stateless : RemoteStatelessService, IHbdService
    {
        public Stateless(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        //protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners() => new[] { new ServiceInstanceListener(this.CreateServiceRemotingListener) };

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            ServiceEventSource.Current.ServiceMessage(this.Context, "RunAsync");
        }

        public Task<IEnumerable<HbdModel>> GetAsync() 
            => Task.FromResult((IEnumerable<HbdModel>)new List<HbdModel>{ new HbdModel { Id = 1, Name = "HBD" }});
    }
}
