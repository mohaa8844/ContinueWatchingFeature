using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int Length { get; set; }

        [MaxLength(75)]
        public String Name { get; set; }
    }
}
