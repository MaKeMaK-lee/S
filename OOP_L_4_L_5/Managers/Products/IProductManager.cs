using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Products
{
    public interface IProductManager
    {
        Task<Product> AddProduct(CreateOrUpdateProductRequest request);
        Task<IReadOnlyCollection<Product>> GetAll();
        Task<IReadOnlyCollection<Product>> SearchInAll(string SearchMinCountInStock, string[] SearchProductType, string SearchMinPrice, string SearchMaxPrice);
        Task<Product> UpdateProduct(Guid id, CreateOrUpdateProductRequest request);
        Task<Product> GetById(Guid _Id);
        Task<Product> DeleteById(Guid _Id);
        Task<Anime> GetAnimeById(Guid _Id);
        Task<Book> GetBookById(Guid _Id);
        Task<Figure> GetFigureById(Guid _Id);
        Task<IReadOnlyCollection<Anime>> GetAllAnimes();
        Task<IReadOnlyCollection<Book>> GetAllBooks();
        Task<IReadOnlyCollection<Figure>> GetAllFigures();
    }
}
