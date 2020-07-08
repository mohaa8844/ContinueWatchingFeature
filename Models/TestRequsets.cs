using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class SeedOptions
    {
        public bool FlushDB { get; set; }
        public int UsersCount { get; set; }
        public int SeasonsCount { get; set; }
        public int SeriesCount { get; set; }
        public int MoviesCount { get; set; }
        public int EpsoidsCount { get; set; }
        public int SeeksCount { get; set; }
    }
}
