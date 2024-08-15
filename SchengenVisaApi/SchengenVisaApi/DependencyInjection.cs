using System.Reflection;
using Infrastructure;

namespace SchengenVisaApi
{
    /// Provides methods to add services to DI-container
    public static class DependencyInjection
    {
        /// Add needed services
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddInfrastructure()
                .AddPresentation();

            return services;
        }

        /// Add services needed for Presentation layer
        private static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }
    }
}
