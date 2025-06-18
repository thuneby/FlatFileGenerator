using FlatFileGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatFileGenerator.DataAccess.Models
{
    public class FlatFileContext(DbContextOptions<FlatFileContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    }
}
