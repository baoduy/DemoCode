using System.Collections.Generic;
using System.Threading.Tasks;
using HBD.DataContacts;
using Microsoft.ServiceFabric.Services.Remoting;

namespace HBD.Sb.Interface
{
    public interface IHbdService : IService
    {
        Task<IEnumerable<HbdModel>> GetAsync();
    }
}
