using Microsoft.AspNetCore.Mvc;
using MyEuroleagueMVCAspNetCore.DataAccessLayer;
using MyEuroleagueMVCAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StandingsAPIController : Controller
    {
        private readonly IMatchesRepo _repository;
        public StandingsAPIController(IMatchesRepo repository)
        {
            _repository = repository;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Match.ToListAsync());
        //}


        // GET: StandingsAPI
        [HttpGet]
        public ActionResult<IEnumerable<Standings>> GetOverallStandings()
        {
            List<Standings> standings = new List<Standings>();
            List<Standings> standingsDynamically = new List<Standings>();
            standingsDynamically.AddRange(_repository.teamsInStandings());
            var allMatches = _repository.GetAppMatches();
           // return populateStandings(standingsDynamically, allMatches);
            return View(populateStandings(standingsDynamically, allMatches));
        }
        // GET: StandingsAPI/5
        [HttpGet("{RoundId}")]
        public ActionResult<IEnumerable<Standings>> GetStandingsByRound(int RoundId)
        {
            List<Standings> standingsDynamically = new List<Standings>();
            standingsDynamically.AddRange(_repository.teamsInStandings());
            var allMatchesUntilRoundID = _repository.GetMatchesBeUntilRoundId(RoundId);
            //return populateStandings(standingsDynamically, allMatchesUntilRoundID);
            return View(populateStandings(standingsDynamically, allMatchesUntilRoundID));
        }

        List<Standings> populateStandings(List<Standings> standingsDynamically,IEnumerable<Matches> allMatches) {
            List<Standings> standings = new List<Standings>();
            foreach (var matchItem in allMatches)
            {
                foreach (var teamsInStand in standingsDynamically)
                {
                    if (teamsInStand.TeamName.Trim() == matchItem.Home_Team.Trim())
                    {
                        var TeamNameExist = standings.Find(x => x.TeamName.Contains(teamsInStand.TeamName.Trim()));
                        Standings populateStandingsHome = _repository.populateStanding((TeamNameExist != null) ? TeamNameExist : teamsInStand, matchItem, true);
                        if (TeamNameExist != null)
                        {
                            var itemToRemove = standings.Single(r => r.TeamName == teamsInStand.TeamName.Trim());
                            standings.Remove(itemToRemove);
                        }
                        standings.Add(populateStandingsHome);
                    }
                    if (teamsInStand.TeamName.Trim() == matchItem.Away_Team.Trim())
                    {
                        var AwayTeamNameExist = standings.Find(x => x.TeamName.Contains(teamsInStand.TeamName.Trim()));
                        Standings populateStandingsAway = _repository.populateStanding((AwayTeamNameExist != null) ? AwayTeamNameExist : teamsInStand, matchItem, false);

                        if (AwayTeamNameExist != null)
                        {
                            var awayItemToRemove = standings.Single(r => r.TeamName == teamsInStand.TeamName.Trim());
                            standings.Remove(awayItemToRemove);
                        }
                        standings.Add(populateStandingsAway);
                    }
                    continue;
                }

            }
            int positionCnt = 0;
            var results = standings.OrderByDescending(p => p.PointsDif).OrderBy(m => m.MatchesNo).OrderByDescending(w => w.Wins).ToList();
            List<Standings> finalResults = new List<Standings>();
            foreach (var item in results)
            {
                item.PositionNo = positionCnt + 1;
                positionCnt += 1;
            }
            return results;
        }
    }
}
