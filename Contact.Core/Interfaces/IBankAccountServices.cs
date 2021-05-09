using Contact.Core.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces
{
    public interface IBankAccountServices
    {        
        Task<BankAccount> InsertBankAccount(BankAccount bankAccount);
        Task<BankAccount> GetBankAccount(Guid accountId);
        Task<IEnumerable<BankAccount>> GetBankAccountByClientId(int clientId);
        Task<BankAccount> GetBankAccountByAccountNumber(Guid accountNumber);
    }
}
