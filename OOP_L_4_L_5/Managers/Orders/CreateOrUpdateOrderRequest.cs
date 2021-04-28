using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Orders
{
    public class CreateOrUpdateOrderRequest
    {
        public Guid[] ProductIds { get; set; }
        public Guid ClientId { get; set; }
    }
}
