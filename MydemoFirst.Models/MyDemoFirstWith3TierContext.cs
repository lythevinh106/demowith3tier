using DTOShared.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MydemoFirst.Models.Modules.Category.Models;
using MydemoFirst.Models.Modules.Order.Models;
using MydemoFirst.Models.Modules.ProductOrder.Models;
using MydemoFirst.Models.Modules.Products.Models;
using MydemoFirst.Models.Modules.Room.Models;
using MydemoFirst.Models.Modules.RoomMember.Models;
using MydemoFirst.Models.Modules.User.Models;
using MydemoFirst.Models.Modules.UserRefreshToken.Models;

namespace MydemoFirst.Models
{
    public class MyDemoFirstWith3TierContext : IdentityDbContext<Modules.User.Models.User>
    {
        public MyDemoFirstWith3TierContext(DbContextOptions<MyDemoFirstWith3TierContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> RefreshToken { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomMember> RoomMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasMany(c => c.Products).WithOne(p => p.Category);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                //relative category
                entity.HasOne(p => p.Category)
                .WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);

                // relative producrOrder
                entity.HasMany(p => p.ProductOrders)
                .WithOne(po => po.Product);
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(po => new { po.ProductId, po.OrderId });

                entity.HasOne(po => po.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductId);

                entity.HasOne(po => po.Order)
               .WithMany(o => o.ProductOrders).HasForeignKey(po => po.OrderId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                //relative user
                entity.HasOne(o => o.User)
                .WithMany(u => u.Orders).HasForeignKey(o => o.UserId);

                //relative product order
                entity.HasMany(o => o.ProductOrders)
                .WithOne(po => po.Order);
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.HasKey(urt => urt.UserId);
                entity.Property(urt => urt.UserId).ValueGeneratedNever();

                entity.HasOne(urt => urt.User)
                    .WithOne(u => u.UserRefreshToken)
                    .HasForeignKey<UserRefreshToken>(urt => urt.UserId);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasMany(r => r.RoomMembers).WithOne(rm => rm.Room);
            });

            modelBuilder.Entity<RoomMember>(entity =>
            {
                entity.HasKey(rm => new { rm.UserId, rm.RoomId });

                entity.HasOne(rm => rm.User).WithMany(u => u.RoomMembers).HasForeignKey(rm => rm.UserId);
                entity.HasOne(rm => rm.Room).WithMany(r => r.RoomMembers).HasForeignKey(rm => rm.RoomId);

            });

            ///seed

            var entities = typeof(ISeedable).Assembly.GetTypes().Where(x => typeof(ISeedable).IsAssignableFrom(x))
                                .Where(x => !x.IsInterface).ToList();

            foreach (var m in entities)
            {
                var entity = modelBuilder.Entity(m);

                Console.WriteLine(m);

                if (typeof(ISeedable).IsAssignableFrom(m))
                {
                    var inst = Activator.CreateInstance(m) as ISeedable;
                    var data = inst?.Seed();

                    foreach (var d in data)
                        entity.HasData(d);
                }
            }
        }
    }
}