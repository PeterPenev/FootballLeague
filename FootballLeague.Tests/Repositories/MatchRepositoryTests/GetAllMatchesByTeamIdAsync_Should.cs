using FootballLeague.DAL;
using FootballLeague.DAL.Repositories;
using FootballLeague.Tests.HelperMethods;
using FootballLeague.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Tests.Repositories.MatchRepositoryTests
{
    [TestClass]
    public class GetAllMatchesByTeamIdAsync_Should
    {
        [TestMethod]
        public async Task Succeed_GetAllMatchesByTeamIdAsync()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_GetAllMatchesByTeamIdAsync));

            using (var arrangeContext = new FootballLeagueContext(options))
            {
                await arrangeContext.Matches.AddAsync(TestHelperMatchRepository.Match01());
                await arrangeContext.Matches.AddAsync(TestHelperMatchRepository.Match02());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new FootballLeagueContext(options))
            {
                var sut = new MatchRepository(assertContext);

                var matches = await sut.GetAllMatchesByTeamIdAsync(1);

                Assert.AreEqual(matches.ToList().Count, 2);
            }
        }
    }
}
