using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Enitties
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public string Description { get; set; }
        public Guid Product { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime RecordDate { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}
