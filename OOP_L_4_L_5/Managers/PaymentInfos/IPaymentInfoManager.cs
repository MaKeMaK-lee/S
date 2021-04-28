using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.PaymentInfos
{
    public interface IPaymentInfoManager
    {
        Task<PaymentInfo> AddPaymentInfo(CreateOrUpdatePaymentInfoRequest request);
        Task<IReadOnlyCollection<PaymentInfo>> GetAll();
        Task<PaymentInfo> UpdatePaymentInfo(Guid id, CreateOrUpdatePaymentInfoRequest request);
        Task<PaymentInfo> GetById(Guid _Id);
        Task<PaymentInfo> DeleteById(Guid _Id);
        Task<IReadOnlyCollection<Client>> GetAllClients();
        Task<Client> GetClientById(Guid _Id);
    }
}
