using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhMegoldas.Program
{
    interface IPizza
    {
        string Type { get; set; }
        int Size { get; set; }
        int PastaThickness { get; set; }
        int NumberOfToppings { get; set; }
        int Price { get; set; }
    }
}
