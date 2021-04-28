using OOP_L_4_L_5.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Animes
{
    public class CreateOrUpdateAnimeRequest
    {
        public string Name { get; set; }
        public string Studio { get; set; }
        public DateTime Date { get; set; }
        public int EpisodesCount { get; set; }
        public string AnimeType { get; set; } 
        public string ArtGenre { get; set; }
        //public bool TV { get; set; }
        //public bool Movie { get; set; }
        //public bool OVA { get; set; }
        //public bool Other { get; set; }
        //public bool Comedy { get; set; }
        //public bool Drama { get; set; }
        //public bool Romance { get; set; }
        //public bool Fantasy { get; set; }
        //public bool SliceOfLife { get; set; }


    }
}
