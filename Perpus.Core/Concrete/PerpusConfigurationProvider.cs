using Perpus.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Perpus.Core.Entity;
using Microsoft.Extensions.Configuration;


namespace Perpus.Core.Concrete
{
    public class PerpusConfigurationProvider : IPerpusConfigurationProvider
    {
        private IConfiguration _configuraton;

        public PerpusConfigurationProvider(IConfiguration configuration)
        {
            this._configuraton = configuration;
        }
        public PerpusConfig GetPerpusConfig()
        {
            try
            {
                return this._configuraton
               .GetSection("PerpusConfig").Get<PerpusConfig>();
            }
            catch (Exception ex)
            {

                throw new Exception("PerpusConfig is not  defined in the appsetting configuration files", ex);
            }
        }
    }
}
