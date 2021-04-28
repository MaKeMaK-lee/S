using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Animes
{
    public interface IAnimeManager
    {
        Task<IReadOnlyCollection<Anime>> GetAll();
        Task<Anime> AddAnime(CreateOrUpdateAnimeRequest request);
        Task<Anime> GetById(Guid Id);
        Task<Anime> UpdateAnime(Guid id, CreateOrUpdateAnimeRequest request);
        Task<Anime> DeleteById(Guid _Id);
        Task<IReadOnlyCollection<Anime>> SearchInAll(string SearchName, string SearchMinEp, string SearchMaxEp, bool SearchArtGenreComedy, bool SearchArtGenreDrama, bool SearchArtGenreFantasy, bool SearchArtGenreRomance, bool SearchArtGenreSliceOfLife);
    }
}
