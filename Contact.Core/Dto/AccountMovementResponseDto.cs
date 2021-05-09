using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Dto
{
    public class AccountMovementResponseDto
    {
        public string FullName { get; set; }
        public Guid Product { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<AccountMovementDto> accountMovement { get; set; }

        public AccountMovementResponseDto()
        {
            accountMovement = new List<AccountMovementDto>();
        }
    }
}
