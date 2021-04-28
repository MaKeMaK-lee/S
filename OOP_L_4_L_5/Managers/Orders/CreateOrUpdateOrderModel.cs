using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Orders
{
    public class CreateOrUpdateOrderModel
    {
        public Order Order { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public CreateOrUpdateOrderModel(Order _Order, IEnumerable<Product> _Products, IEnumerable<Client> _Clients)
        {
            Order = _Order;
            Products = _Products;
            Clients = _Clients;
        }
    }
}
