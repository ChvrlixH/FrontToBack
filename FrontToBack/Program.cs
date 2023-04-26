using FrontToBack.Database;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDb>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-0ETCTHF\\MSSQLSERVER01;Database=ProniaDB;Trusted_Connection=True");
            }
            );

            builder.Services.AddControllersWithViews();
           
            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}