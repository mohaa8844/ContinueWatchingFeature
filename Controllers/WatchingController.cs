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
    public class WatchingController : ControllerBase
    {
        private readonly ContinueWatchingFeatureContext _context;

        public WatchingController(ContinueWatchingFeatureContext context)
        {
            _context = context;
        }

        [HttpGet("watchings")]
        public async Task<ActionResult<IEnumerable<Still_Watching>>> Index()
        {
            return await _context.Still_Watchings.ToListAsync();
        }

        [HttpGet("watchings/{Id}")]
        public async Task<ActionResult<WatchingsResult>> getWatching(int Id)
        {
            return new WatchingsResult { Status = "Done", Watchings = await _context.Still_Watchings.Where(x => x.User_id == Id).ToListAsync() };
        }

        [HttpPost("seek")]
        public async Task<ActionResult<Result>> Seek([Bind("User_Id","Media_Id","Type", "SeekPosition")] NewSeek newSeek)
        {
            //To be used while watching, it dosent return a seek position so that it wont update the seek position if there were two persons using the same account on different devices and watching the same media
            Result res = null;
            var query = _context.Still_Watchings.Where(x => x.Media_Id == newSeek.Media_Id && x.Type == newSeek.Type && x.User_id == newSeek.User_Id);

            if (query.Count()>0)
            {
                Still_Watching chosen_Still_Watching = query.SingleOrDefault();

                if (IsCompleted(newSeek.SeekPosition, newSeek.Media_Id, newSeek.Type))
                {

                    _context.Still_Watchings.Remove(chosen_Still_Watching);
                    await _context.SaveChangesAsync();

                    res=new Result { Text = "Completed" };
                }
                else
                {
                    chosen_Still_Watching.SeekPosition = newSeek.SeekPosition;
                    await _context.SaveChangesAsync();

                    res = new Result { Text = "Watching" };
                }

            }
            else
            {
                _context.Still_Watchings.Add(new Still_Watching { User_id = newSeek.User_Id, Media_Id = newSeek.Media_Id, Type = newSeek.Type, SeekPosition = newSeek.SeekPosition });
                await _context.SaveChangesAsync();

                res = new Result { Text = "Added" };
            }


            return res;
        }


        [HttpPost("check")]
        public  ActionResult<WatchingResult> Check([Bind("User_Id", "Media_Id", "Type", "SeekPosition")] NewSeek newSeek)
        {
            String resultText = "";
            Still_Watching still_WatchingResult = null;
            var query = _context.Still_Watchings.Where(x => x.Media_Id == newSeek.Media_Id && x.Type == newSeek.Type && x.User_id == newSeek.User_Id);

            if (query.Count() > 0)
            {
                resultText = "Watching";
                still_WatchingResult = query.SingleOrDefault();
            }
            else
            {
                resultText = "notWatching";
            }

            WatchingResult watchingResult = new WatchingResult { Status = resultText, Watchings = still_WatchingResult };
            return watchingResult;
        }

        [HttpPost("register")]
        public async Task<ActionResult<WatchingResult>> Register([Bind("User_Id", "Media_Id", "Type", "SeekPosition")] NewSeek newSeek)
        {
            String resultText = "";
            var query = _context.Still_Watchings.Where(x => x.Media_Id == newSeek.Media_Id && x.Type == newSeek.Type && x.User_id == newSeek.User_Id);

            if (query.Count() > 0)
            {
                Still_Watching chosen_Still_Watching = query.SingleOrDefault();

                if (IsCompleted(newSeek.SeekPosition, newSeek.Media_Id, newSeek.Type))
                {
                    _context.Still_Watchings.Remove(chosen_Still_Watching);
                    await _context.SaveChangesAsync();

                    resultText = "Completed";
                }
                else
                {
                    resultText = "Watching";
                }

            }
            else
            {
                _context.Still_Watchings.Add(new Still_Watching { User_id = newSeek.User_Id, Media_Id = newSeek.Media_Id, Type = newSeek.Type, SeekPosition = newSeek.SeekPosition });
                await _context.SaveChangesAsync();

                resultText = "Added";
            }

            WatchingResult watchingResult = new WatchingResult { Status = resultText, Watchings = await _context.Still_Watchings.Where(x => x.User_id == newSeek.User_Id && x.Media_Id == newSeek.Media_Id && x.Type == newSeek.Type).SingleOrDefaultAsync() };
            return watchingResult;
        }



        [HttpPost("seriesinquery")]
        public  ActionResult<Result> SeriesInquery([Bind("User_Id", "Series_Id")] SeriesInquery seriesInquery)
        {
            //To be used for inqueing about a series
            Result res = null;
            IEnumerable<Epsoide> epsiodes = _context.Epsoides.Where(x => x.Series_Id == seriesInquery.Series_Id);
            var query = _context.Still_Watchings.Where(x => x.Type==1 &&  epsiodes.Any(u=>u.Id==x.Media_Id));

            if (query.Count() > 0)
            {
                 res = new Result { Text = "Watching" };
            }
            else
            {
                res = new Result { Text = "NotWatching" };
            }


            return res;
        }

        private bool IsCompleted(int current,int media_id,int type)
        {
            int total;
            if (type == 0)
            {
                total = _context.Movies.Where(x => x.Id == media_id).SingleOrDefault().Length;
            }
            else
            {
                total = _context.Epsoides.Where(x => x.Id == media_id).SingleOrDefault().Length;
            }
            return current * 100 / total > 90;
        }
    }
}
