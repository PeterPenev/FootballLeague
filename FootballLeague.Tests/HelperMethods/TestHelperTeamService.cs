using FootballLeague.DAL.Models;

namespace FootballLeague.Tests.HelperMethods
{
    public class TestHelperTeamService
    {
        public static Team Team01()
        {
            return new Team
            {
                Id = 1,
                Name = "ABCDE",
                Points = 0,
                GoalFor = 0,
                GoalAgainst = 0
            };
        }
    }
}
