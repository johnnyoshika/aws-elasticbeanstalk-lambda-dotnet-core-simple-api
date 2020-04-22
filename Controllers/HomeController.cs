using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet("info")]
        public IActionResult Info() => Content(Directory.GetCurrentDirectory());

        [HttpGet("list-files")]
        public IActionResult ListFiles() => Content(string.Join('\n', Directory.GetFiles(Directory.GetCurrentDirectory())));

        [HttpGet("list-folders")]
        public IActionResult ListFolders() => Content(string.Join('\n', Directory.GetDirectories(Directory.GetCurrentDirectory())));

        [HttpGet("write-file")]
        public IActionResult WriteFile(string content)
        {
            string fileName = $"{Guid.NewGuid().ToString()}.txt";
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory()), fileName);
            return Content(fileName);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
