using MediatR;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Entities.Movies;

namespace Wifi.SD.Core.Application.Movies.Queries
{
    public class GetGenresQuery : IRequest<IEnumerable<Genre>>
    {
    }
}
