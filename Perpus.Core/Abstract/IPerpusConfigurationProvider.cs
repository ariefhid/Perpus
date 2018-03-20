using Perpus.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Core.Abstract
{
    public interface IPerpusConfigurationProvider
    {
        PerpusConfig GetPerpusConfig();
    }
}
