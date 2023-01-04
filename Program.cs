using Microsoft.EntityFrameworkCore;
using task_tracker;
using task_tracker.Facades;
using task_tracker.Hub;
using task_tracker.Interfaces;
using task_tracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUsersFacade, UsersFacade>();
builder.Services.AddScoped<ITasksFacade, TasksFacade>();

builder.Services.AddHostedService<HubTimer>();
builder.Services.AddScoped<ISuccessesFacade, SuccessesFacade>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CORSPolicy");
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<SuccessHub>("/successes");
});
app.UseHttpsRedirection();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();

