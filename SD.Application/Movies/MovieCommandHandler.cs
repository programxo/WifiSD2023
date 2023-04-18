using MediatR;
using SD.Application.Base;
using Wifi.SD.Core.Application.Movies.Commands;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Attributes;
using Wifi.SD.Core.Entities.Movies;
using Wifi.SD.Core.Repositories;

namespace SD.Application.Movies
{
    [MapServiceDependency(nameof(MovieCommandHandler))]
    public class MovieCommandHandler : HandlerBase, IRequestHandler<CreateMovieDtoCommand, MovieDto>,
                                                    IRequestHandler<UpdateMovieDtoCommand, MovieDto>,
                                                    IRequestHandler<DeleteMovieDtoCommand, bool>
    {
        protected readonly IMovieRepository movieRepository;

        //public MovieQueryHandler(IServiceProvider serviceProvider)
        //{
        //    movieRepository = serviceProvider.GetRequiredService<IMovieRepository>(); // Single-Responsibility-Prinzip
        //}

        public MovieCommandHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<MovieDto> Handle(CreateMovieDtoCommand request, CancellationToken cancellationToken)
        {

            
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "n/a",
                GenreId = 1,
                Rating = Ratings.Medium
            };

            await this.movieRepository.AddAsync(movie, true, cancellationToken);
            return MovieDto.MapFrom(movie);
        }

        public async Task<MovieDto> Handle(UpdateMovieDtoCommand request, CancellationToken cancellationToken)
        {
            request.MovieDto.Id = request.Id; // Übereinstimmung mit der Id, damit niemand das überschreibt

            var movie = new Movie();

            base.MapEntityProperties<MovieDto, Movie>(request.MovieDto, movie, null);

            var updMovie = await movieRepository.UpdateAsync<Movie>(movie, request.Id, true, cancellationToken);
            return MovieDto.MapFrom(updMovie);
        }

        public async Task<bool> Handle(DeleteMovieDtoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await this.movieRepository.RemoveAsync<Movie>(request.Id, true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
