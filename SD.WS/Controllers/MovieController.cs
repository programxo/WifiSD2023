using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wifi.SD.Core.Application.Movies.Commands;
using Wifi.SD.Core.Application.Movies.Queries;
using Wifi.SD.Core.Application.Movies.Results;

namespace SD.WS.Controllers
{
    [ApiController]
    [Route("[controller]")] // [] Platzhalter für den Route MovieController
    public class MovieController : MediatRBaseController
    {
        [HttpGet(nameof(MovieDto))]
        public async Task<IEnumerable<MovieDto>> GetMovieDtos([FromQuery] GetMovieDtosQuery query, CancellationToken cancellationToken)
        {
            return await base.Mediator.Send(query);
        }

        [HttpGet(nameof(MovieDto) + "/{Id}")]
        public async Task<MovieDto> GetMovieDto([FromQuery] GetMovieDtoQuery query, CancellationToken cancellationToken)
        {
            return await base.Mediator.Send(query);
        }

        [ProducesResponseType(typeof(MovieDto), (int)HttpStatusCode.Created)] // Für Swagger - Generierung für Status 201
        [HttpPost(nameof(MovieDto))] 
        public async Task<MovieDto> CreateMovieDto(CancellationToken cancellationToken)
        {
            var createMovieDtoCommand = new CreateMovieDtoCommand();
            var result = await base.Mediator.Send(createMovieDtoCommand, cancellationToken);

            base.SetLocationUri(result, result.Id.ToString());
            return result;
        }

    }
}
