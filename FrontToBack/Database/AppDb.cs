using FrontToBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Database
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<Info> FAQ { get; set; }

        public DbSet<Slider> Sliders { get; set; }
    }

}
