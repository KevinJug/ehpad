﻿using ehpad.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ehpad.WEB.Controllers
{
    public class FilterController : Controller
    {
        private readonly Context _context = new Context();

        public async Task<IActionResult> Index()
        {
            ViewData["Injection"] = await _context.Injections
                .Include("People").Include("Vaccine.Drug")
                .Where(m => m.ReminderDate < DateTime.Today)
                .OrderByDescending(injection => injection.Vaccine.Drug.Name).ThenBy(injection => injection.People.Name).ThenBy(injection => injection.People.Firstname)
                .ToListAsync();
            ViewData["Page"] = 2;

            return View();
        }

         // GET: FilterController
        public ActionResult IndexVaccineByPeople()
        {
            IEnumerable<SelectListItem> selectList = from p in _context.Peoples
                                                     select new SelectListItem
                                                     {
                                                         Value = p.Id.ToString(),
                                                         Text = p.Name + " " + p.Firstname
                                                     };
            ViewData["People"] = new SelectList(
               selectList.OrderBy(people => people.Text),
                "Value",
                "Text");
            ViewData["Injection"] = null;
            ViewData["Page"] = 1;
            return View("Index");
        }

        // GET: Filter/IndexFiltre3
        //   [HttpPost]
        //   [ValidateAntiForgeryToken]
        public ActionResult IndexNoVaccine()
        {
            IEnumerable<SelectListItem> selectList = from d in _context.Drugs
                                                     select new SelectListItem
                                                     {
                                                         Value = d.Id.ToString(),
                                                         Text = d.Name
                                                     };
            ViewData["Drug"] = new SelectList(
               selectList.OrderBy(drug => drug.Text),
                "Value",
                "Text");
            ViewData["People"] = null;
            ViewData["Page"] = 3;
            return View("Index");
        }

        // GET: Filter/Filtre3
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598
        public async Task<IActionResult> NoVaccine([Bind("Id")] Drug drug, bool date)
        {
            IEnumerable<SelectListItem> selectList = from d in _context.Drugs
                                                     select new SelectListItem
                                                     {
                                                         Value = d.Id.ToString(),
                                                         Text = d.Name
                                                     };
            ViewData["Drug"] = new SelectList(
               selectList,
                "Value",
                "Text");


            List<People> personneNonVaccineeListe = new List<People>();
            List<People> personneListe = await _context.Peoples.ToListAsync();
            List<Injection> personneVaccineeListe = new List<Injection>();

            if (date is false)
            {
                personneVaccineeListe = await _context.Injections
                   .Include("People").Include("Vaccine.Drug")
                   .Where(m => m.Vaccine.Drug.Id == drug.Id)
                   .ToListAsync();
            }
            else
            {
                DateTime AnneeActuelle = new DateTime(DateTime.Today.Year, 1, 1);
                personneVaccineeListe = await _context.Injections
                   .Include("People").Include("Vaccine.Drug")
                   .Where(m => m.Vaccine.Drug.Id == drug.Id && m.VaccineDate >= AnneeActuelle)
                   .ToListAsync();
            }
            
            personneListe.ForEach(delegate (People people)
            {
                bool present = false;
                personneVaccineeListe.ForEach(delegate (Injection injection)
                {
                    if (injection.PeopleId == people.Id)
                    {
                        present = true;
                    }
                });
                if (present is false)
                {
                    personneNonVaccineeListe.Add(people);
                }
            });     

            ViewData["People"] = personneNonVaccineeListe.OrderBy(people => people.Name).ThenBy(people => people.Firstname);
            ViewData["Page"] = 3;
            return View("Index");
        }

        // POST: Filter/Filtre2
        //   [HttpPost]
        //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexReminderDelay()
        {
            ViewData["Injection"] = await _context.Injections
                .Include("People").Include("Vaccine.Drug")
                .Where(m => m.ReminderDate < DateTime.Today).ToListAsync();

            return View("IndexFiltre2");
        }

        
        // POST: Filter/Details
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("Id")] People people)
        {
            IEnumerable<SelectListItem> selectList = from p in _context.Peoples
                                                     select new SelectListItem
                                                     {
                                                         Value = p.Id.ToString(),
                                                         Text = p.Name + " " + p.Firstname
                                                     };
            ViewData["People"] = new SelectList(
               selectList.OrderBy(people => people.Text),
                "Value",
                "Text");

            ViewData["Injection"] = await _context.Injections
                .Include("Vaccine.Drug")
                .Where(m => m.PeopleId == people.Id)
                .OrderBy(injection => injection.People.Name)
                .ThenBy(injection => injection.People.Firstname)
                .ThenBy(injection => injection.Vaccine.Drug.Name)
                .ThenBy(injection => injection.ReminderDate)
                .ToListAsync();
            ViewData["Page"] = 1;
            return View("Index");
        }

    }
}
