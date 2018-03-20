using Microsoft.EntityFrameworkCore.ChangeTracking;
using Perpus.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Abstract
{
    public interface IAuditProcessor<TKey>
    {
        TKey GetCurrentUserId { get; }
        Tuple<IEnumerable<Audit>, IEnumerable<AuditEntry>> AuditSavedEntities(IEnumerable<EntityEntry> Entities);

        IEnumerable<Audit> AuditGeneratedDbValues(IEnumerable<AuditEntry> auditEntries);
    }
}
