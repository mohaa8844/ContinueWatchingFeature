using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public static class Vars
    {
        public static List<MongoWatching> still_s = new List<MongoWatching>();
        public static List<MongoWatching> still_s_temp = new List<MongoWatching>();
        public static List<MongoWatching> removed = new List<MongoWatching>();
        public static List<MongoWatching> removed_temp = new List<MongoWatching>();
        public static bool initeated = false;
        public static bool writing = false;
      

    }
}
