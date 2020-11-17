using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.Models
{
    public class Matches
    {
        [Key]
        [Required]
        public int MatchID { get; set; }
        [Required]
        [DisplayName("Round#")]
        public int RoundNo { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Home Team")]
        public string Home_Team { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Away Team")]
        public string Away_Team { get; set; }
        [Required]
        [DisplayName("Home Points")]
        public int HomePointsScored { get; set; }
        [Required]
        [DisplayName("Away Points")]
        public int AwayPointsScored { get; set; }
        [DisplayName("Extra Time")]
        public Boolean HadExtraTime { get; set; }
        [DisplayName("Regular Period")]
        public int? EndOfFourthPeriodPoints { get; set; }
        [NotMapped]
        public SelectList TeamsList { get; set; }
    }
    public class TeamUser
    {
        public string Key { get; set; }
        public string Display { get; set; }
    }
}
