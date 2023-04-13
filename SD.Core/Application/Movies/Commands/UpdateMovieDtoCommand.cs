using MediatR;
using Wifi.SD.Core.Application.Movies.Results;

namespace Wifi.SD.Core.Application.Movies.Commands
{
    public class UpdateMovieDtoCommand : IRequest<MovieDto>
    {
        public Guid Id { get; set; }
        public MovieDto MovieDto { get; set; }


    }
}
