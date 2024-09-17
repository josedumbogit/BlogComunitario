using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using YourProject.Data;
using BlogComunitario.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BlogComunitario.Controllers
{
	public class PostsController : Controller
	{
		private readonly BlogDbContext _blogDbContext;

		public PostsController(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		// GET: PostsController
		// GET: Posts
		public async Task<IActionResult> Index()
		{
			return View(await _blogDbContext.Posts.ToListAsync());
		}

		// GET: Posts/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var post = await _blogDbContext.Posts
				.FirstOrDefaultAsync(m => m.Id == id);
			if (post == null)
			{
				return NotFound();
			}

			return View(post);
		}

		// GET: Posts/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Posts/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Content,DatePosted,CategoryId")] Post post)
		{
			if (ModelState.IsValid)
			{
				_blogDbContext.Add(post);
				await _blogDbContext.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(post);
		}

		// GET: Posts/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var post = await _blogDbContext.Posts.FindAsync(id);
			if (post == null)
			{
				return NotFound();
			}
			return View(post);
		}

		// POST: Posts/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CreatedAt")] Post post)
		{
			if (id != post.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_blogDbContext.Update(post);
					await _blogDbContext.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PostExists(post.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}
			return View(post);
		}

		// GET: Posts/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var post = await _blogDbContext.Posts
				.FirstOrDefaultAsync(m => m.Id == id);
			if (post == null)
			{
				return NotFound();
			}

			return View(post);
		}

		// POST: Posts/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var post = await _blogDbContext.Posts.FindAsync(id);
			_blogDbContext.Posts.Remove(post);
			await _blogDbContext.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PostExists(int id)
		{
			return _blogDbContext.Posts.Any(e => e.Id == id);
		}

	}
}
