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
    public class SeriesController : ControllerBase
    {
        private readonly ContinueWatchingFeatureContext _context;

        public SeriesController(ContinueWatchingFeatureContext context)
        {
            _context = context;
        }

        [HttpGet("series")]
        public async Task<ActionResult<IEnumerable<Series>>> Index()
        {
            return await _context.Series.ToListAsync();
        }

        [HttpPost("add")]
        public async Task<ActionResult<Result>> Create([Bind("Name","Season")] NewSeries series)
        {
            _context.Series.Add(new Series { Name=series.Name,Seasons=series.Seasons });
            await _context.SaveChangesAsync();

            Result res = new Result { Text = "Done" };
            return res;
        }
    }
}
