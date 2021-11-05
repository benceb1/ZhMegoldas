using FuncLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhMegoldas.Program
{
    public class Pizza : IPizza
    {
        public string Type { get; set; }

        [ToBeFiltered]
        public int Size { get; set; }
        public int PastaThickness { get; set; }
        public int NumberOfToppings { get; set; }
        public int Price { get; set; }
        public string FantasyName { get; set; }

    }
}
