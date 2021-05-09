using Contact.Core.Enitties;
using Contact.Core.Interfaces;
using Contact.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Repository
{
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(ContactDbContext context): base(context)
        {

        }

        public async Task<BankAccount> GetBankAccountByAccountNumber(Guid accountNumber)
        {
            return await entities.FirstOrDefaultAsync(c => c.Product == accountNumber);
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountByClientId(int clientId)
        {
            return await entities.Where(c => c.ClientId == clientId).ToListAsync();
        }
    }
}