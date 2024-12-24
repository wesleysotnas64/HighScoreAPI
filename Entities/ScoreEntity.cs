namespace HighScoreAPI.Entities
{
    public class ScoreEntity
    {
        public int Id { get; set; }
        public string? GameCode { get; set; }
        public string? PlayerName { get; set; }
        public int PlayerScore { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
