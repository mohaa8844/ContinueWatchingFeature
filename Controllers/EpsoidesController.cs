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
    public class EpsoidesController : ControllerBase
    {
        private readonly ContinueWatchingFeatureContext _context;

        public EpsoidesController(ContinueWatchingFeatureContext context)
        {
            _context = context;
        }

        [HttpGet("epsoides")]
        public async Task<ActionResult<IEnumerable<Epsoide>>> Index()
        {
            return await _context.Epsoides.ToListAsync();
        }

        [HttpPost("add")]
        public async Task<ActionResult<Result>> Create([Bind("Series_Id","Season", "Length")] NewEpsoide epsoide)
        {
            String text = "";
            if (!_context.Series.Any(x => x.Id == epsoide.Series_Id))
            {
                _context.Epsoides.Add(new Epsoide { Series_Id = epsoide.Series_Id, Season = epsoide.Season, Length = epsoide.Length });
                await _context.SaveChangesAsync();
                text = "Done";
            }
            else
            {
                text = "Error - content wasn't found";
            }
            return new Result { Text = text };
        }
    }
}
