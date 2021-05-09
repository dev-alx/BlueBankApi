using Contact.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Enitties
{
    public class AccountMovement
    {
        public int AccountMovementId { get; set; }
        public MovementType MovementType { get; set; }
        public decimal Amount { get; set; }
        public DateTime RecorDate { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
