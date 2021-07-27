using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ATMModel.Models;

    public class ATMModelContext : DbContext
    {
        public ATMModelContext (DbContextOptions<ATMModelContext> options)
            : base(options)
        {
        }

        public DbSet<ATMModel.Models.User> User { get; set; }
    }
