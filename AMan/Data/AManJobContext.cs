using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AMan.Models
{
    public class AManJobContext : DbContext
    {
        public AManJobContext (DbContextOptions<AManJobContext> options)
            : base(options)
        {
        }

        public DbSet<AMan.Models.Job> Job { get; set; }
    }
}
