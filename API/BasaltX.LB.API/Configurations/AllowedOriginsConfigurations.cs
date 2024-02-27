namespace BasaltX.LB.API.Configurations
{
    internal static class AllowedOriginsConfigurations
    {
        /// <summary>
        /// This function will protect the api from unknown request
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="configuration"></param>
        public static void AddAllowedOriginsConfiguration(this WebApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            var allowedOrigins = configuration["AllowedOrigins"]?.Split(",");

            if (allowedOrigins is not null)
            {
                applicationBuilder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: "CorsPolicy",
                     policy =>
                     {
                         policy
                         .WithOrigins(allowedOrigins)
                         .SetIsOriginAllowed(x => _ = true)
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                     });
                });
            }
        }
    }
}
