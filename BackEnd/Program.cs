using PomoSyncAPI.BackEnd.TextTools;

namespace PomoSyncAPI.BackEnd;

public static class Executable
{
    public const int KESTREL_HTTP_PORT = 32510;
    public const int KESTREL_HTTPS_PORT = 32500;

    public const string DEPLOY_MODE = "FANTOMLIS_POMOSYNCAPI_DEPLOY_MODE";
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(kestrel =>
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
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}