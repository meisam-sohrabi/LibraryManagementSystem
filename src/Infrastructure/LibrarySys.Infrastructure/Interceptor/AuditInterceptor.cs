using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LibrarySys.Infrastructure.Interceptor
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            ApplyAudit(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ApplyAudit(DbContext? context)
        {
            if (context == null) return;

            var entiries = context.ChangeTracker.Entries<BaseClass>();
            var utcNow = DateTime.UtcNow;
            foreach ( var entry in entiries )
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = utcNow;
                    entry.Entity.UpdatedAt = utcNow;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = utcNow;

                    // Prevent accidental overwrite
                    entry.Property(c => c.CreatedAt).IsModified = false;
                }

            }
        }
    }
}
