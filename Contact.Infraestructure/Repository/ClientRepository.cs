using Contact.Core.Enitties;
using Contact.Core.Interfaces;
using Contact.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Repository
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ContactDbContext context) : base(context)
        {

        }
        public async Task<Client> GetClientBySocialNumber(string socialNumber)
        {
            return await entities.FirstOrDefaultAsync(c => c.SocialNumber == socialNumber);
        }
    }
}
