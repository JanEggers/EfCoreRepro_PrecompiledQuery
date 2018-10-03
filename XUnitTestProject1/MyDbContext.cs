using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace XUnitTestProject1
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
    }

    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
