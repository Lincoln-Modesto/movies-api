﻿
using EFCoreSqlServer.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movies_api.Contracts;
using movies_api.DTO;
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
        //Camada de services para conectar-se ao dbContext e trabalhar nas regras de negócio
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            return Ok(await _moviesService.GetMovies());
        }

        //Poderia ficar numa controller única para gêneros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            return Ok(await _moviesService.GetGenders());
        }

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> CreateMovie([FromBody] MovieViewModel movieViewModel)
        {
            //Verifica se o model passado no body da requisição é válido
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validGenders = await _moviesService.GetGenders();

            //Verifica se o gênero passado para a criação do filme é um gênero válido
            if (!movieViewModel.IsValid(validGenders))
            {
                ModelState.AddModelError("GenderId", "Gênero inválido.");
                return BadRequest(ModelState);
            }

            return Ok(await _moviesService.CreateMovie(movieViewModel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDTO>> UpdateMovie(int id, [FromBody] MovieViewModel movieViewModel)
        {
            //Verifica se o model passado no body da requisição é válido
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

        [HttpDelete]
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