using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Orders
{
    public interface IOrderManager
    {
        Task<Order> AddOrder(CreateOrUpdateOrderRequest request);
        Task<IReadOnlyCollection<Order>> GetAll();
        Task<Order> UpdateOrder(Guid id, CreateOrUpdateOrderRequest request);
        Task<Order> GetById(Guid _Id);
        Task<Order> DeleteById(Guid _Id);
        Task<IReadOnlyCollection<Client>> GetAllClients();
        Task<IReadOnlyCollection<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid _Id);
        Task<Anime> GetAnimeById(Guid _Id);
        Task<Book> GetBookById(Guid _Id);
        Task<Figure> GetFigureById(Guid _Id);
        Task<ICollection<PaymentInfo>> GetPaymentInfosByClientId(Guid id);
        Task<Order> OrderNextStatusById(Guid id);
        Task<Client> GetClientById(Guid _Id);
        Task<ICollection<OrderProduct>> GetOrderProductsByOrderId(Guid _Id);
    }
}
