using Microsoft.AspNetCore.Mvc;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;
using System.Linq;

namespace ProductsWithRouting.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet("products")]
        [HttpGet("products/index")]
        [HttpGet("items")]
        [HttpGet("items/index")]
        public IActionResult Index(string name, decimal? minPrice, decimal? maxPrice)
        {
            var products = Data.Products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(p => p.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            ViewBag.NameFilter = name;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View(products.ToList());
        }

        [HttpGet("products/{id:int}")]
        public IActionResult View(int id)
        {
            var product = Data.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }

            return View(product);
        }

        [HttpGet("products/create")]
        [HttpGet("products/new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("products/create")]
        [HttpPost("products/new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Data.Products.Any() ? Data.Products.Max(p => p.Id) + 1 : 1;
                Data.Products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet("products/edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var product = Data.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }

            return View(product);
        }

        [HttpPost("products/edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product updatedProduct)
        {
            var product = Data.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }

            if (ModelState.IsValid)
            {
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                return RedirectToAction(nameof(Index));
            }

            return View(updatedProduct);
        }

        [HttpGet("products/delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = Data.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }

            Data.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        [Route("Products/NotFoundPage")]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("ProductNotFound");
        }
    }
}