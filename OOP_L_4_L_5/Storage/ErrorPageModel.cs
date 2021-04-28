using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage
{
    public class ErrorPageModel
    {
        public Exception Exception { get; set; }
        public string BackPath { get; set; }
        public ErrorPageModel (string _BackPath = "/Anime/AnimeCatalog", Exception _Exception = null)
        {
            BackPath = _BackPath;
            Exception = (_Exception ?? new Exception("Неизвестная ошибка, лолкек"));
        }
    }
}
