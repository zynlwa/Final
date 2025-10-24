using AppointmentSystem.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AppointmentSystem.Persistance.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        if (eventData.Context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = eventData.Context.ChangeTracker
            .Entries()
            .Where(e => e.Entity is ISoftDeletable && e.State == EntityState.Deleted);

        foreach (var entityEntry in entries)
        {
            ((ISoftDeletable)entityEntry.Entity).SoftDelete("System");

            entityEntry.State = EntityState.Modified;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}