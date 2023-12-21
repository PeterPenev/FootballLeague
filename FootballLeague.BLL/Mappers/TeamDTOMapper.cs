using FootballLeague.BLL.DTOs;
using FootballLeague.DAL.Models;

namespace FootballLeague.BLL.Mappers
{
    static class TeamDTOMapper
    {
        public static TeamDTO ToDTO(this Team entity)
        {
            if (entity is null)
            {
                return null;
            }

            var dto = new TeamDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Points = entity.Points.ToString(),
                GoalFor = entity.GoalFor.ToString(),
                GoalAgainst = entity.GoalAgainst.ToString()
            };

            return dto;
        }
    }
}
