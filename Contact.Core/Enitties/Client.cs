using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Enitties
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string SocialNumber { get; set; }              
        public DateTime RecordDate { get; set; }
    }
}
