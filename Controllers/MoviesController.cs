using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContinueWatchingFeature.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContinueWatchingFeature.Models;
using Microsoft.EntityFrameworkCore;

namespace ContinueWatchingFeature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ContinueWatchingFeatureContext _context;

        public MoviesController(ContinueWatchingFeatureContext context)
        {
            _context = context;
        }

        [HttpGet("movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> Index()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpPost("add")]
        public async Task<ActionResult<Result>> Create([Bind("Name","Length")] NewMovie movie)
        {
            _context.Movies.Add(new Movie { Name = movie.Name ,Length=movie.Length});
            await _context.SaveChangesAsync();

            Result res = new Result { Text = "Done" };
            return res;
        }

    }
}
