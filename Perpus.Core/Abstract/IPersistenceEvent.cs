using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Abstract
{
    /// <summary>
    /// Grab the event for each persistence action
    /// </summary>
    public interface IPersistenceEvent
    {
        Task BeforeSaveAsync(IEnumerable<object> Objects);
        Task AfterSaveAsync(IEnumerable<object> Objects);
        Task BeforeDeleteAsync(IEnumerable<object> Objects);
        Task AfterDeleteAsync();
    }
}
