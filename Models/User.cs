using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public String Name { get; set; }
    }
}
