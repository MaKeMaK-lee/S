using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Products
{
    public class ProductManager : IProductManager
    {
        private readonly RestShopDataContext _dbContext;
        public ProductManager(RestShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> AddProduct(CreateOrUpdateProductRequest request)
        {
            var entity = new Product
            {
                Id = Guid.NewGuid(),
                MatterId = request.MatterId,
                Price = request.Price,
                CountInStock = request.CountInStock,
            };
            var tryAnime = await _dbContext.Animes.FirstOrDefaultAsync(t => t.Id == entity.MatterId);
            if (tryAnime != null)
                entity.ProductType = ProductTypes.Anime;
            else
            {
                var tryBook = await _dbContext.Books.FirstOrDefaultAsync(t => t.Id == entity.MatterId);
                if (tryBook != null)
                    entity.ProductType = ProductTypes.Book;
                else
                {
                    var tryFigure = await _dbContext.Figures.FirstOrDefaultAsync(t => t.Id == entity.MatterId);
                    if (tryFigure!= null)
                        entity.ProductType = ProductTypes.Figure;
                }
            }
            _dbContext.Products.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        #region GetAll
        public async Task<IReadOnlyCollection<Product>> GetAll()
        {
            var query = _dbContext.Products.OrderByDescending(a => a.Price).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<Anime>> GetAllAnimes()
        {
            var query = _dbContext.Animes.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<Book>> GetAllBooks()
        {
            var query = _dbContext.Books.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<Figure>> GetAllFigures()
        {
            var query = _dbContext.Figures.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        #endregion
        #region GetById
        public async Task<Anime> GetAnimeById(Guid _Id)
        {
            return await _dbContext.Animes.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Book> GetBookById(Guid _Id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Figure> GetFigureById(Guid _Id)
        {
            return await _dbContext.Figures.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Product> GetById(Guid _Id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(g => g.Id == _Id);
        } 
        #endregion
        public async Task<IReadOnlyCollection<Product>> SearchInAll(string SearchMinCountInStock, string[] SearchProductType, string SearchMinPrice, string SearchMaxPrice)
        {
            List<ProductTypes> _spt = new List<ProductTypes>();
            foreach (var r in SearchProductType.ToList())
            {
                if (r == "Anime")
                    _spt.Add(ProductTypes.Anime);
                if (r == "Book")
                    _spt.Add(ProductTypes.Book);
                if (r == "Figure")
                    _spt.Add(ProductTypes.Figure);
            }
            var query = _dbContext.Products.OrderBy(a => a.Price).AsNoTracking();

            var list = from a in query
                       where string.IsNullOrEmpty(SearchMinCountInStock) ? true : a.CountInStock >= Convert.ToInt32(SearchMinCountInStock)
                       where string.IsNullOrEmpty(SearchMinPrice) ? true : a.Price >= Convert.ToInt32(SearchMinPrice)
                       where string.IsNullOrEmpty(SearchMaxPrice) ? true : a.Price <= Convert.ToInt32(SearchMaxPrice)
                       where _spt.Contains(a.ProductType)
                       select a;

            var qq = await list.ToListAsync();
            return qq;
        }
        public async Task<Product> UpdateProduct(Guid id, CreateOrUpdateProductRequest request)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(a => a.Id == id);
            entity.MatterId = request.MatterId;
            entity.Price = request.Price;
            entity.CountInStock = request.CountInStock;
            var tryAnime = await _dbContext.Animes.FirstOrDefaultAsync(t => t.Id == entity.MatterId);
            if (tryAnime != null)
                entity.ProductType = ProductTypes.Anime;
            else
            {
                var tryBook = await _dbContext.Books.FirstOrDefaultAsync(t => t.Id == entity.MatterId);
                if (tryBook != null)
                    entity.ProductType = ProductTypes.Book;
                else
                {
                    var tryFigure = await _dbContext.Figures.FirstOrDefaultAsync(t => t.Id == entity.MatterId);
                    if (tryFigure != null)
                        entity.ProductType = ProductTypes.Figure;
                }
            }
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Product> DeleteById(Guid _Id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(g => g.Id == _Id);
            _dbContext.Products.Remove(entity);
            foreach (var rop in await _dbContext.OrderProducts.Where(t => t.ProductId == _Id).ToListAsync())
            {
                _dbContext.OrderProducts.Remove(rop);
            }
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
