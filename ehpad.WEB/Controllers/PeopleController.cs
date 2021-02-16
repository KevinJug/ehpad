using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ehpad.ORM;
using System.Globalization;

namespace ehpad.WEB.Controllers
{
    public class PeopleController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _config;

        public PeopleController(IConfiguration conf)
        {
            _config = conf;
            _context = new Context(conf.GetConnectionString("BDD"));
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.Peoples.OrderBy(people => people.Name).ThenBy(people => people.Firstname).ToListAsync());
        }

        // GET: People/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.Peoples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Firstname,Sex,Birth,Condition")] People people)
        {
            if (ModelState.IsValid)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                people.Name = people.Name.Trim().ToUpper();
                people.Firstname = textInfo.ToTitleCase(people.Firstname.Trim());
                _context.Add(people);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(people);
        }

        // GET: People/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.Peoples.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: People/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Firstname,Sex,Birth,Condition")] People people)
        {
            if (id != people.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    people.Name = people.Name.Trim().ToUpper();
                    people.Firstname = textInfo.ToTitleCase(people.Firstname.Trim());
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.Id))
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
            return View(people);
        }

        // GET: People/Delete/:id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.Peoples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var people = await _context.Peoples.FindAsync(id);
            _context.Peoples.Remove(people);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeopleExists(int id)
        {
            return _context.Peoples.Any(e => e.Id == id);
        }
    }
}
