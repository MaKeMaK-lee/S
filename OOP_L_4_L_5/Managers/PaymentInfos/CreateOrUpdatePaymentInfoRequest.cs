using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.PaymentInfos
{
    public class CreateOrUpdatePaymentInfoRequest
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public Guid ClientId { get; set; }
    }
}
