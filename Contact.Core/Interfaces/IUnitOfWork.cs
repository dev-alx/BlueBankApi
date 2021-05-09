using Contact.Core.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository ClientRepository { get; }
        IBankAccountRepository BankAccountRepository { get; }
        IAccountMovementRepository AccountMovementRepository { get; }
        Task SaveChangesAsync();
    }
}
