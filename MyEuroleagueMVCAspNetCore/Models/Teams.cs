using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.Models
{
    public class Teams
    {
        [Key]
        [Required]
        public int TeamID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string Coach { get; set; }
    }
}
