using Microsoft.AspNetCore.Builder;
using OnboardingSIGDB1.API.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Extensions
{
    public static class CommitMiddlewareExtension
    {
        public static IApplicationBuilder AddCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CommitMiddleware>();
        }
    }
}
