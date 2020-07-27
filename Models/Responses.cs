using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class Result
    {
        public String Text { get; set; }
    }
    public class WatchingResult
    {
        public String Status { get; set; }
        public MongoWatching Watchings { get; set; }
    }
    public class WatchingsResult
    {
        public String Status { get; set; }
        public IEnumerable<MongoWatching> Watchings { get; set; }
    }
}
