using MediatR;
using Wifi.SD.Core.Application.Movies.Results;

namespace Wifi.SD.Core.Application.Movies.Commands
{
    public class DeleteMovieDtoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
