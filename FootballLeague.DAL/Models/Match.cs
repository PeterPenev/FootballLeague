using System;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.DAL.Models
{
    public class Match : EntityBase
    {        
        [Required]
        public int Team1Id { get; set; }

        [Required]
        public Team Team1 { get; set; }
        
        [Required]
        public int Team2Id { get; set; }

        [Required]
        public Team Team2 { get; set; }

        [Required]
        [Range(0, ushort.MaxValue, ErrorMessage = "Value of GoalTeam1 must be zero or integer number")]
        public ushort GoalTeam1 { get; set; }

        [Required]
        [Range(0, ushort.MaxValue, ErrorMessage = "Value of GoalTeam2 must be zero or integer number")]
        public ushort GoalTeam2 { get; set; }
    }
}
