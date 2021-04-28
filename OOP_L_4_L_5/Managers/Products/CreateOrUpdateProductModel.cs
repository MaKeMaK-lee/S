using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Products
{
    public class CreateOrUpdateProductModel
    {
        public Product Product { get; set; }
        public IEnumerable<Anime> Animes { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Figure> Figures { get; set; }
        public CreateOrUpdateProductModel(Product _Product, IEnumerable<Anime> _Animes, IEnumerable<Book> _Books, IEnumerable<Figure> _Figures)
        {
            Product = _Product;
            Animes = _Animes;
            Books = _Books;
            Figures = _Figures;
        }
    }
}
