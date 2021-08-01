using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ATMModel.DataAccess.EF.Migration
{
    public static class MigrationService
    {
        /// <summary>
        /// Performs run-time migration. In case of error throws error into ErrorInterceptor
        /// PLEASE NOTE: At moment of database migration ErrorInterceptor is not ready yet,
        /// so all exceptions goes to console.
        /// Maybe we'll fix it later.
        /// <param name="webHost">Built IWebHost</param>
        /// <param name="args">Command-line arguments</param>
        /// <returns>returns IWebHost to allow chained calls </returns>
        /// </summary>
        public static IHost DoMigrate(this IHost host, string[] args)
        {
            //migration during startup is on by default, --disable_migrate suppresses it
            if (Array.Find(args, element => element == "--disable_migration") != null) 
                return host;
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DbContext>();

                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        $"Error during migration, thr code is {ex.HResult}, the message is {ex.Message}");
                }
            }

            return host;
        }
    }
}