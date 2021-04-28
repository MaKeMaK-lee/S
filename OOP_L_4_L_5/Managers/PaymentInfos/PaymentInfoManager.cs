using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.PaymentInfos
{ 
    public class PaymentInfoManager : IPaymentInfoManager
    {
        private readonly RestShopDataContext _dbContext;
        public PaymentInfoManager(RestShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaymentInfo> AddPaymentInfo(CreateOrUpdatePaymentInfoRequest request)
        {
            var entity = new PaymentInfo
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CardNumber = request.CardNumber,
                Cvc = request.Cvc,
                ClientId = request.ClientId
            };
            _dbContext.PaymentInfos.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IReadOnlyCollection<Client>> GetAllClients()
        {
            var query = _dbContext.Clients.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<PaymentInfo>> GetAll()
        {
            var query = _dbContext.PaymentInfos.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<PaymentInfo> UpdatePaymentInfo(Guid id, CreateOrUpdatePaymentInfoRequest request)
        {
            var entity = await _dbContext.PaymentInfos.FirstOrDefaultAsync(a => a.Id == id);
            entity.Name = request.Name;
            entity.CardNumber = request.CardNumber;
            entity.Cvc = request.Cvc;
            entity.ClientId = request.ClientId;

            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<PaymentInfo> GetById(Guid _Id)
        {
            return await _dbContext.PaymentInfos.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<PaymentInfo> DeleteById(Guid _Id)
        {
            var entity = await _dbContext.PaymentInfos.FirstOrDefaultAsync(g => g.Id == _Id);
            _dbContext.PaymentInfos.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Client> GetClientById(Guid _Id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(g => g.Id == _Id);
        }
    }
}
