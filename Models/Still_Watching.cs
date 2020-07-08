using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class Still_Watching
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public int Media_Id { get; set; }
        public int Type { get; set; }
        public int SeekPosition { get; set; }
    }
}
