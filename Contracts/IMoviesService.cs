using movies_api.DTO;
using movies_api.Entities;
using movies_api.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace movies_api.Contracts
{
    public interface IMoviesService
    {
        Task<IEnumerable<MovieDTO>> GetMovies();
        Task<Movie> GetMoviesById(int id);
        Task<List<Movie>> GetMoviesByListIds(List<int> movieIds);
        //Poderia ficar nos contracts específicos para gêneros
        Task<List<Gender>> GetGenders();
        Task<MovieDTO> CreateMovie(MovieViewModel movieViewModel);
        Task<MovieDTO> UpdateMovie(MovieViewModel movieViewModel, Movie movie);
        Task<string> DeleteMovie(int id, Movie movie);
        Task<string> DeleteMultipleMovies(List<Movie> moviesToDelete);
    }
}
