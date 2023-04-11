using MediatR;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Entities.Movies;

namespace Wifi.SD.Core.Application.Movies.Queries
{
    public class GetMovieDtosQuery : IRequest<IEnumerable<MovieDto>>
    {
        public int? GenreId { get; set; }
        public string? MediumTypeCode { get; set;}

        public List<Ratings>? Ratings { get; set; }

        public int Take { get; set; } // Pagging
        public int Skip { get; set; }
    }
}
