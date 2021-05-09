﻿using Contact.Core.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        Task<BankAccount> GetBankAccountByAccountNumber(Guid accountNumber);
        Task<IEnumerable<BankAccount>> GetBankAccountByClientId(int clientId);
    }
}
