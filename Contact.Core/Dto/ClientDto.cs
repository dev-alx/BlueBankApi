using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Dto
{
    public class ClientDto
    {        
        public string FullName { get; set; }
        public string SocialNumber { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
