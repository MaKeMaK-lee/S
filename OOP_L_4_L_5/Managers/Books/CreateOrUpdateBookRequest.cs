using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Books
{
    public class CreateOrUpdateBookRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int PageCount { get; set; }
        public string BookType { get; set; }
        public string ArtGenre { get; set; }
    }
}
