﻿namespace SchengenVisaApi;

/// Provides methods for configuring middleware
public static class PipelineRequest
{
    /// Configure middleware
    public static WebApplication ConfigurePipelineRequest(this WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI();

        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }
}