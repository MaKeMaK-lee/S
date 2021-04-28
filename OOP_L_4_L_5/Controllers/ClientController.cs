using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOP_L_4_L_5.Managers.Clients;
using OOP_L_4_L_5.Storage;

namespace OOP_L_4_L_5.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientManager _manager;
        public ClientController(IClientManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ClientCatalog()
        {
            try
            {
                var entityes = await _manager.GetAll();
                foreach (var c in entityes)
                {
                    c.PaymentInfos = await _manager.GetPaymentInfosByClientId(c.Id);
                }
                return View(entityes);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Client/ClientCatalog", ex));
            }
        }
        public ViewResult CreateClient()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Client/ClientCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateClientRequest request)
        {
            try
            {
                await _manager.AddClient(request);
                return RedirectToAction(nameof(ClientCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Client/CreateClient", ex));
            }
        }
        [HttpGet]
        public async Task<ViewResult> UpdateClient(Guid id)
        {
            try
            {
                var entity = await _manager.GetById(id);
                entity.PaymentInfos = await _manager.GetPaymentInfosByClientId(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Client/ClientCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid ClientId, CreateOrUpdateClientRequest request)
        {
            try
            {
                await _manager.UpdateClient(ClientId, request);
                return RedirectToAction(nameof(ClientCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Client/UpdateClient/" + ClientId, ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeleteClient(Guid id)
        {
            try
            {
                await _manager.DeleteById(id);
                return RedirectToAction(nameof(ClientCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Client/ClientCatalog", ex));
            }
        }
    }
}