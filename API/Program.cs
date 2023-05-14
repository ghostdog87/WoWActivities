using API.Extensions;
using API.Middleware;
using API.SignalR;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapFallbackToController("Index", "Fallback");

using IServiceScope scope = app.Services.CreateScope();
IServiceProvider services = scope.ServiceProvider;

try
{
    DataContext context = services.GetRequiredService<DataContext>();
    UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();
    context.Database.Migrate();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

app.Run();
