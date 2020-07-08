using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class NewUser
    {
        public String Name { get; set; }
    }
    public class NewMovie
    {
        public int Length { get; set; }
        public String Name { get; set; }
    }
    public class NewEpsoide
    {
        public int Series_Id { get; set; }
        public int Length { get; set; }
        public int Season { get; set; }
    }
    public class NewSeries
    {
        public String Name { get; set; }
        public int Seasons { get; set; }
    }
    public class NewSeek
    {
        public int User_Id { get; set; }
        public int Media_Id { get; set; }
        public int Type { get; set; }
        public int SeekPosition { get; set; }
    }
    public class SeriesInquery
    {
        public int User_Id { get; set; }
        public int Series_Id { get; set; }
    }

}
