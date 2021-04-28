using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Managers.Figures
{
    public class CreateOrUpdateFigureRequest
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public int Scale { get; set; }
        public int Weight { get; set; }
    }
}
