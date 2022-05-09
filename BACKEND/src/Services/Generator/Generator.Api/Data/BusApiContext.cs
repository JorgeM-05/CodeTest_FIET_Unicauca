using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Generator.Api.Models;

namespace Generator
{
    public class BusApiContext : DbContext
    {
        public BusApiContext (DbContextOptions<BusApiContext> options)
            : base(options)
        {
        }

        public DbSet<Generator.Api.Models.CasBus> CasBus { get; set; }
    }
}
