using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ATMModel.Models;
using ATMModel.DataAccess.EF.DBSchema;

public class ATMModelContext : DbContext
{
    public ATMModelContext (DbContextOptions<ATMModelContext> options)
        : base(options)
    {
    }

    public DbSet<ATMModel.Models.User> User { get; set; }
    public DbSet<ATMModel.Models.Card> Card { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SetupDbSchema();
//        DoDataSeeding(modelBuilder);
    }
}
