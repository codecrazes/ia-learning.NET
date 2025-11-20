using Microsoft.OpenApi.Models;

namespace ia_learning.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "IA Learning API - V1",
                    Version = "v1"
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "IA Learning API - V2",
                    Version = "v2"
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IA Learning API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "IA Learning API V2");
                c.DocumentTitle = "IA Learning API";
            });

            return app;
        }
    }
}
