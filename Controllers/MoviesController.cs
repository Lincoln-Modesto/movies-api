
using EFCoreSqlServer.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movies_api.Contracts;
using movies_api.Entities;
using movies_api.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EFCoreSqlServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(await _moviesService.GetMovies());
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validGenders = await _moviesService.GetGenders();

            if (!movieViewModel.IsValid(validGenders))
            {
                ModelState.AddModelError("GenderId", "Gênero inválido.");
                return BadRequest(ModelState);
            }

            return Ok(await _moviesService.CreateMovie(movieViewModel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> UpdateMovie(int id, [FromBody] MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = await _moviesService.GetMoviesById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(await _moviesService.UpdateMovie(movieViewModel, movie));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteMovie(int id)
        {
            var movie = await _moviesService.GetMoviesById(id);

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(await _moviesService.DeleteMovie(id, movie));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMultipleMovies([FromBody] List<int> movieIds)
        {
            if (movieIds == null || !movieIds.Any())
            {
                return BadRequest("Nenhum ID de filme fornecido.");
            }

            var moviesToDelete = await _moviesService.GetMoviesByListIds(movieIds);

            if (!moviesToDelete.Any())
            {
                return NotFound("Nenhum filme encontrado com os IDs fornecidos.");
            }

            return Ok(await _moviesService.DeleteMultipleMovies(moviesToDelete));
        }
    }
}