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

        [HttpGet("/hello")]
        public IActionResult Hello()
        {
            return Ok("Hello!");
        }

        [HttpGet("/get-all-scores")]
        public IActionResult GetAllScores()
        {
            List<ScoreEntity> scores = [];

            ConnectionService connection = new();
            scores = connection.GetAllScores();

            return Ok(scores);
        }

    }
}
