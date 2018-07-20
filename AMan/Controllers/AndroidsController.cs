using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMan.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AMan.Controllers
{
	public class AndroidsController : Controller
    {
        private readonly AManJobContext _context;
		private IHostingEnvironment _appEnvironment;
		private readonly ILogger _logger;

		public AndroidsController(AManJobContext context, IHostingEnvironment appEnvironment, ILogger<HomeController> logger)
        {
            _context = context;
			_appEnvironment = appEnvironment;
			_logger = logger;
		}

		// GET: Androids
		[Authorize]
		public async Task<IActionResult> Index()
        {
            return View(await _context.Android.ToListAsync());
        }

		// GET: Androids/Details/5
		[Authorize]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var android = await _context.Android
                .SingleOrDefaultAsync(m => m.Id == id);
            if (android == null)
            {
                return NotFound();
            }

            return View(android);
        }

		// GET: Androids/Create
		[Authorize]
		public IActionResult Create()
        {
            return View();
        }

        // POST: Androids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create([Bind("Id,Name,SkillsTags,Reliability,Status")] Android android, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
				if (avatar != null)
				{
					string fileName = Path.GetFileNameWithoutExtension(avatar.FileName);
					string extension = Path.GetExtension(avatar.FileName);
					fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
					string path = Path.Combine("Avatars", fileName);

					android.AvatarPath = path;

					using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
					{
						await avatar.CopyToAsync(fileStream);
					}
				}

				_context.Add(android);
				
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

			


			return View(android);
        }

		// GET: Androids/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var android = await _context.Android.SingleOrDefaultAsync(m => m.Id == id);
            if (android == null)
            {
                return NotFound();
            }
            return View(android);
        }

        // POST: Androids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Avatar,Reliability,Status")] Android android)
        {
            if (id != android.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(android);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AndroidExists(android.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(android);
        }

		// GET: Androids/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var android = await _context.Android
                .SingleOrDefaultAsync(m => m.Id == id);
            if (android == null)
            {
                return NotFound();
            }

            return View(android);
        }

        // POST: Androids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var android = await _context.Android.SingleOrDefaultAsync(m => m.Id == id);
            _context.Android.Remove(android);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AndroidExists(int id)
        {
            return _context.Android.Any(e => e.Id == id);
        }
    }
}
