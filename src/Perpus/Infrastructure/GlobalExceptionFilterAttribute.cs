using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Perpus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perpus.Infrastructure
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public GlobalExceptionFilterAttribute(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DbUpdateException)
            {
                var exMessage = context.Exception.GetBaseException().Message.Replace("\r\n", string.Empty);
                var result = PerpusResult.Failed(exMessage);
                var error = new JsonResult(result.Errors);
                error.StatusCode = 500;
                context.Result = error;
            }
            if (!_hostingEnvironment.IsDevelopment())
            {
                // do nothing
                return;
            }
        }
    }
}
