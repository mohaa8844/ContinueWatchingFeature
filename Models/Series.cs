using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class Series
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public String Name { get; set; }
        public int Seasons { get; set; }
    }
}
