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
    public class UsersController : ControllerBase
    {
        private readonly ContinueWatchingFeatureContext _context;

        public UsersController(ContinueWatchingFeatureContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            return await _context.Users.ToListAsync();
        }


        [HttpPost("add")]
        public async Task<ActionResult<Result>> Create([Bind("Name")] NewUser user)
        {
            _context.Users.Add(new User { Name = user.Name});
            await _context.SaveChangesAsync();

            Result res = new Result { Text = "Done" };
            return res;
        }
    }
}
