using EmailSender.Domain.DTO;
using EmailSender.Persistence;
using EmailSender.Services.Implementation;
using EmailSender.Services.Interface;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var myMailSettings = builder.Configuration.GetSection("MailSettings").Get<MailSettings>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddControllersWithViews();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton(myMailSettings);

builder.Services. AddScoped<IMailMessgesService, MailMessagesService>();

//builder.Services.
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
