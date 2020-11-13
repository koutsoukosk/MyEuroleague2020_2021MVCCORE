using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEuroleagueMVCAspNetCore.Models;

namespace MyEuroleagueMVCAspNetCore.Controllers
{
    public class TeamsAPIController : Controller
    {
        private readonly Euroleague2020_21ASPDBContext _context;

        public TeamsAPIController(Euroleague2020_21ASPDBContext context)
        {
            _context = context;
        }

        // GET: TeamsAPI
        public async Task<IActionResult> Index()
        {
            return View(await _context.Team.ToListAsync());
        }

        // GET: TeamsAPI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamID == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // GET: TeamsAPI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamsAPI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamID,Name,Country,Coach")] Teams teams)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teams);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teams);
        }

        // GET: TeamsAPI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Team.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }
            return View(teams);
        }

        // POST: TeamsAPI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamID,Name,Country,Coach")] Teams teams)
        {
            if (id != teams.TeamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsExists(teams.TeamID))
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
            return View(teams);
        }

        // GET: TeamsAPI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamID == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // POST: TeamsAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teams = await _context.Team.FindAsync(id);
            _context.Team.Remove(teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(int id)
        {
            return _context.Team.Any(e => e.TeamID == id);
        }
    }
}
