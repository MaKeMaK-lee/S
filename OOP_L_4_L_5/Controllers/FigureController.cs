using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOP_L_4_L_5.Managers.Figures;
using OOP_L_4_L_5.Storage;

namespace OOP_L_4_L_5.Controllers
{
    public class FigureController : Controller
    {
        private readonly IFigureManager _manager;
        public FigureController(IFigureManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> FigureCatalog(string SearchName = null, string SearchSource = null)
        {
            try
            {
                var entity = await _manager.GetAll();
                if ((string.IsNullOrEmpty(SearchName) && string.IsNullOrEmpty(SearchSource)) || (entity.Count == 0))
                    return View(entity);

                entity = await _manager.SearchInAll(SearchName, SearchSource);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Figure/FigureCatalog", ex));
            }
        }
        public ViewResult CreateFigure()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Figure/FigureCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateFigureRequest request)
        {
            try
            {
                await _manager.AddFigure(request);
                return RedirectToAction(nameof(FigureCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Figure/CreateFigure", ex));
            }
        }
        [HttpGet]
        public async Task<ViewResult> UpdateFigure(Guid id)
        {
            try
            {
                var entity = await _manager.GetById(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Figure/FigureCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid FigureId, CreateOrUpdateFigureRequest request)
        {
            try
            {
                await _manager.UpdateFigure(FigureId, request);
                return RedirectToAction(nameof(FigureCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Figure/UpdateFigure/" + FigureId, ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeleteFigure(Guid id)
        {
            try
            {
                await _manager.DeleteById(id);
                return RedirectToAction(nameof(FigureCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Figure/FigureCatalog", ex));
            }
        }
    }
}