using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameApi.Models;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGame = new List<VideoGame>()
        {
            new VideoGame
            {
                Id = 1,
                Title = "Spider-Man 2",
                Platform = "PS5",
                Developer = "Insomniac Games",
                Publisher = "Sony Interactive Entertainment",
            },
            new VideoGame
            {
                Id = 2,
                Title = "The Legend of Zelda: Tears of the Kingdom",
                Platform = "Nintendo Switch",
                Developer = "Nintendo EPD",
                Publisher = "Nintendo",
            },
            new VideoGame
            {
                Id = 3,
                Title = "Elden Ring",
                Platform = "Multi-platform",
                Developer = "FromSoftware",
                Publisher = "Bandai Namco Entertainment",
            },
            new VideoGame
            {
                Id = 4,
                Title = "God of War Ragnarök",
                Platform = "PS5, PS4",
                Developer = "Santa Monica Studio",
                Publisher = "Sony Interactive Entertainment",
            },
        };
        [HttpGet]
        public ActionResult<List<VideoGame>> GetAllVideoGames()
        {
            return Ok(videoGame);
        }
        [HttpGet("{id}")]
        public ActionResult<VideoGame> GetVideoGameById(int id)
        {
            var game = videoGame.FirstOrDefault(vg => vg.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
    }
}
