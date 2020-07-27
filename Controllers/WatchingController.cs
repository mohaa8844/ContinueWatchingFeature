using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContinueWatchingFeature.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContinueWatchingFeature.Models;
using Microsoft.EntityFrameworkCore;
using ContinueWatchingFeature.Services;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace ContinueWatchingFeature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchingController : ControllerBase
    {
        private readonly ContinueWatchingFeatureContext _context;
        private readonly WatchingsService _ws;
        Vars Vars = new Vars();
        public WatchingController(ContinueWatchingFeatureContext context,WatchingsService ws)
        {
            _context = context;
            _ws = ws;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MongoWatching>>> Index()
        {
            return _ws.Get();
           // return await _context.Still_Watchings.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<WatchingsResult>> getWatching(int Id)
        {

            return new WatchingsResult { Status = "Done", Watchings = _ws.Get(Id.ToString()) }; //new WatchingsResult { Status = "Done", Watchings = await _context.Still_Watchings.Where(x => x.User_id == Id).ToListAsync() };
        }
        [HttpPost]
        public async Task<ActionResult<Result>> InsertWatching(MongoWatching newWatch)
        {
            _ws.Create(newWatch);
            return new Result { Text = "Done" };
        }


        [HttpPost("seek")]
        public async Task<ActionResult<Result>> Seek([Bind("User_Id","Media_Id","Type", "SeekPosition")] NewSeek newSeek)
        {
            //To be used while watching, it dosent return a seek position so that it wont update the seek position if there were two persons using the same account on different devices and watching the same media
           
            Result res = null;

            String resultText;
            //List<MongoWatching> temp = Vars.still_s;

            List<MongoWatching> list = Vars.writing ? Vars.still_s_temp : Vars.still_s;
            List<MongoWatching> remove_list = Vars.writing ? Vars.removed_temp : Vars.removed;

            MongoWatching ramQuery = list.Where(watching =>watching!=null && watching.User_id == newSeek.User_Id.ToString() && watching.Media_Id == newSeek.Media_Id && watching.Type == newSeek.Type).SingleOrDefault();
            if (ramQuery == null)
            {
                MongoWatching query = _ws.Get(newSeek.User_Id.ToString(), newSeek.Media_Id, newSeek.Type);
                if (query != null)
                {
                    ramQuery = query;
                }
                else
                {
                    ramQuery = new MongoWatching { User_id = newSeek.User_Id.ToString(), Media_Id = newSeek.Media_Id, Type = newSeek.Type, SeekPosition = newSeek.SeekPosition };
                    if (ramQuery != null)
                    {
                        Vars.still_s.Add(ramQuery);
                        resultText = "Added";
                    }
                    else
                    {
                        resultText = "Error occurred";
                    }
                }
            }


            if (IsCompleted(newSeek.SeekPosition, newSeek.Media_Id, newSeek.Type))
            {
                remove_list.Add(ramQuery);
                list.Remove(ramQuery);
                _ws.Remove(ramQuery);
                //var q = from qu in _context.Still_Watchings where qu.User_id == newSeek.User_Id && qu.Media_Id == newSeek.Media_Id && qu.Type == newSeek.Type select qu;
                //if (q.Count() > 0)
                //    _context.Still_Watchings.Remove(q.SingleOrDefault());
                resultText = "Completed";
            }
            else
            {
                ramQuery.SeekPosition = newSeek.SeekPosition;
                resultText = "Watching";
            }

            return new Result {Text = resultText };


        }


        [HttpPost("check")]
        public  ActionResult<WatchingResult> Check([Bind("User_Id", "Media_Id", "Type", "SeekPosition")] NewSeek newSeek)
        {
            String resultText = "";

            List<MongoWatching> list = Vars.writing ? Vars.still_s_temp : Vars.still_s;

            MongoWatching ramQuery = list.Where(watching => watching.User_id == newSeek.User_Id.ToString() && watching.Media_Id == newSeek.Media_Id && watching.Type == newSeek.Type).SingleOrDefault();
            if (ramQuery == null)
            {
                MongoWatching query = _ws.Get(newSeek.User_Id.ToString(), newSeek.Media_Id, newSeek.Type);
                if (query != null)
                {
                    ramQuery = query;
                }
            }
            if (ramQuery != null)
            {
                resultText = "Watching";
            }
            else
            {
                resultText = "Not Watching";
            }

            WatchingResult watchingResult = new WatchingResult { Status = resultText, Watchings = ramQuery };
            return watchingResult;
        }


        [HttpPost("register")]
        public async Task<ActionResult<WatchingResult>> Register([Bind("User_Id", "Media_Id", "Type", "SeekPosition")] NewSeek newSeek)
        {
            String resultText;
            if (!Vars.initeated)
            {
                Vars.initeated = true;
                Thread thread = new Thread(task);
               // thread.Start();

            }
            List<MongoWatching> list = Vars.writing ? Vars.still_s_temp : Vars.still_s;
            List<MongoWatching> remove_list = Vars.writing ? Vars.removed_temp : Vars.removed;

            List<MongoWatching> ramQuerys = list.Where(watching => watching.User_id == newSeek.User_Id.ToString() && watching.Media_Id == newSeek.Media_Id && watching.Type == newSeek.Type).ToList();
            MongoWatching ramQuery = null;
            if (ramQuerys.Count==0)
            {
                MongoWatching query = _ws.Get(newSeek.User_Id.ToString(), newSeek.Media_Id, newSeek.Type);
                if (query != null)
                {
                    ramQuery = query;
                }
                else
                {
                    ramQuery = new MongoWatching { User_id = newSeek.User_Id.ToString(), Media_Id = newSeek.Media_Id, Type = newSeek.Type, SeekPosition = newSeek.SeekPosition };
                    if (ramQuery != null)
                    {
                        list.Add(ramQuery);
                        resultText = "Added";
                    }
                    else
                    {
                        resultText = "Error occurred";
                    }
                }
            }
            else
            {
                ramQuery = ramQuerys[0];
            }

            if (IsCompleted(newSeek.SeekPosition, newSeek.Media_Id, newSeek.Type))
            {
                remove_list.Add(ramQuery);
                list.Remove(ramQuery);
                var q = from qu in _context.Still_Watchings where qu.User_id == newSeek.User_Id && qu.Media_Id == newSeek.Media_Id && qu.Type == newSeek.Type select qu;
                if (q.Count() > 0)
                    _context.Still_Watchings.Remove(q.SingleOrDefault());
                _ws.Remove(ramQuery);
                resultText = "Completed";
            }
            else
            {
                resultText = "Watching";
            }

           return new WatchingResult { Status = resultText, Watchings = ramQuery };
           
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
        [HttpGet("write")]
        public ActionResult<Result> Write()
        {
            // int count = -1;// Vars.still_s.Count;
            if (Vars.still_s.Count > 0)
            {

                Console.WriteLine("in");
                List<Still_Watching> toChange = new List<Still_Watching>();
                List<MongoWatching> mongoWatchings = Vars.still_s.Where(x => true).ToList(); ;
                List<MongoWatching> removed = Vars.removed.Where(x => true).ToList(); ;


                toChange = mongoWatchings.Select(x => new Still_Watching { Media_Id = x.Media_Id, Type = x.Type, User_id = int.Parse(x.User_id) }).ToList();

                foreach (MongoWatching mongoWatching in removed)
                {
                    int u_id = int.Parse(mongoWatching.User_id);
                    var watch = (from q in _context.Still_Watchings where q.Media_Id == mongoWatching.Media_Id && q.User_id == u_id && q.Type == mongoWatching.Type select q).ToList();
                    if (watch.Count > 0) _context.Still_Watchings.Remove(watch[0]);
                }

                _context.Still_Watchings.AddRange(toChange);
                _context.SaveChanges();

                foreach (MongoWatching mw in mongoWatchings)
                {
                    MongoWatching query = _ws.Get(mw.User_id, mw.Media_Id, mw.Type);
                    if (query != null)
                    {
                        query.SeekPosition = mw.SeekPosition;
                        _ws.Update(query.Id, query);
                    }
                    else
                    {
                        _ws.Create(mw);
                    }
                }

                Vars.still_s.Clear();
                Vars.removed.Clear();
                Vars.still_s = Vars.still_s_temp.Where(x => true).ToList();
                Vars.removed = Vars.removed_temp.Where(x => true).ToList();

            }

            return new Result { Text = "Done"};
        }
        private void task()
        {
            while (true)
            {
                if (Vars.still_s.Count > 0)
                {

                    Console.WriteLine("in");
                    List<Still_Watching> toChange = new List<Still_Watching>();
                    List<MongoWatching> mongoWatchings = Vars.still_s;
                    List<MongoWatching> removed = Vars.removed;


                    toChange = mongoWatchings.Select(x => new Still_Watching { Media_Id = x.Media_Id, Type = x.Type, User_id = int.Parse(x.User_id) }).ToList();

                    foreach (MongoWatching mongoWatching in removed)
                    {
                        int u_id = int.Parse(mongoWatching.User_id);
                        var watch = (from q in _context.Still_Watchings where q.Media_Id == mongoWatching.Media_Id && q.User_id == u_id && q.Type == mongoWatching.Type select q).ToList();
                        if (watch.Count>0) _context.Still_Watchings.Remove(watch[0]);
                    }

                    _context.Still_Watchings.AddRange(toChange);
                    _context.SaveChanges();

                    foreach (MongoWatching mw in mongoWatchings)
                    {
                        MongoWatching query = _ws.Get(mw.User_id, mw.Media_Id, mw.Type);
                        if (query != null)
                        {
                            query.SeekPosition = mw.SeekPosition;
                            _ws.Update(query.Id, query);
                        }
                        else
                        {
                            _ws.Create(mw);
                        }
                    }

                    Vars.still_s.Clear();
                    Vars.removed.Clear();
                    Vars.still_s = Vars.still_s_temp.Where(x => true).ToList();
                    Vars.removed = Vars.removed_temp.Where(x => true).ToList();

                }
                Thread.Sleep(8000);
               // Thread.Sleep(6 * 3600000);
            }
        }
    }
}
