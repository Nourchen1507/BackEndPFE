using App.ApplicationCore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistance
{
 public  class TimeStampInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var changeEntries = eventData.Context.ChangeTracker.Entries();
            foreach (var entry in changeEntries)
            {
                if (entry.Entity is TimeStamp entity)
                {
                    var currentTime = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = currentTime;
                        entity.UpdatedAt = currentTime;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = currentTime;
                    }
                }
            }
            return result;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken)
        {
            var changeEntries = eventData.Context.ChangeTracker.Entries();

            foreach (var entry in changeEntries)
            {
                if (entry.Entity is TimeStamp entity)
                {
                    var currentTime = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = currentTime;
                        entity.UpdatedAt = currentTime;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = currentTime;
                    }
                }
            }
            return new ValueTask<InterceptionResult<int>>(result);
        }
    }
}