using FootballLeague.BLL.DTOs;

namespace FootballLeague.Tests.HelperMethods
{
    public class TestHelperTeamsController
    {
        public static TeamDTO TeamDTO01()
        {
            return new TeamDTO
            {
                Id = 1,
                Name = "ABCDE",
                Points = "0",
                GoalFor = "1",
                GoalAgainst = "2"
            };
        }

        public static TeamDTO TeamDTO02()
        {
            return new TeamDTO
            {
                Id = 2,
                Name = "aaa",
                Points = "1",
                GoalFor = "2",
                GoalAgainst = "2"
            };
        }
    }
}
