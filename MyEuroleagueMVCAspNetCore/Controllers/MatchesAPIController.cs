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
    public class MatchesAPIController : Controller
    {
        private readonly Euroleague2020_21ASPDBContext _context;

        public MatchesAPIController(Euroleague2020_21ASPDBContext context)
        {
            _context = context;
        }

        // GET: MatchesAPI
        public async Task<IActionResult> Index(int searchRound)
        {
            var indexMatches = await _context.Match.ToListAsync();
            if (searchRound>0) {
                indexMatches = indexMatches.Where(x => x.RoundNo == searchRound).OrderBy(y=>y.MatchID).ToList();
            }
            return View(indexMatches);
        }

        // GET: MatchesAPI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Match
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // GET: MatchesAPI/Create
        public IActionResult Create()
        {
            var list = new List<TeamUser>();
            var playerNames = _context.Team.OrderBy(y => y.Name).Select(x => x.Name).ToList();
            foreach (var item in playerNames)
            {
                list.Add(new TeamUser
                {
                    Key = item,
                    Display = item
                });
            }
            var model = new Matches();
            model.TeamsList = new SelectList(list, "Key", "Display");
            return View(model);
        }

        // POST: MatchesAPI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchID,RoundNo,Home_Team,Away_Team,HomePointsScored,AwayPointsScored,HadExtraTime,EndOfFourthPeriodPoints")] Matches matches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matches);
        }

        // GET: MatchesAPI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Match.FindAsync(id);
            if (matches == null)
            {
                return NotFound();
            }
            return View(matches);
        }

        // POST: MatchesAPI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchID,RoundNo,Home_Team,Away_Team,HomePointsScored,AwayPointsScored,HadExtraTime,EndOfFourthPeriodPoints")] Matches matches)
        {
            if (id != matches.MatchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchesExists(matches.MatchID))
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
            return View(matches);
        }

        // GET: MatchesAPI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Match
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // POST: MatchesAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matches = await _context.Match.FindAsync(id);
            _context.Match.Remove(matches);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchesExists(int id)
        {
            return _context.Match.Any(e => e.MatchID == id);
        }
    }
}
