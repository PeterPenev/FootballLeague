using FootballLeague.DAL.Models;

namespace FootballLeague.Tests.HelperMethods
{
    public class TestHelperMatchRepository
    {
        public static Match Match01()
        {
            return new Match
            {
                Team1Id = 1, 
                Team2Id = 2,
                GoalTeam1 = 2,
                GoalTeam2 = 0,
            };
        }

        public static Match Match02()
        {
            return new Match
            {
                Team1Id = 1,
                Team2Id = 3,
                GoalTeam1 = 3,
                GoalTeam2 = 0,
            };
        }

        public static Match Match03()
        {
            return new Match
            {
                Team1Id = 2,
                Team2Id = 3,
                GoalTeam1 = 1,
                GoalTeam2 = 1,
            };
        }
    }
}
