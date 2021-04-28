using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace OOP_L_4_L_5.Managers.Books
{
    public class BookManager : IBookManager
    {
        private readonly RestShopDataContext _dbContext;
        public BookManager(RestShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyCollection<Book>> SearchInAll(string SearchName, string SearchMinPg, string SearchMaxPg, bool SearchArtGenreComedy, bool SearchArtGenreDrama, bool SearchArtGenreFantasy, bool SearchArtGenreRomance, bool SearchArtGenreSliceOfLife)
        {
            var query = _dbContext.Books.OrderBy(a => a.Name).AsNoTracking();

            var list = from a in query
                       where string.IsNullOrEmpty(SearchName) ? true : a.Name.ToLower().Contains(SearchName.ToLower())
                       where string.IsNullOrEmpty(SearchMinPg) ? true : a.PageCount >= Convert.ToInt32(SearchMinPg)
                       where string.IsNullOrEmpty(SearchMaxPg) ? true : a.PageCount <= Convert.ToInt32(SearchMaxPg)
                       where (a.Genre == ArtGenres.Comedy ? SearchArtGenreComedy : true) && (a.Genre == ArtGenres.Drama ? SearchArtGenreDrama : true) && (a.Genre == ArtGenres.Fantasy ? SearchArtGenreFantasy : true) && (a.Genre == ArtGenres.Romance ? SearchArtGenreRomance : true) && (a.Genre == ArtGenres.SliceOfLife ? SearchArtGenreSliceOfLife : true)
                       select a;

            var qq = await list.ToListAsync();

            return qq;
        }
        public async Task<Book> AddBook(CreateOrUpdateBookRequest request)
        {
            var entity = new Book
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Author = request.Author,
                Date = request.Date,
                PageCount = request.PageCount
            };
            entity.BookType = request.BookType switch
            {
                "LightNovel" => BookTypes.LightNovel,
                "Manga" => BookTypes.Manga,
                _ => BookTypes.Other
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
            _dbContext.Books.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IReadOnlyCollection<Book>> GetAll()
        {
            var query = _dbContext.Books.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<Book> UpdateBook(Guid id, CreateOrUpdateBookRequest request)
        {
            var entity = await _dbContext.Books.FirstOrDefaultAsync(a => a.Id == id);
            entity.Name = request.Name;
            entity.Author = request.Author;
            entity.Date = request.Date;
            entity.PageCount = request.PageCount;
            entity.BookType = request.BookType switch
            {
                "LightNovel" => BookTypes.LightNovel,
                "Manga" => BookTypes.Manga,
                _ => BookTypes.Other,
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
        public async Task<Book> GetById(Guid _Id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Book> DeleteById(Guid _Id)
        {
            var entity = await _dbContext.Books.FirstOrDefaultAsync(g => g.Id == _Id);
            _dbContext.Books.Remove(entity);
            foreach (var f in await _dbContext.Products.Where(g => g.MatterId == _Id).ToListAsync())
                f.MatterId = Guid.Empty;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
