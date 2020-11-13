﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        [DisplayName("Round#")]
        public int RoundNo { get; set; }
        [MaxLength(50)]
        [DisplayName("Home Team")]
        public string Home_Team { get; set; }
        [MaxLength(50)]
        [DisplayName("Away Team")]
        public string Away_Team { get; set; }
        [DisplayName("Home Points Scored")]
        public int HomePointsScored { get; set; }
        [DisplayName("Away Points Scored")]
        public int AwayPointsScored { get; set; }
        [DisplayName("Had Extra Time")]
        public Boolean HadExtraTime { get; set; }
        [DisplayName("End Of Fourth Period Points")]
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
