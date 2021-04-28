using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OOP_L_4_L_5.Managers.Animes;
using OOP_L_4_L_5.Storage.Entity;
using OOP_L_4_L_5.Storage;

//return View("ErrorPage", new ErrorPageModel("/Anime/AnimeCatalog",new Exception("Ошиб очка, получается?))0")));

namespace OOP_L_4_L_5.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeManager _manager;
        public AnimeController(IAnimeManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> AnimeCatalog(string SearchName = null, string SearchMinEp = null, string SearchMaxEp = null, string _SearchArtGenreComedy = null, string _SearchArtGenreDrama = null, string _SearchArtGenreFantasy = null, string _SearchArtGenreRomance = null, string _SearchArtGenreSliceOfLife = null)
        {
            try
            {
                var entity = await _manager.GetAll();
                if ((string.IsNullOrEmpty(SearchName) && string.IsNullOrEmpty(SearchMinEp) && string.IsNullOrEmpty(SearchMaxEp) && string.IsNullOrEmpty(_SearchArtGenreComedy) && string.IsNullOrEmpty(_SearchArtGenreDrama) && string.IsNullOrEmpty(_SearchArtGenreFantasy) && string.IsNullOrEmpty(_SearchArtGenreRomance) && string.IsNullOrEmpty(_SearchArtGenreSliceOfLife)) || (entity.Count == 0))
                    return View(entity);
                if (string.IsNullOrEmpty(SearchName) && string.IsNullOrEmpty(SearchMinEp) && string.IsNullOrEmpty(SearchMaxEp) && _SearchArtGenreComedy == "true" && _SearchArtGenreDrama == "true" && _SearchArtGenreFantasy == "true" && _SearchArtGenreRomance == "true" && _SearchArtGenreSliceOfLife == "true")
                    return View(entity);

                bool SearchArtGenreComedy = (_SearchArtGenreComedy == "true" ? true : false), SearchArtGenreDrama = (_SearchArtGenreDrama == "true" ? true : false), SearchArtGenreFantasy = (_SearchArtGenreFantasy == "true" ? true : false), SearchArtGenreRomance = (_SearchArtGenreRomance == "true" ? true : false), SearchArtGenreSliceOfLife = (_SearchArtGenreSliceOfLife == "true" ? true : false);

                entity = await _manager.SearchInAll(SearchName, SearchMinEp, SearchMaxEp, SearchArtGenreComedy, SearchArtGenreDrama, SearchArtGenreFantasy, SearchArtGenreRomance, SearchArtGenreSliceOfLife);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Anime/AnimeCatalog", ex));
            }
        }
        public ViewResult CreateAnime()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Anime/AnimeCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateAnimeRequest request)
        {
            try
            {
                await _manager.AddAnime(request);
                return RedirectToAction(nameof(AnimeCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Anime/CreateAnime", ex));
            }
        }
        [HttpGet]
        public async Task<ViewResult> UpdateAnime(Guid id)
        {
            try
            {
                var entity = await _manager.GetById(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Anime/AnimeCatalog", ex));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid AnimeId, CreateOrUpdateAnimeRequest request)
        {
            try
            {
                await _manager.UpdateAnime(AnimeId, request);
                return RedirectToAction(nameof(AnimeCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Anime/UpdateAnime/" + AnimeId, ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeleteAnime(Guid id)
        {
            try
            {
                await _manager.DeleteById(id);
                return RedirectToAction(nameof(AnimeCatalog));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", new ErrorPageModel("/Anime/AnimeCatalog", ex));
            }
        }
    }
}