using Microsoft.OpenApi.Models;
using PomoSyncAPI.Backend.TextTools;
using Serilog;
using Serilog.Events;

namespace PomoSyncAPI.Backend;

public static class Executable
{
    public const int KESTREL_HTTP_PORT = 32510;
    public const int KESTREL_HTTPS_PORT = 32500;

    public const string DEPLOY_MODE = "FANTOMLIS_POMOSYNCAPI_DEPLOY_MODE";

    public static string API_VERSION = "v0-dev";
    
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateLogger();
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSerilog();

        builder.WebHost.UseKestrel(kestrel =>
        {
            if (Environment.GetEnvironmentVariable(DEPLOY_MODE).IsSame("https"))
            {
                kestrel.ListenAnyIP(KESTREL_HTTPS_PORT, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            }
            kestrel.ListenAnyIP(KESTREL_HTTP_PORT);
        });

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc(API_VERSION, new OpenApiInfo()
            {
                Title = "PomoSyncAPI",
                Description = "Simple API for syncing Pomodoro timers",
                Version = API_VERSION
            });
        });


        var app = builder.Build();

        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(uiOptions =>
            {
                uiOptions.SwaggerEndpoint($"/swagger/{API_VERSION}/swagger.json", $"PomoSyncAPI {API_VERSION}");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}