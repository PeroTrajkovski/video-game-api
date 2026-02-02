using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Models;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAllVideoGames()
        {
            var game = await _context.VideoGames.ToListAsync();
            return Ok(game);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoGameById(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
        [HttpPost]
        public async Task<IActionResult> AddVideoGame(VideoGame newGame)
        {
            if (newGame == null)
            {
                return BadRequest();
            }
            _context.VideoGames.Add(newGame);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, VideoGame updateVideoGame)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            game.Title = updateVideoGame.Title;
            game.Platform = updateVideoGame.Platform;
            game.Developer = updateVideoGame.Developer;
            game.Publisher = updateVideoGame.Publisher;

            await _context.SaveChangesAsync();
            return Ok(game);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var game = _context.VideoGames.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();
            return Ok($"{game.Title} is deleted successfully");
        }
    }
}
