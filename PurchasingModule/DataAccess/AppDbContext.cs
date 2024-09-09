using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PurchasingModule.DataAccess.Models;

namespace PurchasingModule.DataAccess
{
    public class AppDbContext:DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<PurchaseReceipt> PurchaseReceipts { get; set; }
        public DbSet<PurchaseReceiptItem> PurchaseReceiptItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectionString = "Data Source=DESKTOP-1SLE0TS;Database=PurchasingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseOrderItem>()
            .Property(p => p.UnitPrice)
            .HasColumnType("decimal(10, 4)");
            // Configure the foreign key for PurchaseReceipt with no cascading delete
            modelBuilder.Entity<PurchaseReceiptItem>()
                .HasOne(i => i.PurchaseReceipt)
                .WithMany(r => r.PurchaseReceiptItems)
                .HasForeignKey(i => i.PurchaseReceiptId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            // Configure the foreign key for PurchaseOrderItem
            modelBuilder.Entity<PurchaseReceiptItem>()
                .HasOne(i => i.PurchaseOrderItem)
                .WithMany() // Assuming no navigation collection in PurchaseOrderItem
                .HasForeignKey(i => i.PurchaseOrderItemId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete here is optional

            base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            string ConnectionString = "Data Source=DESKTOP-1SLE0TS;Database=PurchasingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            optionsBuilder.UseSqlServer(ConnectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
