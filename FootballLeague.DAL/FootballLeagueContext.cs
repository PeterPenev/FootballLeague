using FootballLeague.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.DAL
{
    public class FootballLeagueContext : DbContext
    {
        public FootballLeagueContext()
        { }

        public FootballLeagueContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().HasOne(m => m.Team1)
            .WithOne()
            .HasForeignKey<Match>(m => m.Team1Id)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>().HasOne(m => m.Team2)
           .WithOne()
           .HasForeignKey<Match>(m => m.Team2Id)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
