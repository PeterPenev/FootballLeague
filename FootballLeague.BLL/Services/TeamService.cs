using FootballLeague.BLL.Constants;
using FootballLeague.BLL.Contracts;
using FootballLeague.BLL.CustomExeptions;
using FootballLeague.BLL.DTOs;
using FootballLeague.BLL.Mappers;
using FootballLeague.DAL.Contracts;
using FootballLeague.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.BLL.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository teamRepository;
        private readonly IMatchService matchService;

        public TeamService(ITeamRepository teamRepository, IMatchService matchService)
        {
            this.teamRepository = teamRepository;
            this.matchService = matchService;
        }

        public async Task<IEnumerable<TeamDTO>> GetAllTeamsDTOAsync()
        {
            var teams = await this.teamRepository.GetAllAsync();

            var orderedTeamsDTOs = teams.OrderByDescending(t => t.Points)
                          .ThenByDescending(t => t.GoalFor - t.GoalAgainst)
                          .ThenByDescending(t => t.GoalFor)
                          .Select(t => t.ToDTO())
                          .ToList();

            return orderedTeamsDTOs;
        }

        public async Task<Team> CreateTeamAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception($"{name} {Errors.CRUDIsNullOrEmpty}");
            }

            var teamExists = await this.teamRepository.GetTeamByNameAsync(name);

            if (teamExists != null)
            {
                throw new Exception($"{name} {Errors.CRUDAlreadyExists}");
            }

            var team = new Team();
            team.Name = name;

            try
            {
                await this.teamRepository.AddAsync(team);
            }
            catch (Exception)
            {
                throw new CRUDException($"{CRUD.Create} {name} {Errors.CRUDIsNotPossible}");
            }

            var createdTeam = await this.teamRepository.GetTeamByNameAsync(team.Name);

            if (createdTeam == null)
            {
                throw new CRUDException($"{Errors.GetTeam} {team.Name}");
            }

            return createdTeam;
        }

        public async Task<Team> UpdateTeamAsync(Team teamWithNewData)
        {
            var isTeamForUpdateExisting = await this.teamRepository.GetByIdAsync(teamWithNewData.Id);

            if (isTeamForUpdateExisting == null)
            {
                throw new NotFoundException($"{CRUD.Update} {teamWithNewData.Name} {Errors.CRUDNotFound}");
            }

            try
            {
                await this.teamRepository.Update(teamWithNewData);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{CRUD.Update} {teamWithNewData.Name} {ex.Message}");
            }

            return teamWithNewData;
        }

        public async Task<Team> DeleteTeamAsync(Team teamForDelete)
        {
            var isTeamForDeleteExisting = await this.teamRepository.GetByIdAsync(teamForDelete.Id);

            if (isTeamForDeleteExisting == null)
            {
                throw new NotFoundException($"({teamForDelete.Name} {Errors.CRUDNotFound}");
            }

            var matchesTeamForDelete = await this.matchService.GetMatchesByTeamId(teamForDelete.Id);

            if (matchesTeamForDelete != null)
            {
                try
                {
                    foreach (var match in matchesTeamForDelete)
                    {
                        await this.matchService.DeleteMatchAsync(match);
                    }
                }
                catch (Exception ex)
                {
                    throw new CRUDException($"{CRUD.Delete} matches {ex.Message}");
                }
            }

            try
            {
                await this.teamRepository.Remove(teamForDelete);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{CRUD.Delete} {teamForDelete.Name} {ex.Message}");
            }

            return isTeamForDeleteExisting;
        }
    }
}
