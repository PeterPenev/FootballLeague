[FootballLeague.postman_collection.json](https://github.com/PeterPenev/FootballLeague/files/13746269/FootballLeague.postman_collection.json)# FootballLeague

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

#######Start Test Utils#######
INSERT INTO Teams (Name, Points, GoalFor, GoalAgainst)
VALUES 
('Team1', '0', '0', '0'),
('Team2', '0', '0', '0'),
('Team3', '0', '0', '0');

INSERT INTO Matches (Team1Id, Team2Id, GoalTeam1, GoalTeam2)
VALUES 
('1', '2', '3', '0'),
('1', '3', '2', '1');

[Uploading Foot{
	"info": {
		"_postman_id": "7d76db4d-e8d2-4617-a784-c9e872837eac",
		"name": "FootballLeague",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "14194018"
	},
	"item": [
		{
			"name": "https://localhost:44349/api/AddTeam",
			"request": {
				"method": "POST",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Id\":9,\"Name\":\"abc1\",\"Points\":\"100\",\"GoalFor\":\"20\",\"GoalAgainst\":\"10\"}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/AddTeam",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"AddTeam"
					]
				}
			},
			"response": []
		},
		{
			"name": "https://localhost:44349/api/GetAllTeams",
			"protocolProfileBehavior": {
				"disableBodyPruning": true
			},
			"request": {
				"method": "GET",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Id\":1,\"Name\":\"Team1\",\"Points\":\"6\",\"GoalFor\":\"5\",\"GoalAgainst\":\"1\"}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/GetAllTeams",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"GetAllTeams"
					]
				}
			},
			"response": []
		},
		{
			"name": "https://localhost:44349/api/UpdateTeam",
			"request": {
				"method": "PUT",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Id\":1,\"Name\":\"Team1\",\"Points\":\"6\",\"GoalFor\":\"5\",\"GoalAgainst\":\"1\"}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/UpdateTeam",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"UpdateTeam"
					]
				}
			},
			"response": []
		},
		{
			"name": "https://localhost:44349/api/DeleteTeam",
			"request": {
				"method": "DELETE",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Id\":111,\"Name\":\"team4\",\"Points\":\"1\",\"GoalFor\":\"2\",\"GoalAgainst\":\"3\"}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/DeleteTeam",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"DeleteTeam"
					]
				}
			},
			"response": []
		},
		{
			"name": "https://localhost:44349/api/GetAllMatches",
			"protocolProfileBehavior": {
				"disableBodyPruning": true
			},
			"request": {
				"method": "GET",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Id\":1,\"Name\":\"Team1\",\"Points\":\"6\",\"GoalFor\":\"5\",\"GoalAgainst\":\"1\"}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/GetAllMatches",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"GetAllMatches"
					]
				}
			},
			"response": []
		},
		{
			"name": "https://localhost:44349/api/AddMatch",
			"request": {
				"method": "POST",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Team1Id\":2,\"Team1\":{\"Id\":1,\"Name\":\"Team1\",\"Points\":\"0\",\"GoalFor\":\"0\",\"GoalAgainst\":\"0\"},\"Team2Id\":3,\"Team2\":{\"Id\":2,\"Name\":\"Team2\",\"Points\":\"0\",\"GoalFor\":\"0\",\"GoalAgainst\":\"0\"},\"GoalTeam1\":1,\"GoalTeam2\":1,\"Id\":15}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/AddMatch",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"AddMatch"
					]
				}
			},
			"response": []
		},
		{
			"name": "https://localhost:44349/api/UpdateMatch",
			"request": {
				"method": "PUT",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Team1Id\":2,\"Team1\":{\"Id\":1,\"Name\":\"Team1\",\"Points\":\"0\",\"GoalFor\":\"0\",\"GoalAgainst\":\"0\"},\"Team2Id\":3,\"Team2\":{\"Id\":2,\"Name\":\"Team2\",\"Points\":\"0\",\"GoalFor\":\"0\",\"GoalAgainst\":\"0\"},\"GoalTeam1\":2,\"GoalTeam2\":2,\"Id\":15}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/UpdateMatch",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"UpdateMatch"
					]
				}
			},
			"response": []
		},
		{
			"name": "https://localhost:44349/api/DeleteMatch",
			"request": {
				"method": "DELETE",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\"Team1Id\":2,\"Team1\":{\"Id\":1,\"Name\":\"Team1\",\"Points\":\"0\",\"GoalFor\":\"0\",\"GoalAgainst\":\"0\"},\"Team2Id\":3,\"Team2\":{\"Id\":2,\"Name\":\"Team2\",\"Points\":\"0\",\"GoalFor\":\"0\",\"GoalAgainst\":\"0\"},\"GoalTeam1\":1,\"GoalTeam2\":1,\"Id\":13}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:44349/api/DeleteMatch",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "44349",
					"path": [
						"api",
						"DeleteMatch"
					]
				}
			},
			"response": []
		}
	]
}ballLeague.postman_collection.json…]()

#######End Test Utils#######
