using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Clients
{
    public interface IClientManager
    {
        Task<Client> AddClient(CreateOrUpdateClientRequest request);
        Task<IReadOnlyCollection<Client>> GetAll();
        Task<Client> UpdateClient(Guid id, CreateOrUpdateClientRequest request);
        Task<Client> GetById(Guid _Id);
        Task<Client> DeleteById(Guid _Id);
        Task<ICollection<PaymentInfo>> GetPaymentInfosByClientId(Guid id);
        //Task<Client> DeleteById(Guid _Id);
        //Task<Client> GetById(Guid _Id);
        //Task<Client> UpdateClient(Guid id, CreateOrUpdateClientRequest request);
        //Task<IReadOnlyCollection<Client>> GetAll();
        //Task<Client> AddClient(CreateOrUpdateClientRequest request);
        ////Task<IReadOnlyCollection<PaymentInfo>> GetAllPaymentInfos();

    }
}
