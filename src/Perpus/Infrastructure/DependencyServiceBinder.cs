using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Perpus.Core.Abstract;
using Perpus.Core.Concrete;
using Perpus.Domain.Abstract;
using Perpus.Domain.Concrete;
using Perpus.Domain.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perpus.Infrastructure
{
    public class DependencyServiceBinder
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOutboundStore, OutboundStore<PerpusContext>>();
            services.AddTransient<IOutboundManager, OutboundManager>();          
            services.AddTransient<IPerpusConfigurationProvider, PerpusConfigurationProvider>();
            services.AddTransient<IAuthorManager, AuthorManager>();
            services.AddTransient<IAuthorStore, AuthorStore>();
            services.AddTransient<IAuthorValidator, AuthorValidator>();
            services.AddTransient<IBookTypeManager, BookTypeManager>();
            services.AddTransient<IBookTypeStore, BookTypeStore>();
            services.AddTransient<IBookTypeValidator, BookTypeValidator>();
        }
    }
}
