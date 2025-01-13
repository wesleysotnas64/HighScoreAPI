using HighScoreAPI.Entities;
using HighScoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HighScoreAPI.Controllers
{
    [ApiController]
    [Route("/highscore")]
    public class ScoreController : ControllerBase
    {
        public ScoreController() { }

        [HttpGet("/get-all-scores")]
        public IActionResult GetAllScores()
        {
            List<ScoreEntity> scores = [];

            ConnectionService connection = new();
            scores = connection.GetAllScores();

            return Ok(scores);
        }

        [HttpGet("/get-vegan-snake-scores")]
        public IActionResult GetVeganSnakeScores()
        {
            List<ScoreEntity> scores = [];

            ConnectionService connection = new();
            scores = connection.GetScoresByGameCode("VEGANSNAKE");

            return Ok(scores);
        }

        [HttpPost("/register-score")]
        public IActionResult RegisterScore([FromBody] ScoreEntity score)
        {
            if (score == null || string.IsNullOrWhiteSpace(score.GameCode) || string.IsNullOrWhiteSpace(score.PlayerName))
                return BadRequest("Invalid score data. GameCode and PlayerName are required.");

            ConnectionService connection = new();
            score.CreateTime = DateTime.Now;
            bool success = connection.AddScore(score);

            if (success)
                return Ok("Score registered successfully.");

            return StatusCode(500, "An error occurred while registering the score.");
        }

    }
}
