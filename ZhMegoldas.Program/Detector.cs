using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZhMegoldas.Program
{
    class Detector
    {
        public Queue<IPizza> DetectPizzaClasses()
        {
            Type[] types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterface("IPizza") != null)
                .OrderByDescending(x => x.FullName.Length)
                .ToArray();

            XDocument xdoc = new XDocument();
            xdoc.Add(new XElement("pizzas",
                new XAttribute("count", types.Length)));

            foreach (Type type in types)
            {
                string name = type.Name;
                xdoc.Root.Add(new XElement("class",
                    new XElement("name", name, new XAttribute("length", name.Length)),
                    new XElement("hash", name.GetHashCode())
                ));
            }
            return CreateQueue(types);
        }

        private Queue<IPizza> CreateQueue(Type[] types)
        {
            Queue<IPizza> pizzas = new Queue<IPizza>();
            foreach (var type in types) 
            {
                var instance = Activator.CreateInstance(type);
                pizzas.Enqueue((IPizza) instance);
            }
            return pizzas;
        }
    }
}
