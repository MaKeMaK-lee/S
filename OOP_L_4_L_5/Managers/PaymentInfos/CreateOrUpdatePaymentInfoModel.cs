using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.PaymentInfos
{
    public class CreateOrUpdatePaymentInfoModel
    {
        public PaymentInfo PaymentInfo { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public CreateOrUpdatePaymentInfoModel(PaymentInfo _PaymentInfo, IEnumerable<Client> _Clients) 
        {
            PaymentInfo = _PaymentInfo;
            Clients = _Clients;
        }
    }
}
