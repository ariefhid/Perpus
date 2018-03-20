using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Perpus.Domain.Concrete.Context;
using Perpus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perpus.Infrastructure
{
    public class DatabaseInitializer
    {
        public DatabaseInitializer()
        {

        }

        private static void PerpusSeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PerpusContext>();
                context.Database.Migrate();

                context.SaveChanges();
            }
        }

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                PerpusSeedData(app);
            }
        }

        public static void InitializeDevData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PerpusContext>();
                #region initAuthor
                //if (!context.Authors.Any())
                //{

                //    context.Authors.AddRange(
                //        new Author { Code = "A01", AuthorName = "Davinci", IsActive = true }
                //        );
                //    context.SaveChanges();
                //}
                //if (!context.Facilites.Any())
                //{

                //    context.Facilites.AddRange(
                //        new Facility { Code = "F1", Name = "Fanta", IsActive = true }
                //        );
                //    context.SaveChanges();
                //}
                //if (!context.MeetingRooms.Any())
                //{

                //    context.MeetingRooms.AddRange(
                //        new MeetingRoom
                //        {
                //            Code = "RM1",
                //            Location = context.Locations.FirstOrDefault(f => f.Code == "B001"),
                //            Name = "Ruang Meeting 1",
                //            IsActive = true
                //        };

                //        );
                //    context.SaveChanges();
                //}
                #endregion
            }
        }
    }

}

