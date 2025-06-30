using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using ProjectName.Domain.Entities.Enums;

namespace ProjectName.Domain.Entities.Common;

public static class TrackableHelpers
{
    /// <summary>
    /// Walk the changes in a ChangeTracker over changes to TrackableEntity records
    /// and apply data to the fields
    /// </summary>
    /// <param name="changes">changes about to be saved</param>
    /// <param name="userName">user name of person (or machine) that made the changes</param>
    public static void PopulateTrackableFields(ChangeTracker changes, string? username, bool writeTrackableDate)
    {
        foreach (var e in changes.Entries<TrackableEntity>())
        {
            TrackableEntity te = e.Entity;
            switch (e.State)
            {
                case EntityState.Added:
                    var currentDate = DateTime.UtcNow;
                    if (writeTrackableDate)
                    {
                        te.CreatedAt = currentDate;
                        te.UpdatedAt = currentDate;
                        if(username != null)
                        {
                            te.CreatedBy = username;
                            te.UpdatedBy = username;
                        }
                    }
                    break;

                case EntityState.Modified:
                    if (writeTrackableDate)
                    {
                        te.Touch();
                        if (username != null)
                            te.UpdatedBy = username;
                    }
                    break;

                case EntityState.Deleted:
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }
        }
    }
}
