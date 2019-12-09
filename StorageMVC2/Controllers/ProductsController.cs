using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StorageMVC2.Models;

namespace StorageMVC2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageMVC2Context _context;
        private readonly IProductRepository _productRepository;

        public IEnumerable<ProductViewModel> ProductViewModel { get; set; }


        public ProductsController(StorageMVC2Context context, IProductRepository productrepository)
        {
            _context = context;
            _productRepository = productrepository;
        }

        //GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        public IActionResult List()
        {
            var product = _productRepository.AllProduct.Select(a => new ProductViewModel
            {
                Name = a.Name,
                Price = a.Price,
                Count = a.Count,
                InventoryValue = a.Count * a.Price             

            });          
            return View(product);
        }

        public async Task<IActionResult> Filter(string category)
        {
            var model = await _context.Product.ToListAsync();
            if (!string.IsNullOrEmpty(category))
            {
                model = model.Where(p => p.Category.Contains(category)).ToList();
            }
            return View(model);        
        }

        [HttpGet]
        public async Task<IActionResult> Dropdown()
        {
            DropdownViewModel model = new DropdownViewModel();
            model.product = _productRepository.AllProduct;
            model.SelectedProductName = null;

            var list = _productRepository.AllProduct.Select(P => new SelectListItem
            {
                Value = P.Name,
                Text = P.Name,
            });

            model.SelectList = list;
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Dropdown(DropdownViewModel model)
        {
            IEnumerable<ProductViewModel> productNew = await _context.Product
               .Where(p => p.Name.StartsWith(model.SelectedProductName))
               .Select(a => new ProductViewModel
               {
                   Name = a.Name,
                   Price = a.Price,
                   Count = a.Count,
                   InventoryValue = a.Count * a.Price

               }).ToListAsync();

            return View(nameof(List), productNew);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
