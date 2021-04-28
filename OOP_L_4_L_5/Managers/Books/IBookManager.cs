using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Books
{
    public interface IBookManager
    {
        Task<IReadOnlyCollection<Book>> GetAll();
        Task<Book> AddBook(CreateOrUpdateBookRequest request);
        Task<Book> GetById(Guid Id);
        Task<Book> UpdateBook(Guid id, CreateOrUpdateBookRequest request);
        Task<Book> DeleteById(Guid _Id);
        Task<IReadOnlyCollection<Book>> SearchInAll(string SearchName, string SearchMinPg, string SearchMaxPg, bool SearchArtGenreComedy, bool SearchArtGenreDrama, bool SearchArtGenreFantasy, bool SearchArtGenreRomance, bool SearchArtGenreSliceOfLife);
    }
}
