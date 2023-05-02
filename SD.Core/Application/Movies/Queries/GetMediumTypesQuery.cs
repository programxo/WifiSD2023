using MediatR;
using Wifi.SD.Core.Entities.Movies;

namespace Wifi.SD.Core.Application.Movies.Queries
{
    public class GetMediumTypesQuery : IRequest<IEnumerable<MediumType>>
    {
    }
}
