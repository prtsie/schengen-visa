using System.Reflection;

namespace SchengenVisaApi
{
    /// Provides methods to add services to DI-container
    public static class DependencyInjection
    {
        /// Add needed services
        public static IServiceCollection RegisterServices(this IServiceCollection services)
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
