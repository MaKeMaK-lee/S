using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Clients
{
    public class ClientManager : IClientManager
    {
        private readonly RestShopDataContext _dbContext;
        public ClientManager(RestShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Client> AddClient(CreateOrUpdateClientRequest request)
        {
            var entity = new Client
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Telephone = request.Telephone,
                Email= request.Email
            };
            _dbContext.Clients.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IReadOnlyCollection<Client>> GetAll()
        {
            var query = _dbContext.Clients.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<Client> UpdateClient(Guid id, CreateOrUpdateClientRequest request)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(a => a.Id == id);
            entity.Name = request.Name;
            entity.Telephone = request.Telephone;
            entity.Email = request.Email;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Client> GetById(Guid _Id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Client> DeleteById(Guid _Id)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(g => g.Id == _Id);
            _dbContext.Clients.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<ICollection<PaymentInfo>> GetPaymentInfosByClientId(Guid id)
        {
            var entityes = await _dbContext.PaymentInfos.Where(pi => pi.ClientId == id).ToListAsync();
            return entityes;
        }
    }
}
