namespace FootballLeague.BLL.DTOs
{
    public class MatchDTO
    {
        public int Id { get; set; }

        public string TeamName1 { get; set; }

        public string TeamName2 { get; set; }

        public ushort GoalTeam1 { get; set; }

        public ushort GoalTeam2 { get; set; }
    }
}
