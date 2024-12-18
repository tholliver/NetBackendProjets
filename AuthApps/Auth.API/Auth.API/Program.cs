using ActionFilters;
using Auth.API.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repository;

var builder = WebApplication.CreateBuilder(args);

{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var xmlFilePath = string.Concat(Directory.GetCurrentDirectory(), "/nlog.config");
    LogManager.Setup().LoadConfigurationFromXml(xmlFilePath);

    builder.Services.ConfigureCors();
    builder.Services.ConfigureIISIntegration();
    builder.Services.ConfigureLoggerService();

    builder.Services.AddAuthentication();
    builder.Services.ConfigureIdentity();

    builder.Services.ConfigureJWT(builder.Configuration);

    builder.Services.AddDbContext<RepositoryContext>(opts =>
        opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.ConfigureRepositoryManager();
    builder.Services.ConfigureServiceManager();

    builder.Services.AddScoped<ValidationFilterAttribute>();

    builder.Services.AddControllers()
            .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
}

var app = builder.Build();
{

    var logger = app.Services.GetRequiredService<ILoggerManager>();
    app.ConfigureExceptionHandler(logger);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else { app.UseHsts(); }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.All
    });

    app.UseCors("CorsPolicy");

    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

    app.MapControllers();
}
