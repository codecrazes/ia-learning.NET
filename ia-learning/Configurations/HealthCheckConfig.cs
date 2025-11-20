namespace ia_learning.Configurations
{
    public static class HealthCheckConfig
    {
        public static IServiceCollection AddHealthCheckSetup(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }

        public static IApplicationBuilder UseHealthCheckSetup(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });

            return app;
        }
    }
}
