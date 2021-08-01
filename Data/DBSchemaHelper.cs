using System;
using Microsoft.EntityFrameworkCore;

using ATMModel.Models;

namespace ATMModel.DataAccess.EF.DBSchema
{
    /// <summary>
    /// Database schemas helper.
    /// </summary>
    public static class DbSchemaHelper
    {
        /// <summary>
        /// Setup database schemas - constrains, indexes etc.
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        public static void SetupDbSchema(this ModelBuilder modelBuilder)
        {
            SetupDbSchemaCard(modelBuilder);
        }


        private static void SetupDbSchemaCard(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasOne(x => x.User)
                .WithMany(x => x.Cards);

            modelBuilder.Entity<Card>()
                .HasIndex(x => x.UserId);
            modelBuilder.Entity<Card>()
                .HasIndex(x => x.CardNumber)
                .IsUnique(true);
        }
    }
}
