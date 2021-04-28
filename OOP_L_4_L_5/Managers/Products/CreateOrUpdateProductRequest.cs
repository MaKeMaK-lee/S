using OOP_L_4_L_5.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Products
{
    public class CreateOrUpdateProductRequest
    {
        public Guid MatterId { get; set; }
        public int Price { get; set; }
        public int CountInStock { get; set; }
    }
}
