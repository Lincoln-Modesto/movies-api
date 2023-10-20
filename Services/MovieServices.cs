using EFCoreSqlServer.DataContext;
using Microsoft.EntityFrameworkCore;
using movies_api.Contracts;
using movies_api.DTO;
using movies_api.Entities;
using movies_api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movies_api.Services
{
    public class MoviesServices : IMoviesService
    {
        private readonly ApplicationDbContext _moviesDbContext;
        public MoviesServices(ApplicationDbContext moviesDbContext)
        {
            _moviesDbContext = moviesDbContext;
        }

        public async Task<IEnumerable<MovieDTO>> GetMovies()
        {
            var movies = await _moviesDbContext.Movies
                    .Include(movie => movie.Gender)
                    .Select(movie => new MovieDTO
                    {
                        MovieId = movie.MovieId,
                        Name = movie.Name,
                        GenderId = movie.GenderId,
                        GenderName = movie.Gender.Name,
                        Date = movie.Date,
                        Active = movie.Active == 1
                    })
                    .ToListAsync();

            return movies;
        }

        public async Task<List<Movie>> GetMoviesByListIds(List<int> movieIds)
        {
            var movies = await _moviesDbContext.Movies
                 .Where(movie => movieIds.Contains(movie.MovieId))
                 .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetMoviesById(int id)
        {
            var movie = await _moviesDbContext.Movies
                .Include(movie => movie.Gender)
                .FirstOrDefaultAsync(movie => movie.MovieId == id);

            if (movie == null)
            {
                return null;
            }

            return movie;
        }

        //Poderia ficar em uma camada de serviços apenas para os gêneros
        public async Task<List<Gender>> GetGenders()
        {
            var validGenres = await _moviesDbContext.Genders.ToListAsync();
            return validGenres;
        }

        public async Task<MovieDTO> CreateMovie(MovieViewModel movieViewModel)
        {
            var movie = new Movie
            {
                Name = movieViewModel.Name,
                GenderId = movieViewModel.GenderId,
                Date = DateTime.Now,
                Active = movieViewModel.Active == true ? 1 : 0,
            };

            await _moviesDbContext.Movies.AddAsync(movie);
            await _moviesDbContext.SaveChangesAsync();

            var movieDTO = new MovieDTO
            {
                MovieId = movie.MovieId,
                Name = movie.Name,
                GenderId = movie.GenderId,
                GenderName = movie.Gender.Name,
                Date = movie.Date,
                Active = movie.Active == 1
            };

            return movieDTO;
        }

        public async Task<MovieDTO> UpdateMovie(MovieViewModel movieViewModel, Movie movie)
        {
            movie.Name = movieViewModel.Name;
            movie.GenderId = movieViewModel.GenderId;
            movie.Active = movieViewModel.Active == true ? 1 : 0;
            movie.Gender = await _moviesDbContext.Genders.FindAsync(movie.GenderId);
            
            _moviesDbContext.Update(movie);
            await _moviesDbContext.SaveChangesAsync();

            var movieDTO = new MovieDTO
            {
                MovieId = movie.MovieId,
                Name = movie.Name,
                GenderId = movie.GenderId,
                GenderName = movie.Gender?.Name,
                Date = movie.Date,
                Active = movie.Active == 1
            };

            return movieDTO;
        }

        public async Task<string> DeleteMovie(int id, Movie movie)
        {
            _moviesDbContext.Remove(movie);
            await _moviesDbContext.SaveChangesAsync();
            return $"{id} - Filme excluído com sucesso!";
        }

        public async Task<string> DeleteMultipleMovies(List<Movie> moviesToDelete)
        {
            _moviesDbContext.Movies.RemoveRange(moviesToDelete);
            await _moviesDbContext.SaveChangesAsync();
            return "Filmes excluídos com sucesso.";
        }
    }
}
