using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Clients
{
    public class CreateOrUpdateClientRequest
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        //public string[] PaymentInfos { get; set; }
    }
}
