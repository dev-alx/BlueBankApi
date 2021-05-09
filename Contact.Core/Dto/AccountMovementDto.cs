using Contact.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Dto
{
    public class AccountMovementDto
    {       
        public Guid Account { get; set; }
        public decimal Amount { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
