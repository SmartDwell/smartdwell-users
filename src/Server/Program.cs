using System.Text.Json.Serialization;
using Adeptik.Hosting.AspNet.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Seljmov.AspNet.Commons.Helpers;
using Seljmov.AspNet.Commons.Options;
using Seljmov.Blazor.Identity.Server.ApiGroups;
using Seljmov.Blazor.Identity.Shared;
using Server;
using Server.ApiGroups;
using Server.Options;
using Server.Services.CodeSender;
using Server.Services.JwtHelper;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

MapsterConfig.Config();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration)
    .ValidateDataAnnotations()
    .ValidateOnStart()
    .Expose(applicationOptions => applicationOptions.CodeTemplateOptions)
    .Expose(applicationOptions => applicationOptions.SmtpClientOptions)
    .Expose(applicationOptions => applicationOptions.JwtOptions)
    .Expose(applicationOptions => applicationOptions.ConfigurationOptions);

builder.Services.AddScoped<IJwtHelper, JwtHelper>();
builder.Services.AddScoped<IEmailCodeSender, EmailSenderService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.BuildWebApplication(
    buildOptions: new BuildOptions
    {
        UseCors = true,
        AuthenticationPolicies = AuthPolicies.AllPolicies,
    });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapUserGroup();
app.MapAuthGroup();
app.MapConfigurationGroup();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();