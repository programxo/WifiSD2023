﻿using MediatR;
using Wifi.SD.Core.Application.Movies.Queries;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Attributes;
using Wifi.SD.Core.Entities.Movies;
using Wifi.SD.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SD.Application.Movies
{
    [MapServiceDependency(nameof(MovieQueryHandler))]
    public class MovieQueryHandler : IRequestHandler<GetMovieDtoQuery, MovieDto>,
                                                  IRequestHandler<GetMovieDtosQuery, IEnumerable<MovieDto>>,
                                                  IRequestHandler<GetGenresQuery, IEnumerable<Genre>>,
                                                  IRequestHandler<GetMediumTypesQuery, IEnumerable<MediumType>>
    {
        protected readonly IMovieRepository movieRepository;

        //public MovieQueryHandler(IServiceProvider serviceProvider)
        //{
        //    movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
        //}

        public MovieQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<MovieDto> Handle(GetMovieDtoQuery request, CancellationToken cancellationToken)
        {
            var movie = await this.GetMovieDtoQueryWithNavigationProperties() 
                                  .Where(w => w.Id == request.Id)
                                  .FirstOrDefaultAsync<Movie>(cancellationToken);

            if (movie != null)
            {
                return MovieDto.MapFrom(movie);
            }
            else
            {
                return null;
            }
        }

        private IQueryable<Movie> GetMovieDtoQueryWithNavigationProperties()
        {
            return this.movieRepository.QueryFrom<Movie>()
                                       .Include(nameof(Genre))
                                       .Include(nameof(MediumType));
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetMovieDtosQuery request, CancellationToken cancellationToken)
        {
            var movieQuery = this.GetMovieDtoQueryWithNavigationProperties();

            if(request.GenreId != null)
            {
                movieQuery = movieQuery.Where(w => w.GenreId == request.GenreId);
            }

            if (!string.IsNullOrWhiteSpace(request.MediumTypeCode))
            {
                movieQuery = movieQuery.Where(w => w.MediumTypeCode == request.MediumTypeCode);
            }

            if (request.Ratings?.Count > 0)
            {
                movieQuery = movieQuery.Where(w => request.Ratings.Contains(w.Rating.Value));
            }

            if (request.Take > 0)
            {
                movieQuery = movieQuery.Skip(request.Skip).Take(request.Take); // Pager-Funktion in EF mit Linq
            }

            var movieDtos = await movieQuery.Select(s => MovieDto.MapFrom(s)).ToListAsync(cancellationToken);

            //var movieDtos = new List<MovieDto>();
            //var movies = await movieQuery.ToListAsync(cancellationToken);

            //movies.ForEach(m => movieDtos.Add(MovieDto.MapFrom(m)));

            return movieDtos;
        }

        public async Task<IEnumerable<Genre>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            return await this.movieRepository.QueryFrom<Genre>().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<MediumType>> Handle(GetMediumTypesQuery request, CancellationToken cancellationToken)
        {
            return await this.movieRepository.QueryFrom<MediumType>().ToListAsync(cancellationToken);
        }
    }
}
