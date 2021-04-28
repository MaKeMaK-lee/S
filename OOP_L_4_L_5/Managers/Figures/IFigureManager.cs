using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Figures
{
    public interface IFigureManager
    {
        Task<Figure> AddFigure(CreateOrUpdateFigureRequest request);
        Task<IReadOnlyCollection<Figure>> GetAll();
        Task<IReadOnlyCollection<Figure>> SearchInAll(string SearchName, string SearchSource);
        Task<Figure> UpdateFigure(Guid id, CreateOrUpdateFigureRequest request);
        Task<Figure> GetById(Guid _Id);
        Task<Figure> DeleteById(Guid _Id);
    }
}
