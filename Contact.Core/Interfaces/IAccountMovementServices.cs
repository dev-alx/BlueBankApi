using Contact.Core.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces
{
    public interface IAccountMovementServices
    {
        Task<AccountMovement> InsertAccountMovement(AccountMovement accountMovement);

        Task<IEnumerable<AccountMovement>> GetMovements(Guid accountNumber);
        Task<decimal> GetTotalAmount(Guid accountNumber);
    }
}
