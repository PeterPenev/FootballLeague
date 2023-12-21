using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.DAL.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Team : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, ushort.MaxValue, ErrorMessage = "Value of Points must be zero or integer number")]
        public ushort Points { get; set; }

        [Required]
        [Range(0, ushort.MaxValue, ErrorMessage = "Value of GoalFor must be zero or integer number")]
        public ushort GoalFor { get; set; }

        [Required]
        [Range(0, ushort.MaxValue, ErrorMessage = "Value of GoalAgainst must be zero or integer number")]
        public ushort GoalAgainst { get; set; }
    }
}
