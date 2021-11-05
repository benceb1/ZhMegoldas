using FuncLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ZhMegoldas.Program
{
    static class Operations
    {
        // A 'this' miatt lesz minden gyűjteménynek egy meghívható metódusa
        public static void ToConsole<T>(this IEnumerable<T> input, string header)
        {
            Console.WriteLine($"********** {header} **********");
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"********** {header} **********");
            Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Detector detector = new Detector();
            foreach(IPizza value in detector.DetectPizzaClasses())
            {
                Console.WriteLine(value);
            }

            Func<string, IEnumerable<Pizza>> createListDelegate = (filename) =>
            {
                XDocument doc = XDocument.Load(filename);
                var pizzaList = from pizzaNode in doc.Descendants("Pizza")
                                select new Pizza()
                                {
                                    Type = pizzaNode.Element("Type")?.Value,
                                    Size = (int) pizzaNode?.Element("Size"),
                                    PastaThickness = (int) pizzaNode?.Element("PastaThickness"),
                                    NumberOfToppings = (int) pizzaNode?.Element("NumberOfToppings"),
                                    Price = (int) pizzaNode?.Element("Price"),
                                    FantasyName = pizzaNode?.Element("FantasyName").Value
                                };

                ObjectExaminer objectExaminer = new ObjectExaminer();
                foreach (var item in pizzaList)
                {
                    string propsString = objectExaminer.ExtractProperties(item, true);
                    Console.WriteLine(propsString);
                }
                return pizzaList;
            };

            IEnumerable<Pizza> pizzaList = createListDelegate("pizza-database.xml");

            // mibol mennyi van, csokkeno sorrendben
            var q1 = from pizza in pizzaList
                          group pizza by pizza.Size into g
                          let count = g.Count()
                          orderby count descending
                          select new
                          {
                              TYPE = g.Key,
                              COUNT = count
                          };
            q1.ToConsole("Q1");

            // nev es tipus ahol legalabb 4 feltet van
            var q2 = from pizza in pizzaList
                     where pizza.NumberOfToppings > 3
                     select new
                     {
                         Name = pizza.FantasyName,
                         Type = pizza.Type
                     };
            q2.ToConsole("Q2");

            // atlagar meret szerint
            var q3 = from pizza in pizzaList
                     group pizza by pizza.Size into g
                     let avgPrice = g.Average(p => p.Price)
                     select new
                     {
                         TYPE = g.Key,
                         COUNT = (int) avgPrice
                     };
            q3.ToConsole("Q3");
        }
    }
}
