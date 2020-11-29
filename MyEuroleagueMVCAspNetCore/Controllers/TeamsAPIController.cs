using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEuroleagueMVCAspNetCore.Models;

namespace MyEuroleagueMVCAspNetCore.Controllers
{
    public class TeamsAPIController : Controller
    {
        private readonly Euroleague2020_21ASPDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private  string wwwRootPath ;
        public TeamsAPIController(Euroleague2020_21ASPDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
            this.wwwRootPath = _hostEnvironment.WebRootPath;
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
        public async Task<IActionResult> Create([Bind("TeamID,Name,Country,Coach,ImageFileTeamLogo")] Teams teams)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(teams.ImageFileTeamLogo.FileName);
                string extension = Path.GetExtension(teams.ImageFileTeamLogo.FileName);
                teams.TeamLogoImageName = fileName + DateTime.Now.ToString("yyyymmddhhmmssfff") +extension;
                string path = Path.Combine(this.wwwRootPath + "/Image/", teams.TeamLogoImageName);
                using (var fileStream=new FileStream(path, FileMode.Create)) {
                    await teams.ImageFileTeamLogo.CopyToAsync(fileStream);
                }

                    _context.Add(teams);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("IndexTeams"))
                    {
                        return View("UniqueTeams", teams);
                    }
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("TeamID,Name,Country,Coach,ImageFileTeamLogo")] Teams teams)
        {
            if (id != teams.TeamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(teams.ImageFileTeamLogo.FileName);
                    string extension = Path.GetExtension(teams.ImageFileTeamLogo.FileName);
                    teams.TeamLogoImageName = fileName + DateTime.Now.ToString("yyyymmddhhmmssfff") + extension;
                    string path = Path.Combine(this.wwwRootPath + "/Image/", teams.TeamLogoImageName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await teams.ImageFileTeamLogo.CopyToAsync(fileStream);
                    }              

                    _context.Update(teams);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("IndexTeams"))
                        {
                            return View("UniqueTeams", teams);
                        }
                    }
                   
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
