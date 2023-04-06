using MediatR;
using Wifi.SD.Core.Application.Movies.Results;

namespace Wifi.SD.Core.Application.Movies.Queries
{
    public class GetMovieDtoQuery : IRequest<MovieDto>
    {
        public Guid Id { get; set; }
    }
}
