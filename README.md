When the project is setup locally, following script has to be executed on database

#######Start of SQL script#######

alter table Teams
  add constraint Points
      check (Points >= 0)

alter table Teams
  add constraint GoalFor
      check (GoalFor>= 0)

alter table Teams
  add constraint GoalAgainst
      check (GoalAgainst >= 0)

alter table Matches
  add constraint GoalTeam1
      check (GoalTeam1>= 0)

alter table Matches
  add constraint GoalTeam2
      check (GoalTeam2>= 0)


alter table Matches
add check (Team1Id!= Team2Id)

create unique index index_matches_team1id_team2id
on Matches (Team1Id, Team2Id);

alter index IX_Matches_Team1Id on Matches set ( IGNORE_DUP_KEY = OFF ) 
drop index IX_Matches_Team1Id on Matches
create index IX_Matches_Team1Id on Matches(Team1Id)

alter index IX_Matches_Team2Id on Matches set ( IGNORE_DUP_KEY = OFF )
drop index IX_Matches_Team2Id on Matches
create index IX_Matches_Team2Id on Matches(Team2Id)

#######End of SQL script#######
