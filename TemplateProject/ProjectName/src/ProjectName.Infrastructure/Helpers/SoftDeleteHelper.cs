using ProjectName.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Helpers;
public static class SoftDeleteHelper
{
    public static void ProcessSoftDelete(ChangeTracker changeTracker)
    {
        changeTracker.DetectChanges();

        IEnumerable<EntityEntry> markedAsDeleted = changeTracker.Entries()
            .Where(x => x.State == EntityState.Deleted);

        foreach (EntityEntry item in markedAsDeleted)
        {
            if (item.Entity is not ISoftDeletable entity)
                continue;

            // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
            item.State = EntityState.Unchanged;

            // Only update the IsDeleted flag - only this will get sent to the Db
            entity.IsDeleted = true;
        }
    }
}
