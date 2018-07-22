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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using AMan.ViewModels;

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
            return View(await _context.Android.Include(a => a.CurrentJob).ToListAsync());
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
		public async Task<IActionResult> Create([Bind("Id,Name,SkillsTags,Reliability")] Android android, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
				if (avatar != null)
				{
					string path = GetAvatarPath(avatar);

					android.AvatarPath = "~/" + path;

					using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/" + path, FileMode.Create))
					{
						await avatar.CopyToAsync(fileStream);
					}
				}

				android.Status = true;
				_context.Add(android);
				
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

			return View(android);
        }

		private string GetAvatarPath(IFormFile avatar)
		{
			string fileName = Path.GetFileNameWithoutExtension(avatar.FileName);
			string extension = Path.GetExtension(avatar.FileName);
			fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

			return Path.Combine("Avatars", fileName);
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
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AvatarPath,SkillsTags,Reliability,Status,CurrentJobId")] Android android, IFormFile avatar)
        {
            if (id != android.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					if (avatar != null)
					{
						if (android.AvatarPath != null)
						{
							System.IO.File.Delete(_appEnvironment.WebRootPath + "/Avatars/" + Path.GetFileName(android.AvatarPath));
						}

						string path = GetAvatarPath(avatar);

						android.AvatarPath = "~/" + path;

						using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/" + path, FileMode.Create))
						{
							await avatar.CopyToAsync(fileStream);
						}
					}
					if (android.Reliability == 0)
					{
						android.Status = false;
					}
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
			if (android.AvatarPath != null)
			{
				System.IO.File.Delete(_appEnvironment.WebRootPath + "/Avatars/" + Path.GetFileName(android.AvatarPath));
			}
			_context.Android.Remove(android);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AndroidExists(int id)
        {
            return _context.Android.Any(e => e.Id == id);
        }

		[Authorize]
		public async Task<IActionResult> Assign(int? id)
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

			IEnumerable<SelectListItem> activeJobs = _context.Job
				.Select(a => new SelectListItem()
				{
					Value = a.Id.ToString(),
					Text = a.Name
				}).ToList();

			AndroidViewModel androidViewModel = new AndroidViewModel(android, activeJobs);

			return View(androidViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Assign(int id, [Bind("Android")] AndroidViewModel androidViewModel)
		{
			_logger.LogError("in post");
			if (androidViewModel.Android.CurrentJobId != null)
			{
				var android = await _context.Android.SingleOrDefaultAsync(m => m.Id == id);
				if (android == null)
				{
					return NotFound();
				}

				try
				{
					android.CurrentJobId = androidViewModel.Android.CurrentJobId;
					android.Reliability--;

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
			}
			return RedirectToAction("Index");
		}
	}
}
