using BlogComunitario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogComunitario.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BlogDbContext _blogDbContext;

        public CategoriesController(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _blogDbContext.Categories.ToListAsync());
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _blogDbContext.Add(category);
                await _blogDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
