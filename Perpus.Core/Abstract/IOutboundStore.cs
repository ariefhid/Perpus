using Perpus.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Abstract
{
    /// <summary>
    /// Store the outbound data before it's exported to the third party system
    /// </summary>
    public interface IOutboundStore : IPersistenceStore<Outbound>
    {

    }
}
