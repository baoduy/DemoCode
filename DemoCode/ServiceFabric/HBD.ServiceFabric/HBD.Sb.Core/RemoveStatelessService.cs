using System.Collections.Generic;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace HBD.Sb.Core
{
    public abstract class RemoteStatelessService : StatelessService, IService
    {
        protected RemoteStatelessService(StatelessServiceContext serviceContext) : base(serviceContext)
        {
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
            => new[] { new ServiceInstanceListener(this.CreateServiceRemotingListener) };
    }
}
