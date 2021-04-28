using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOP_L_4_L_5.Managers.Products;
using OOP_L_4_L_5.Storage;

namespace OOP_L_4_L_5.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _manager;
        public ProductController(IProductManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ProductCatalog(string SearchMinCountInStock = null, string[] SearchProductType = null, string SearchMinPrice = null, string SearchMaxPrice = null)
        {
            try
            {
                var entity = await _manager.GetAll();

                if (!((string.IsNullOrEmpty(SearchMinCountInStock) && string.IsNullOrEmpty(SearchMinPrice) && string.IsNullOrEmpty(SearchMaxPrice) && (SearchProductType.Length == 0 || SearchProductType.Length == 3)) || (entity.Count == 0)))
                    entity = await _manager.SearchInAll(SearchMinCountInStock, SearchProductType, SearchMinPrice, SearchMaxPrice);

                foreach (var e in entity)
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
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Product/ProductCatalog", ex));
            }
        }
        public async Task<ViewResult> CreateProduct()
        {
            try
            {
                return View(new CreateOrUpdateProductModel(null, await _manager.GetAllAnimes(), await _manager.GetAllBooks(), await _manager.GetAllFigures()));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Product/ProductCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateProductRequest request)
        {
            try
            {
                await _manager.AddProduct(request);
                return RedirectToAction(nameof(ProductCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Product/CreateProduct", ex));
            }
        }
        [HttpGet]
        public async Task<ViewResult> UpdateProduct(Guid id)
        {
            try
            {
                var entity = await _manager.GetById(id);
                return View(new CreateOrUpdateProductModel(entity, await _manager.GetAllAnimes(), await _manager.GetAllBooks(), await _manager.GetAllFigures()));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Product/ProductCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid ProductId, CreateOrUpdateProductRequest request)
        {
            try
            {
                await _manager.UpdateProduct(ProductId, request);
                return RedirectToAction(nameof(ProductCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Product/UpdateProduct/" + ProductId, ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _manager.DeleteById(id);
                return RedirectToAction(nameof(ProductCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Product/ProductCatalog", ex));
            }
        }
    }
}