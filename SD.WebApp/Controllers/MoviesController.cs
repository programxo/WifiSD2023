﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SD.Application.Extensions;
using SD.Persistence.Repositories.DBContext;
using SD.WebApp.Extensions;
using System.Threading;
using Wifi.SD.Core.Application.Movies.Commands;
using Wifi.SD.Core.Application.Movies.Queries;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Entities.Movies;

namespace SD.WebApp.Controllers
{
    // [Authorize (Roles = "Admin")]
    public class MoviesController : MediatRBaseController
    {
        private readonly MovieDbContext _context;

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        // Beispiele:
        [AllowAnonymous]
        public async Task<string> HelloWorld(string name)
        {
            string result = string.Empty;
            for (int i = 0; i < 10; i++)
            {
               result = await Task.FromResult($"Hallo {name}!");
            }
            return result;
        }

        // GET: Movies
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var movieQuery = new GetMovieDtosQuery();
            var result = await base.Mediator.Send(movieQuery, cancellationToken);
            return View(result);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id, CancellationToken cancellationToken)
        {
            var movieQuery = new GetMovieDtoQuery { Id = id.Value };
            var result = await base.Mediator.Send(movieQuery, cancellationToken);

            return View(result);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var result = new MovieDto { Rating = Ratings.Medium, GenreId = 1, MediumTypeCode = "BR" };
            await this.InitMasterDataViewData(result, cancellationToken);

            result.ReleaseDate = DateTime.Now;
            return View(result);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieDto movieDto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateMovieDtoCommand();
                /* Movie neu anlegen */
                var newMovieDto = await base.Mediator.Send(command, cancellationToken);
                movieDto.Id = newMovieDto.Id;

                await Mediator.Send(new UpdateMovieDtoCommand { Id = newMovieDto.Id, MovieDto = movieDto }, cancellationToken);
 
                return RedirectToAction(nameof(Index));
            }

            await this.InitMasterDataViewData(movieDto, cancellationToken);
            return View(movieDto);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken)
        {
                if (id == null)
                {
                    return NotFound();
                }

                var movieQuery = new GetMovieDtoQuery { Id = id.Value };
                var result = await base.Mediator.Send(movieQuery, cancellationToken);

                if (result == null)
                {
                    return NotFound();
                }

            await this.InitMasterDataViewData(result, cancellationToken);

            //ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", result.GenreId);
            //ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code", result.MediumTypeCode);
            return View(result);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MovieDto movieDto, CancellationToken cancellationToken)
        {
            if (id != movieDto.Id)
            {
                return NotFound();
            }

            

            if (ModelState.IsValid)
            {
                var command = new UpdateMovieDtoCommand() { MovieDto = movieDto, Id = id };
                await base.Mediator.Send(command, cancellationToken);

                return RedirectToAction(nameof(Index));
            }

            await this.InitMasterDataViewData(movieDto, cancellationToken);
            return View(movieDto);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetMovieDtoQuery { Id = id };
            var result =  await base.Mediator.Send(query, cancellationToken);

            if(result == null)
            {
                return NotFound();
            }

            await InitMasterDataViewData(result, cancellationToken);
            return View(result);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
        {
            await base.Mediator.Send(new DeleteMovieDtoCommand { Id = id }, cancellationToken);

            return RedirectToAction(nameof(Index));
        }


        #region Private helpers
        private bool MovieExists(Guid id)
        {
          return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task InitMasterDataViewData(MovieDto movieDto, CancellationToken cancellationToken)
        {
            var genres = this.HttpContext.Session.Get<IEnumerable<Genre>>("Genres");
            if (genres == null)
            {
                genres = await this.Mediator.Send(new GetGenresQuery(), cancellationToken);
                this.HttpContext.Session.Set<IEnumerable<Genre>>("Genres", genres);
            }

            var localizedRatings = this.HttpContext.Session.Get<List<KeyValuePair<object, string>>>("Ratings");
            if (localizedRatings == null)
            {
                localizedRatings = EnumExtension.EnumToList<Ratings>();
                this.HttpContext.Session.Set<List<KeyValuePair<object, string>>>("Ratings", localizedRatings);
            }

            var mediumTypes = this.HttpContext.Session.Get < IEnumerable<MediumType>>("MediumTypes");
            if (mediumTypes == null)
            {
                mediumTypes = await this.Mediator.Send(new GetMediumTypesQuery(), cancellationToken);

                this.HttpContext.Session.Set<IEnumerable<MediumType>>("MediumTypes", mediumTypes);
            }

            ViewBag.Genres = new SelectList(genres, nameof(Genre.Id), nameof(Genre.Name), movieDto.GenreId);
            ViewData.Add("MediumTypes", new SelectList(mediumTypes, nameof(MediumType.Code), nameof(MediumType.Name), movieDto.MediumTypeCode)); ;
            ViewBag.Ratings = new SelectList(localizedRatings, "Key", "Value", movieDto.Rating);
        }
        #endregion
    }
}
