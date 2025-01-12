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

    }
}
