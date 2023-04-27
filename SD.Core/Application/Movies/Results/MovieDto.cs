using Wifi.SD.Core.Entities.Movies;

namespace Wifi.SD.Core.Application.Movies.Results
{
    /// <summary>
    /// Movie Data Transaction Object
    /// </summary>

    public class MovieDto : MovieBase // 4.
    {
        // A navigation property is an optional property on an entity type that allows
        // for navigation from one end of an association to the other end.
        // Unlike other properties, navigation properties do not carry data.

        private string genreName;

        public string GenreName { get => this.genreName; }

        public static MovieDto MapFrom(Movie movie) // MovieBase verbraucht mehr Ressourcen, 
        {
            return new MovieDto()
            {
                Id = movie.Id,
                Title = movie.Title,
                GenreId = movie.GenreId,
                MediumTypeCode = movie.MediumTypeCode,
                Price = movie.Price,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                genreName = movie.Genre?.Name ?? string.Empty

            };
        }
    }

    
}
