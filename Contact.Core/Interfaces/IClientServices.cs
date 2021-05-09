using Contact.Core.Dto;
using Contact.Core.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces
{
    public interface IClientServices
    {      
        Task<Client> InsertClient(Client client);
        Task<IEnumerable<ClientResponseDto>> GetAll();
        Task<Client> GetById(int id);
    }
}
