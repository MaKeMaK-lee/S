using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOP_L_4_L_5.Managers.PaymentInfos;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;

namespace OOP_L_4_L_5.Controllers
{
    public class PaymentInfoController : Controller
    {

        private readonly IPaymentInfoManager _manager;
        public PaymentInfoController(IPaymentInfoManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> PaymentInfoCatalog()
        {
            try
            {
                var entityes = await _manager.GetAll();
                foreach (var e in entityes)
                {
                    e.Client = await _manager.GetClientById(e.ClientId);
                }
                return View(entityes);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/PaymentInfo/PaymentInfoCatalog", ex));
            }
        }
        public async Task<ViewResult> CreatePaymentInfo()
        {
            try
            {
                return View(new CreateOrUpdatePaymentInfoModel(null, await _manager.GetAllClients()));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/PaymentInfo/PaymentInfoCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdatePaymentInfoRequest request)
        {
            try
            {
                await _manager.AddPaymentInfo(request);
                return RedirectToAction(nameof(PaymentInfoCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/PaymentInfo/CreatePaymentInfo", ex));
            }
        }
        [HttpGet]
        public async Task<ViewResult> UpdatePaymentInfo(Guid id)
        {
            try
            {
                var entity = await _manager.GetById(id);
                return View(new CreateOrUpdatePaymentInfoModel(entity, await _manager.GetAllClients()));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/PaymentInfo/PaymentInfoCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid PaymentInfoId, CreateOrUpdatePaymentInfoRequest request)
        {
            try
            {
                await _manager.UpdatePaymentInfo(PaymentInfoId, request);
                return RedirectToAction(nameof(PaymentInfoCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/PaymentInfo/UpdatePaymentInfo/" + PaymentInfoId, ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeletePaymentInfo(Guid id)
        {
            try
            {
                await _manager.DeleteById(id);
                return RedirectToAction(nameof(PaymentInfoCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/PaymentInfo/PaymentInfoCatalog", ex));
            }
        }
    }
}