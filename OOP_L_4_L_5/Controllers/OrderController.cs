using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOP_L_4_L_5.Managers.Orders;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;

namespace OOP_L_4_L_5.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager _manager;
        public OrderController(IOrderManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> OrderCatalog()
        {
            try {
                var entityes = await _manager.GetAll();

                foreach (var o in entityes)
                {
                    o.Client = await _manager.GetClientById(o.ClientId);
                    o.OrderProducts = await _manager.GetOrderProductsByOrderId(o.Id);
                    foreach (var op in o.OrderProducts)
                    {
                        op.Product = await _manager.GetProductById(op.ProductId);
                        if (op.Product.ProductType == ProductTypes.Anime)
                        {
                            op.Product.Anime = await _manager.GetAnimeById(op.Product.MatterId);
                        }
                        if (op.Product.ProductType == ProductTypes.Book)
                        {
                            op.Product.Book = await _manager.GetBookById(op.Product.MatterId);
                        }
                        if (op.Product.ProductType == ProductTypes.Figure)
                        {
                            op.Product.Figure = await _manager.GetFigureById(op.Product.MatterId);
                        }
                    }
                    o.Client.PaymentInfos = await _manager.GetPaymentInfosByClientId(o.ClientId);

                }

                return View(entityes);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Order/OrderCatalog", ex));
            }
        }
        public async Task<ViewResult> CreateOrder()
        {
            try {
                var lp = await _manager.GetAllProducts();
                var lc = await _manager.GetAllClients();
                if (lc.Count < 1)
                {
                    var entityes = await _manager.GetAll();

                    foreach (var o in entityes)
                    {
                        o.Client = await _manager.GetClientById(o.ClientId);
                        o.OrderProducts = await _manager.GetOrderProductsByOrderId(o.Id);
                        foreach (var op in o.OrderProducts)
                        {
                            op.Product = await _manager.GetProductById(op.ProductId);
                            if (op.Product.ProductType == ProductTypes.Anime)
                            {
                                op.Product.Anime = await _manager.GetAnimeById(op.Product.MatterId);
                            }
                            if (op.Product.ProductType == ProductTypes.Book)
                            {
                                op.Product.Book = await _manager.GetBookById(op.Product.MatterId);
                            }
                            if (op.Product.ProductType == ProductTypes.Figure)
                            {
                                op.Product.Figure = await _manager.GetFigureById(op.Product.MatterId);
                            }
                        }
                        o.Client.PaymentInfos = await _manager.GetPaymentInfosByClientId(o.ClientId);

                    }
                    return View("OrderCatalog", entityes);
                }
                foreach (var e in lp)
                {
                    if (e.ProductType == ProductTypes.Anime)
                    {
                        e.Anime = await _manager.GetAnimeById(e.MatterId);
                    }
                    if (e.ProductType == ProductTypes.Book)
                    {
                        e.Book = await _manager.GetBookById(e.MatterId);
                    }
                    if (e.ProductType == ProductTypes.Figure)
                    {
                        e.Figure = await _manager.GetFigureById(e.MatterId);
                    }
                }
                foreach (var c in lc)
                {
                    c.PaymentInfos = await _manager.GetPaymentInfosByClientId(c.Id);
                }
                return View(new CreateOrUpdateOrderModel(null, lp, lc));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Order/OrderCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateOrderRequest request)
        {
            try {
                await _manager.AddOrder(request);
                return RedirectToAction(nameof(OrderCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Order/CreateOrder", ex));
            }
        }
        [HttpGet]
        public async Task<ViewResult> UpdateOrder(Guid id)
        {
            try {
                var entity = await _manager.GetById(id);
                entity.OrderProducts = await _manager.GetOrderProductsByOrderId(id);
                foreach (var op in entity.OrderProducts)
                    op.Product = await _manager.GetProductById(op.ProductId);
                var lp = await _manager.GetAllProducts();
                var lc = await _manager.GetAllClients();
                foreach (var e in lp)
                {
                    if (e.ProductType == ProductTypes.Anime)
                    {
                        e.Anime = await _manager.GetAnimeById(e.MatterId);
                    }
                    if (e.ProductType == ProductTypes.Book)
                    {
                        e.Book = await _manager.GetBookById(e.MatterId);
                    }
                    if (e.ProductType == ProductTypes.Figure)
                    {
                        e.Figure = await _manager.GetFigureById(e.MatterId);
                    }
                }
                foreach (var c in lc)
                {
                    c.PaymentInfos = await _manager.GetPaymentInfosByClientId(c.Id);
                }
                return View(new CreateOrUpdateOrderModel(entity, lp, lc));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Order/OrderCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> NextStatus(Guid OrderId)
        {
            try {
                await _manager.OrderNextStatusById(OrderId);
                return RedirectToAction(nameof(OrderCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Order/OrderCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid OrderId, CreateOrUpdateOrderRequest request)
        {
            try {
                await _manager.UpdateOrder(OrderId, request);
                return RedirectToAction(nameof(OrderCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Order/UpdateOrder/" + OrderId, ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            try {
                await _manager.DeleteById(id);
                return RedirectToAction(nameof(OrderCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Order/OrderCatalog", ex));
            }
        }
    }
}