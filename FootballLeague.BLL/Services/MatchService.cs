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
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;

        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository)
        {
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
        }

        public async Task<Match> CreateMatchAsync(Match matchForCreate)
        {
            bool isMachtValid = await this.IsMatchValid(matchForCreate, CRUD.Create);

            if (!isMachtValid)
            {
                throw new CRUDException($"({CRUD.Create} Match {matchForCreate.Id} {Errors.MatchIsNotValid})");
            }

            var matchForCRUDOperation = new Match();

            matchForCRUDOperation = await this.PrepareMatchForCRUDOperationsAsync(matchForCRUDOperation, matchForCreate);

            try
            {
                await this.matchRepository.AddAsync(matchForCRUDOperation);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{CRUD.Create} {matchForCRUDOperation.Team1} {matchForCRUDOperation.Team2} {ex.Message}");
            }

            try
            {
                await this.UpdateRankingTable(matchForCRUDOperation, CRUD.Create);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{Errors.CRUDUpdateRankingTable} {matchForCRUDOperation.Team1} {matchForCRUDOperation.Team2} {ex.Message}");
            }

            var createdMatch = await this.matchRepository.GetMatchByTeamId1AndTeamId2Async(matchForCRUDOperation.Team1Id, matchForCRUDOperation.Team2Id);

            if (createdMatch == null)
            {
                throw new CRUDException($"{Errors.GetMatch} {matchForCRUDOperation.Team1} {matchForCRUDOperation.Team2}");
            }

            return createdMatch;
        }

        public async Task<Match> DeleteMatchAsync(Match matchForDelete)
        {
            bool isMachtValid = await this.IsMatchValid(matchForDelete, CRUD.Delete);

            if (!isMachtValid)
            {
                throw new CRUDException($"{Errors.MatchIsNotValid} {matchForDelete.Team1} {matchForDelete.Team2}");
            }

            var matchForCRUDOperation = await this.matchRepository.GetByIdAsync(matchForDelete.Id);

            try
            {
                try
                {
                    await this.UpdateRankingTable(matchForCRUDOperation, CRUD.Delete);
                }
                catch (Exception ex)
                {
                    throw new CRUDException($"{Errors.CRUDUpdateRankingTable} {matchForCRUDOperation.Team1} {matchForCRUDOperation.Team2} {ex.Message}");
                }

                await this.matchRepository.Remove(matchForCRUDOperation);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{Errors.CRUDUpdateRankingTable} {matchForCRUDOperation.Team1} {matchForCRUDOperation.Team2} {ex.Message}");
            }

            return matchForDelete;
        }

        public async Task<Match> UpdateMatchAsync(Match matchForUpdate)
        {
            bool isMachtValid = await this.IsMatchValid(matchForUpdate, CRUD.Update);

            if (!isMachtValid)
            {
                throw new CRUDException($"({CRUD.Update} Match {matchForUpdate.Id} {Errors.MatchIsNotValid})");
            }

            var matchForCRUDOperation = await this.matchRepository.GetByIdAsync(matchForUpdate.Id);

            var matchForInsert = new Match();

            matchForInsert = await this.PrepareMatchForCRUDOperationsAsync(matchForInsert, matchForUpdate);

            try
            {
                await this.DeleteMatchAsync(matchForCRUDOperation);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{CRUD.Delete} {matchForCRUDOperation.Team1} {matchForCRUDOperation.Team2} {ex.Message}");
            }

            try
            {
                await this.CreateMatchAsync(matchForInsert);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{CRUD.Create} {matchForInsert.Team1} {matchForInsert.Team2} {ex.Message}");
            }

            return matchForUpdate;
        }

        public Task<IEnumerable<Match>> GetMatchesByTeamId(int teamId)
        {
            return this.matchRepository.GetAllMatchesByTeamIdAsync(teamId);
        }

        public async Task<IEnumerable<Match>> GetAllMatchesAsync()
        {
            var matches = await this.matchRepository.GetAllAsync();

            return matches;
        }

        public async Task<IEnumerable<MatchDTO>> GetAllMatchesDTOAsync()
        {
            var matches = await this.matchRepository.GetAllAsync();
            var teams = await this.teamRepository.GetAllAsync();

            foreach (var match in matches)
            {
                var team1Id = match.Team1Id;
                var team2Id = match.Team2Id;

                var team1 = await this.teamRepository.GetByIdAsync(team1Id);
                var team2 = await this.teamRepository.GetByIdAsync(team2Id);

                match.Team1 = team1;
                match.Team2 = team2;
            }

            var matchDTOs = matches.Select(m => m.ToDTO())
                          .ToList();

            return matchDTOs;
        }        

        private async Task<bool> IsMatchValid(Match match, string crudOperation)
        {
            var isMatchExisting = await this.matchRepository.GetByIdAsync(match.Id);

            if (isMatchExisting != null && crudOperation == CRUD.Create)
            {
                throw new AlreadyExistsException($"(Match {match.Id} exists! {crudOperation} is not possible!)");
            }

            if (isMatchExisting == null && crudOperation != CRUD.Create)
            {
                throw new NotFoundException($"(Match {match.Id} is not found! {crudOperation} is not possible!)");
            }

            var isTeam1Existing = await this.teamRepository.GetByIdAsync(match.Team1Id);

            if (isTeam1Existing == null)
            {
                throw new NotFoundException($"(Team {match.Team1Id} in Match {match.Id} is not found! {crudOperation} match is not possible!)");
            }

            var isTeam2Existing = await this.teamRepository.GetByIdAsync(match.Team2Id);

            if (isTeam2Existing == null)
            {
                throw new NotFoundException($"(Team {match.Team2Id} in Match {match.Id} is not found! {crudOperation} match is not possible!)");
            }

            return true;
        }

        private async Task UpdateRankingTable(Match matchToHandle, string crudOperation)
        {
            var team1Id = matchToHandle.Team1Id;
            var team2Id = matchToHandle.Team2Id;
            var goalTeam1 = matchToHandle.GoalTeam1;
            var goalTeam2 = matchToHandle.GoalTeam2;

            var team1 = await this.teamRepository.GetByIdAsync(team1Id);
            var team2 = await this.teamRepository.GetByIdAsync(team2Id);

            if (crudOperation == CRUD.Create)
            {
                team1.GoalFor += goalTeam1;
                team1.GoalAgainst += goalTeam2;

                team2.GoalFor += goalTeam2;
                team2.GoalAgainst += goalTeam1;

                if (goalTeam1 > goalTeam2)
                {
                    team1.Points += (ushort)(Enum.PointsCode.Win);
                }
                else if (goalTeam1 < goalTeam2)
                {
                    team2.Points += (ushort)(Enum.PointsCode.Win);
                }
                else
                {
                    team1.Points += (ushort)(Enum.PointsCode.Draw);
                    team2.Points += (ushort)(Enum.PointsCode.Draw);
                }
            }
            else if (crudOperation == CRUD.Delete)
            {
                team1.GoalFor -= goalTeam1;
                team1.GoalAgainst -= goalTeam2;

                team2.GoalFor -= goalTeam2;
                team2.GoalAgainst -= goalTeam1;

                if (goalTeam1 > goalTeam2)
                {
                    team1.Points -= (ushort)(Enum.PointsCode.Win);
                }
                else if (goalTeam1 < goalTeam2)
                {
                    team2.Points -= (ushort)(Enum.PointsCode.Win);
                }
                else
                {
                    team1.Points -= (ushort)(Enum.PointsCode.Draw);
                    team2.Points -= (ushort)(Enum.PointsCode.Draw);
                }
            }

            try
            {
                await this.teamRepository.Update(team1);
                await this.teamRepository.Update(team2);
            }
            catch (Exception ex)
            {
                throw new CRUDException($"{CRUD.Update} {team1.Name} {team2.Name} {ex.Message}");
            }
        }

        private async Task<Match> PrepareMatchForCRUDOperationsAsync(Match matchForCRUDOperation, Match match)
        {
            var matchCRUDOperations = matchForCRUDOperation;

            matchCRUDOperations.Team1Id = match.Team1Id;
            matchCRUDOperations.Team2Id = match.Team2Id;
            matchCRUDOperations.GoalTeam1 = match.GoalTeam1;
            matchCRUDOperations.GoalTeam2 = match.GoalTeam2;

            return matchCRUDOperations;
        }
    }
}
