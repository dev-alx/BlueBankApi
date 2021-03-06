using Contact.Core.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetClientBySocialNumber(string socialNumber);
    }
}
