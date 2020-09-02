using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Core.Notifications;

namespace OnboardingSIGDB1.IOC
{
    public static class NucleoIOC
    {
        public static void AddNucleo(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();
        }
    }
}
