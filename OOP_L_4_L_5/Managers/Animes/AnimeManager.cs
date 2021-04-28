using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Animes
{
    public class AnimeManager : IAnimeManager
    {
        private readonly RestShopDataContext _dbContext;
        public AnimeManager(RestShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Anime> AddAnime(CreateOrUpdateAnimeRequest request)
        {
            var entity = new Anime
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Studio = request.Studio,
                Date = request.Date,
                EpisodesCount = request.EpisodesCount
            };
            entity.AnimeType = request.AnimeType switch
            {
                "TV" => AnimeTypes.TV,
                "Movie" => AnimeTypes.Movie,
                "OVA" => AnimeTypes.OVA,
                _ => AnimeTypes.Other,
            };
            entity.Genre = request.ArtGenre switch
            {
                "Comedy" => ArtGenres.Comedy,
                "Drama" => ArtGenres.Drama,
                "Fantasy" => ArtGenres.Fantasy,
                "Romance" => ArtGenres.Romance,
                "SliceOfLife" => ArtGenres.SliceOfLife,
                _ => throw (new Exception("ArtGenre???"))
            };
            _dbContext.Animes.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IReadOnlyCollection<Anime>> GetAll()
        {
            var query = _dbContext.Animes.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<Anime>> SearchInAll(string SearchName, string SearchMinEp, string SearchMaxEp, bool SearchArtGenreComedy, bool SearchArtGenreDrama, bool SearchArtGenreFantasy, bool SearchArtGenreRomance, bool SearchArtGenreSliceOfLife)
        {
            var query = _dbContext.Animes.OrderBy(a => a.Name).AsNoTracking();

            var list = from a in query
                       where string.IsNullOrEmpty(SearchName) ? true : a.Name.ToLower().Contains(SearchName.ToLower())
                       where string.IsNullOrEmpty(SearchMinEp) ? true : a.EpisodesCount >= Convert.ToInt32(SearchMinEp)
                       where string.IsNullOrEmpty(SearchMaxEp) ? true : a.EpisodesCount <= Convert.ToInt32(SearchMaxEp)
                       where (a.Genre == ArtGenres.Comedy ? SearchArtGenreComedy : true) && (a.Genre == ArtGenres.Drama ? SearchArtGenreDrama : true) && (a.Genre == ArtGenres.Fantasy ? SearchArtGenreFantasy : true) && (a.Genre == ArtGenres.Romance ? SearchArtGenreRomance : true) && (a.Genre == ArtGenres.SliceOfLife ? SearchArtGenreSliceOfLife : true)
                       select a;

            var qq = await list.ToListAsync();

            return qq;
        }
        public async Task<Anime> UpdateAnime(Guid id, CreateOrUpdateAnimeRequest request)
        {
            var entity = await _dbContext.Animes.FirstOrDefaultAsync(a => a.Id == id);
            entity.Name = request.Name;
            entity.Studio = request.Studio;
            entity.Date = request.Date;
            entity.EpisodesCount = request.EpisodesCount;
            entity.AnimeType = request.AnimeType switch
            {
                "TV" => AnimeTypes.TV,
                "Movie" => AnimeTypes.Movie,
                "OVA" => AnimeTypes.OVA,
                _ => AnimeTypes.Other,
            };
            entity.Genre = request.ArtGenre switch
            {
                "Comedy" => ArtGenres.Comedy,
                "Drama" => ArtGenres.Drama,
                "Fantasy" => ArtGenres.Fantasy,
                "Romance" => ArtGenres.Romance,
                "SliceOfLife" => ArtGenres.SliceOfLife,
                _ => ArtGenres.Comedy
            };
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Anime> GetById(Guid _Id)
        {
            return await _dbContext.Animes.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Anime> DeleteById(Guid _Id)
        {
            var entity = await _dbContext.Animes.FirstOrDefaultAsync(g => g.Id == _Id);
            _dbContext.Animes.Remove(entity);
            foreach (var f in await _dbContext.Products.Where(g => g.MatterId == _Id).ToListAsync())
                f.MatterId = Guid.Empty;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}