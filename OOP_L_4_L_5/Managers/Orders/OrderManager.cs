using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Orders
{
    public class OrderManager : IOrderManager
    {
        private readonly RestShopDataContext _dbContext;
        public OrderManager(RestShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> AddOrder(CreateOrUpdateOrderRequest request)
        {
            var entity = new Order
            {
                Id = Guid.NewGuid(),
                DateOfCompletion = DateTime.Now,
                OrderStatus = OrderStatuses.Basket,
                ClientId = request.ClientId
            };
            if (request.ProductIds != null)
                foreach (var i in request.ProductIds)
                {
                    var op = new OrderProduct
                    {
                        OrderId = entity.Id,
                        ProductId = i
                    };
                    _dbContext.OrderProducts.Add(op);
                }
            _dbContext.Orders.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Order> UpdateOrder(Guid id, CreateOrUpdateOrderRequest request)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(a => a.Id == id);
            entity.Id = id;
            entity.ClientId = request.ClientId;
            List<Guid> AddProductIdsList = new List<Guid>();
            List<OrderProduct> RemoveProductIdsList = new List<OrderProduct>();
            if (request.ProductIds != null)
            {
                foreach (var newprodid in request.ProductIds)
                    if (await _dbContext.OrderProducts.Where(t => t.OrderId == entity.Id).FirstOrDefaultAsync(t => t.ProductId == newprodid) == null)
                        AddProductIdsList.Add(newprodid);
                foreach (var oldprod in await _dbContext.OrderProducts.Where(t => t.OrderId == entity.Id).ToListAsync())
                    if (!request.ProductIds.ToList().Contains(oldprod.ProductId))
                        RemoveProductIdsList.Add(oldprod);
                foreach (var ri in RemoveProductIdsList)
                    _dbContext.OrderProducts.Remove(ri);
                foreach (var ai in AddProductIdsList)
                {
                    var op = new OrderProduct
                    {
                        OrderId = entity.Id,
                        ProductId = ai
                    };
                    _dbContext.OrderProducts.Add(op);
                }
            }
            else
            {
                foreach (var f in await _dbContext.OrderProducts.Where(t => t.OrderId == entity.Id).ToListAsync())
                {
                    _dbContext.OrderProducts.Remove(f);
                }
            }
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        #region GetAll
        public async Task<IReadOnlyCollection<Order>> GetAll()
        {
            var query = _dbContext.Orders.OrderByDescending(a => a.DateOfCompletion).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<Client>> GetAllClients()
        {
            var query = _dbContext.Clients.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<Product>> GetAllProducts()
        {
            var query = _dbContext.Products.OrderByDescending(a => a.Price).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        } 
        #endregion
        public async Task<Order> OrderNextStatusById(Guid id)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(a => a.Id == id);
            entity.OrderStatus = entity.OrderStatus switch
            {
                OrderStatuses.Basket => OrderStatuses.WaitingForPayment,
                OrderStatuses.WaitingForPayment => OrderStatuses.WaitingForSupply,
                OrderStatuses.WaitingForSupply => OrderStatuses.ReadyForTakeAway,
                OrderStatuses.ReadyForTakeAway => OrderStatuses.Completed,
                _ => OrderStatuses.Completed
            };
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        #region GetById
        public async Task<Order> GetById(Guid _Id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Anime> GetAnimeById(Guid _Id)
        {
            return await _dbContext.Animes.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Book> GetBookById(Guid _Id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Figure> GetFigureById(Guid _Id)
        {
            return await _dbContext.Figures.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Client> GetClientById(Guid _Id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Product> GetProductById(Guid _Id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<ICollection<OrderProduct>> GetOrderProductsByOrderId(Guid _Id)
        {
            return await _dbContext.OrderProducts.Where(g => g.OrderId == _Id).ToListAsync();
        }
        public async Task<ICollection<PaymentInfo>> GetPaymentInfosByClientId(Guid id)
        {
            var entityes = await _dbContext.PaymentInfos.Where(pi => pi.ClientId == id).ToListAsync();
            return entityes;
        }
        #endregion
        public async Task<Order> DeleteById(Guid _Id)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == _Id);
            _dbContext.Orders.Remove(entity);
            foreach (var rop in await _dbContext.OrderProducts.Where(t => t.OrderId == _Id).ToListAsync())
            {
                _dbContext.OrderProducts.Remove(rop);
            }
            await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}
