using FootballLeague.BLL.Contracts;
using FootballLeague.BLL.Services;
using FootballLeague.DAL.Contracts;
using FootballLeague.DAL;
using FootballLeague.Tests.HelperMethods;
using FootballLeague.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using FootballLeague.BLL.CustomExeptions;
using FootballLeague.BLL.Constants;

namespace FootballLeague.Tests.Services.TeamServicesTests
{
    [TestClass]
    public class UpdateTeamAsync_Should
    {
        [TestMethod]
        public async Task ThrowsExeptionWhen_UpdateTeamAsync()
        {
            var options = TestUtils.GetOptions(nameof(ThrowsExeptionWhen_UpdateTeamAsync));
            
            using (var assertContext = new FootballLeagueContext(options))
            {
                var mockedTeamRepository = new Mock<ITeamRepository>();
                var mockedMatchService = new Mock<IMatchService>();

                var sut = new TeamService(mockedTeamRepository.Object, mockedMatchService.Object);             

                var ex = await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.UpdateTeamAsync(TestHelperTeamService.Team01()));

                Assert.AreEqual(ex.Message, string.Format($"{CRUD.Update} {TestHelperTeamService.Team01().Name} {Errors.CRUDNotFound}"));
            }
        }

    }
}
