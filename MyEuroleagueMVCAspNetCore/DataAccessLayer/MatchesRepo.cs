using MyEuroleagueMVCAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace MyEuroleagueMVCAspNetCore.DataAccessLayer
{
    public class MatchesRepo : IMatchesRepo
    {
        private readonly Euroleague2020_21ASPDBContext _context;

        public MatchesRepo(Euroleague2020_21ASPDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Matches> GetAppMatches()
        {
            return _context.Match.ToList();
        }

        public Matches GetMatchesByRoundId(int RoundId)
        {
            return _context.Match.FirstOrDefault(p => p.RoundNo == RoundId);
        }
        public IEnumerable<Matches> GetMatchesBeUntilRoundId(int RoundId)
        {
            return _context.Match.Where(e => e.RoundNo <= RoundId);
        }

        public Standings populateStanding(Standings teamsInStand, Matches matchItem, bool isHomeTeam)
        {
            if (matchItem.HadExtraTime)
            {
                teamsInStand.ExtraTimeMatches += 1;
                teamsInStand.PointsPlus += Convert.ToInt32(matchItem.EndOfFourthPeriodPoints == null ? 0 : matchItem.EndOfFourthPeriodPoints);
                teamsInStand.PointsMinus += Convert.ToInt32(matchItem.EndOfFourthPeriodPoints == null ? 0 : matchItem.EndOfFourthPeriodPoints);

                if (isHomeTeam ? (matchItem.HomePointsScored > matchItem.AwayPointsScored) : (matchItem.AwayPointsScored > matchItem.HomePointsScored))
                {
                    teamsInStand.Wins += 1;
                    //teamsInStand.PointsDif += isHomeTeam?(matchItem.HomePointsScored - matchItem.AwayPointsScored): (matchItem.AwayPointsScored - matchItem.HomePointsScored);
                }
                else
                {
                    teamsInStand.Loses += 1;
                    // teamsInStand.PointsDif += isHomeTeam ? (matchItem.HomePointsScored - matchItem.AwayPointsScored) : (matchItem.AwayPointsScored - matchItem.HomePointsScored);
                }
                //teamsInStand.PointsPlus += isHomeTeam ? matchItem.HomePointsScored: matchItem.AwayPointsScored;
                //teamsInStand.PointsMinus += isHomeTeam ? matchItem.AwayPointsScored: matchItem.HomePointsScored;                              
            }
            else
            {
                teamsInStand.PointsPlus += isHomeTeam ? matchItem.HomePointsScored : matchItem.AwayPointsScored;
                teamsInStand.PointsMinus += isHomeTeam ? matchItem.AwayPointsScored : matchItem.HomePointsScored;
                if (isHomeTeam ? (matchItem.HomePointsScored > matchItem.AwayPointsScored) : (matchItem.AwayPointsScored > matchItem.HomePointsScored))
                {
                    teamsInStand.Wins += 1;
                    teamsInStand.PointsDif += isHomeTeam ? (matchItem.HomePointsScored - matchItem.AwayPointsScored) : (matchItem.AwayPointsScored - matchItem.HomePointsScored);
                }
                else
                {
                    teamsInStand.Loses += 1;
                    teamsInStand.PointsDif += isHomeTeam ? (matchItem.HomePointsScored - matchItem.AwayPointsScored) : (matchItem.AwayPointsScored - matchItem.HomePointsScored);
                }
            }
            teamsInStand.MatchesNo += 1;
            return teamsInStand;
        }

        public List<Standings> teamsInStandings()
        {
            List<string> dbTeams = new List<string>();
            dbTeams.AddRange(_context.Team.Select(x => x.Name).ToList());
            int PositionNum = 0;
            List<Standings> teamsStandings = new List<Standings>();
            foreach (string item in dbTeams)
            {
                teamsStandings.Add(new Standings()
                {
                    PositionNo = ++PositionNum,
                    MatchesNo = 0,
                    TeamName = item,
                    Wins = 0,
                    Loses = 0,
                    PointsPlus = 0,
                    PointsMinus = 0,
                    PointsDif = 0,
                    ExtraTimeMatches = 0
                });              
            }
            return teamsStandings;
  
        }

        public string GetImageTeamName(string teamName)
        {
            return _context.Team.Where(x => x.Name == teamName).First().TeamLogoImageName;
        }
    }
}
