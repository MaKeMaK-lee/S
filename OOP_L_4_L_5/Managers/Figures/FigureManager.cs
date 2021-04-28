using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OOP_L_4_L_5.Storage;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Figures
{
    public class FigureManager : IFigureManager
    {
        private readonly RestShopDataContext _dbContext;
        public FigureManager(RestShopDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Figure> AddFigure(CreateOrUpdateFigureRequest request)
        {
            var entity = new Figure
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Source = request.Source,
                Scale = request.Scale,
                Weight = request.Weight
            };
            _dbContext.Figures.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IReadOnlyCollection<Figure>> GetAll()
        {
            var query = _dbContext.Figures.OrderBy(a => a.Name).AsNoTracking();
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IReadOnlyCollection<Figure>> SearchInAll(string SearchName, string SearchSource)
        {
            var query = _dbContext.Figures.OrderBy(a => a.Name).AsNoTracking();

            var list = from a in query
                       where string.IsNullOrEmpty(SearchName) ? true : a.Name.ToLower().Contains(SearchName.ToLower())
                       where string.IsNullOrEmpty(SearchSource) ? true : a.Source.ToLower().Contains(SearchSource.ToLower())
                       select a;

            var qq = await list.ToListAsync();
            return qq;
        }
        public async Task<Figure> UpdateFigure(Guid id, CreateOrUpdateFigureRequest request)
        {
            var entity = await _dbContext.Figures.FirstOrDefaultAsync(a => a.Id == id);
            entity.Name = request.Name;
            entity.Source = request.Source;
            entity.Scale = request.Scale;
            entity.Weight = request.Weight;

            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Figure> GetById(Guid _Id)
        {
            return await _dbContext.Figures.FirstOrDefaultAsync(g => g.Id == _Id);
        }
        public async Task<Figure> DeleteById(Guid _Id)
        {
            var entity = await _dbContext.Figures.FirstOrDefaultAsync(g => g.Id == _Id);
            _dbContext.Figures.Remove(entity);
            foreach (var f in await _dbContext.Products.Where(g => g.MatterId == _Id).ToListAsync())
                f.MatterId = Guid.Empty;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
