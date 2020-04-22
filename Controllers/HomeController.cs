using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        static List<Product> _products = new List<Product>
        {
            new Product {Id = 1 , Name = "Car"},
            new Product {Id = 2 , Name = "Plane"},
        };

        public IActionResult GetProducts() =>
            Json(_products);

        [HttpGet("products/{id:int}")]
        public IActionResult GetProduct(int id) =>
            Json(_products.FirstOrDefault(p => p.Id == id));
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
