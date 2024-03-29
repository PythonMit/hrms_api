using HRMS.Api;
using HRMS.Api.Middlewares;
using HRMS.Core.Consts;
using HRMS.DBL.Configuration;
using HRMS.DBL.DbContextConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var environemntName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AuthOptions.CORSPolicy, builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("Content-Disposition");
});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddVersionedApiExplorer(o =>
//{
//    o.GroupNameFormat = "'v'VVV";
//    o.SubstituteApiVersionInUrl = true;
//});
//builder.Services.AddApiVersioning(config =>
//{
//    config.DefaultApiVersion = new ApiVersion(1, 0);
//    config.AssumeDefaultVersionWhenUnspecified = true;
//    config.ReportApiVersions = true;
//});
//var provider = builder.Services.BuildServiceProvider()?.GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen(c =>
{
    var version = builder.Configuration.GetSection("GeneralSettings:Version");
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HRMS API",
        Version = version?.Value ?? "1.0.0", //provider.ApiVersionDescriptions.LastOrDefault()?.ApiVersion.ToString(),
        Description = ""
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Description = "Bearer Authentication with JWT Token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Name = "Token",
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer",
            },
        }, new List <string>()
    }});
    c.MapType<TimeSpan?>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "HRMS.Api.xml");
    if (File.Exists(filePath))
    {
        c.IncludeXmlComments(filePath);
    }
});

if (System.OperatingSystem.IsWindows())
{
    builder.Logging.ClearProviders().AddEventLog(new EventLogSettings()
    {
        LogName = "Application",
        SourceName = builder.Configuration.GetSection("GeneralSettings:LogSource").Value,
    }).AddFilter((x, y) => y >= LogLevel.Critical).AddFilter((x, y) => y >= LogLevel.Error);

}
else
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
}

builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environemntName}.json", true, true);

var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

//Auto Migration
if (builder.Environment.IsDevelopment() || builder.Environment.IsProduction() || builder.Environment.IsStaging())
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var dbcontext = services.GetRequiredService<HRMSDbContext>();
        var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        dbcontext.Database.SetConnectionString(defaultConnectionString);
        dbcontext.Database.Migrate();
        await DataSeeder.SeedGlobalSuperAdminData(dbcontext, builder.Configuration);
        await DataSeeder.SeedDemoEmployeeData(dbcontext, builder.Configuration);
        await DataSeeder.SeedAdminUserData(dbcontext, builder.Configuration);
    }
}

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRMS API v1");
    c.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(AuthOptions.CORSPolicy);

// Do not remove this code
//app.UseCookiePolicy(new CookiePolicyOptions()
//{
//    MinimumSameSitePolicy = SameSiteMode.None,
//    HttpOnly = HttpOnlyPolicy.Always,
//    Secure = CookieSecurePolicy.Always,
//});
//app.UseMiddleware<JwtCookieMiddleware>();
//app.UseMiddleware<MaintananceModeMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ActivityLogMiddleware>();

app.UseRequestLocalization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
