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
        public Still_Watching Watchings { get; set; }
    }
    public class WatchingsResult
    {
        public String Status { get; set; }
        public IEnumerable<Still_Watching> Watchings { get; set; }
    }
}
