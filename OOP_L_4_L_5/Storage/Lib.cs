using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage
{
    public enum OrderStatuses
    {
        Basket,
        WaitingForPayment,
        WaitingForSupply,
        ReadyForTakeAway,
        Completed
    }
    public enum ProductTypes
    {
        Anime,
        Book,
        Figure
    }
    public enum BookTypes
    {
        LightNovel,
        Manga,
        Other
    };
    public enum AnimeTypes
    {
        TV,
        Movie,
        OVA,
        Other
    };
    public enum ArtGenres
    {
        Comedy,
        Drama,
        Romance,
        Fantasy,
        SliceOfLife
    };
    public class Lib
    {
        
    }
}