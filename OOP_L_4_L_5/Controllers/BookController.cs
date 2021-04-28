using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOP_L_4_L_5.Managers.Books;
using OOP_L_4_L_5.Storage;

namespace OOP_L_4_L_5.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookManager _manager;
        public BookController(IBookManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> BookCatalog(string SearchName = null, string SearchMinPg = null, string SearchMaxPg = null, string _SearchArtGenreComedy = null, string _SearchArtGenreDrama = null, string _SearchArtGenreFantasy = null, string _SearchArtGenreRomance = null, string _SearchArtGenreSliceOfLife = null)
        {
            try
            {
                var entity = await _manager.GetAll();
                if ((string.IsNullOrEmpty(SearchName) && string.IsNullOrEmpty(SearchMinPg) && string.IsNullOrEmpty(SearchMaxPg) && string.IsNullOrEmpty(_SearchArtGenreComedy) && string.IsNullOrEmpty(_SearchArtGenreDrama) && string.IsNullOrEmpty(_SearchArtGenreFantasy) && string.IsNullOrEmpty(_SearchArtGenreRomance) && string.IsNullOrEmpty(_SearchArtGenreSliceOfLife)) || (entity.Count == 0))
                    return View(entity);
                if (string.IsNullOrEmpty(SearchName) && string.IsNullOrEmpty(SearchMinPg) && string.IsNullOrEmpty(SearchMaxPg) && _SearchArtGenreComedy == "true" && _SearchArtGenreDrama == "true" && _SearchArtGenreFantasy == "true" && _SearchArtGenreRomance == "true" && _SearchArtGenreSliceOfLife == "true")
                    return View(entity);

                bool SearchArtGenreComedy = (_SearchArtGenreComedy == "true" ? true : false), SearchArtGenreDrama = (_SearchArtGenreDrama == "true" ? true : false), SearchArtGenreFantasy = (_SearchArtGenreFantasy == "true" ? true : false), SearchArtGenreRomance = (_SearchArtGenreRomance == "true" ? true : false), SearchArtGenreSliceOfLife = (_SearchArtGenreSliceOfLife == "true" ? true : false);

                entity = await _manager.SearchInAll(SearchName, SearchMinPg, SearchMaxPg, SearchArtGenreComedy, SearchArtGenreDrama, SearchArtGenreFantasy, SearchArtGenreRomance, SearchArtGenreSliceOfLife);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Book/BookCatalog", ex));
            }
        }
        public ViewResult CreateBook()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Book/BookCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateBookRequest request)
        {
            try
            {
                await _manager.AddBook(request);
                return RedirectToAction(nameof(BookCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Book/CreateBook", ex));
            }
        }
        [HttpGet]
        public async Task<ViewResult> UpdateBook(Guid id)
        {
            try
            {
                var entity = await _manager.GetById(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Book/BookCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid BookId, CreateOrUpdateBookRequest request)
        {
            try
            {
                await _manager.UpdateBook(BookId, request);
                return RedirectToAction(nameof(BookCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Book/UpdateBook/" + BookId, ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _manager.DeleteById(id);
                return RedirectToAction(nameof(BookCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Book/BookCatalog", ex));
            }
        }
    }
}