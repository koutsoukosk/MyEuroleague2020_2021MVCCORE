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
            //new Standings{ PositionNo =1,MatchesNo=0,TeamName= "Anadolu Efes Istanbul", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =2,MatchesNo=0,TeamName= "Real Madrid", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =3,MatchesNo=0,TeamName= "Barcelona", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =4,MatchesNo=0,TeamName= "CSKA Moscow", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =5,MatchesNo=0,TeamName= "Maccabi Tel Aviv", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =6,MatchesNo=0,TeamName= "Panathinaikos OPAP Athens", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =7,MatchesNo=0,TeamName= "Khimki Moscow", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =8,MatchesNo=0,TeamName= "Fenerbahce Beko Istanbul", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =9,MatchesNo=0,TeamName= "Zalgiris Kaunas", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =10,MatchesNo=0,TeamName= "Valencia Basket", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =11,MatchesNo=0,TeamName= "Olympiacos Piraeus", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =12,MatchesNo=0,TeamName= "AX Armani Exchange Milan", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =13,MatchesNo=0,TeamName= "TD Systems Baskonia Vitoria-Gasteiz", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =14,MatchesNo=0,TeamName= "Crvena Zvezda mts Belgrade", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //   new Standings{ PositionNo =15,MatchesNo=0,TeamName= "LDLC ASVEL Villeurbanne", Wins=0,Loses=0,PointsPlus=0,
            //   PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //new Standings{ PositionNo =16,MatchesNo=0,TeamName= "ALBA Berlin", Wins=0,Loses=0,PointsPlus=0,
            //PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //new Standings{ PositionNo =17,MatchesNo=0,TeamName= "Bayern Munich", Wins=0,Loses=0,PointsPlus=0,
            //PointsMinus=0,PointsDif=0,ExtraTimeMatches=0},
            //new Standings{ PositionNo =18,MatchesNo=0,TeamName= "Zenit St Petersburg", Wins=0,Loses=0,PointsPlus=0,
            //PointsMinus=0,PointsDif=0,ExtraTimeMatches=0}

        }
    }
}
