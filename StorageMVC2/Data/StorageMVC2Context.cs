using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StorageMVC2.Models
{
    public class StorageMVC2Context : DbContext
    {
        public StorageMVC2Context (DbContextOptions<StorageMVC2Context> options)
            : base(options)
        {
        }

        public DbSet<StorageMVC2.Models.Product> Product { get; set; }
    }
}
