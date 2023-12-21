using FootballLeague.BLL.DTOs;
using FootballLeague.DAL.Models;

namespace FootballLeague.BLL.Mappers
{
    static class MatchDTOMapper
    {
        public static MatchDTO ToDTO(this Match entity)
        {
            if (entity is null)
            {
                return null;
            }

            var dto = new MatchDTO()
            {
                Id = entity.Id,
                TeamName1 = entity.Team1?.Name,
                TeamName2 = entity.Team2?.Name,
                GoalTeam1 = entity.GoalTeam1,
                GoalTeam2 = entity.GoalTeam2
            };

            return dto;
        }
    }
}
