using Microsoft.AspNetCore.Mvc;
using Wifi.SD.Core.Application.Movies.Queries;
using Wifi.SD.Core.Application.Movies.Results;

namespace SD.WS.Controllers
{
    [ApiController]
    [Route("[controller]")] // [] Platzhalter für den Route MovieController
    public class MovieController : MediatRBaseController
    {
        [HttpGet(nameof(MovieDto))]
        public async Task<IEnumerable<MovieDto>> GetMovieDtos([FromQuery] GetMovieDtosQuery query)
        {
                return await base.Mediator.Send(query);
        }

        [HttpGet(nameof(MovieDto) + "/{Id}")]
        public async Task<MovieDto> GetMovieDto([FromQuery] GetMovieDtoQuery query)
        {
            return await base.Mediator.Send(query);
        }

    }
}
