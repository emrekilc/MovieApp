
// See https://aka.ms/new-console-template for more information
using LINQSample.Data;
using System;
namespace LINQSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                //var products = db.Products.ToList();
                var products = db.Products.Select(products=>products.ProductName).ToList();
                foreach (var product in products)
                {
                    Console.WriteLine(product);

                }
            }
            Console.ReadLine();
        }
    }
}

