using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assessment_try.Models;

namespace Assessment_try.Data
{
    public class Assessment_tryContext : DbContext
    {
        public Assessment_tryContext (DbContextOptions<Assessment_tryContext> options)
            : base(options)
        {
        }

        public DbSet<Assessment_try.Models.User> Register { get; set; } = default!;
    }
}
