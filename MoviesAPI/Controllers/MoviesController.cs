using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static readonly List<Movies> movies = new List<Movies>()
        {
            new Movies { MovieId = 1, MovieName = "Harry Potter", Genre = "Fantasy" },
            new Movies { MovieId = 2, MovieName = "Jurassic Park", Genre = "Scifi" },
            new Movies { MovieId = 3, MovieName = "Pirates of the Carribean", Genre = "Adventure" },
            new Movies { MovieId = 4, MovieName = "John Wick", Genre = "Action" },
            new Movies { MovieId = 5, MovieName = "Mr.Bean Holiday", Genre = "Comedy" }
        };

        [HttpGet(Name = "GetMovies")]
        public IEnumerable<Movies> Get()
        {
            return movies;
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public ActionResult<Movies> GetById(int id)
        {
            var movie = movies.FirstOrDefault(p => p.MovieId == id);

            if (movie == null)
            {
                return NotFound(); // 404 Not Found
            }

            return movie;
        }

        [HttpPost]
        public ActionResult<Movies> Post([FromBody] Movies newMovie)
        {
            newMovie.MovieId = movies.Max(p => p.MovieId) + 1; // Generate a new ID
            movies.Add(newMovie);

            return CreatedAtAction(nameof(GetById), new { id = newMovie.MovieId }, newMovie);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movies updatedMovie)
        {
            var existingMovie = movies.FirstOrDefault(p => p.MovieId == id);

            if (existingMovie == null)
            {
                return NotFound(); // 404 Not Found
            }

            existingMovie.MovieName = updatedMovie.MovieName;
            existingMovie.Genre = updatedMovie.Genre;

            return NoContent(); // 204 No Content
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movieToRemove = movies.FirstOrDefault(p => p.MovieId == id);

            if (movieToRemove == null)
            {
                return NotFound(); // 404 Not Found
            }

            movies.Remove(movieToRemove);

            return NoContent(); // 204 No Content
        }
    }
}
