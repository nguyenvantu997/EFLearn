using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore_Library
{
    public class InventoryManageDbContext : DbContext
    {
        private const string _systemUserId = "2fd28110-93d0-427d-9207-d55dbca680fa";
        private static IConfigurationRoot _configuration;
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryDetail> CategoryDetails { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GetItemsForListingDto> ItemsForListing { get; set; }
        public virtual DbSet<AllItemsPipeDelimitedStringDTO> AllItemsOutput { get; set; }
        public virtual DbSet<GetItemsTotalValueDto> GetItemsTotalValues { get; set; }
        public DbSet<FullItemDetailDto> FullItemDetailDtos { get; set; }

        public InventoryManageDbContext() { }

        public InventoryManageDbContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            var tracker = ChangeTracker;
            foreach (var entry in tracker.Entries())
            {
                if (entry.Entity is FullAuditModel)
                {
                    var referenceEntity = entry.Entity as FullAuditModel;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            referenceEntity.CreatedDate = DateTime.UtcNow;
                            referenceEntity.LastModifiedDate = DateTime.UtcNow;
                            if (string.IsNullOrWhiteSpace(referenceEntity.CreatedByUserId))
                            {
                                referenceEntity.CreatedByUserId = _systemUserId;
                                referenceEntity.LastModifiedUserId = _systemUserId;
                            }
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            referenceEntity.LastModifiedDate = DateTime.UtcNow;
                            if (string.IsNullOrWhiteSpace(referenceEntity.LastModifiedUserId))
                            {
                                referenceEntity.LastModifiedUserId = _systemUserId;
                            }
                            break;
                        default:
                            break;
                    }
                }
                System.Diagnostics.Debug.WriteLine($"{entry.Entity} has state {entry.State}");
            }
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();
                var cnstr = _configuration.GetConnectionString("InventoryManager");
                optionsBuilder.UseSqlServer(cnstr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany(x => x.Players)
                .WithMany(p => p.Items)
                .UsingEntity<Dictionary<string, object>>(
                    "ItemPlayers",
                    ip => ip.HasOne<Player>()
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("FK_ItemPlayer_Players_PlayerId")
                        .OnDelete(DeleteBehavior.Cascade),
                    ip => ip.HasOne<Item>()
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .HasConstraintName("FK_PlayerItem_Items_ItemId")
                        .OnDelete(DeleteBehavior.ClientCascade));

            modelBuilder.Entity<ItemGenre>()
                        .HasIndex(ig => new { ig.ItemId, ig.GenreId })
                        .IsUnique();

            modelBuilder.Entity<GetItemsForListingDto>(x =>
            {
                x.HasNoKey();
                x.ToView("ItemsForListing");
            });

            modelBuilder.Entity<AllItemsPipeDelimitedStringDTO>(x =>
            {
                x.HasNoKey();
                x.ToView("AllItemsOutput");
            });

            modelBuilder.Entity<GetItemsTotalValueDto>(x =>
            {
                x.HasNoKey();
                x.ToView("GetItemsTotalValues");
            });

            modelBuilder.Entity<FullItemDetailDto>(x =>
            {
                x.HasNoKey();
                x.ToView("FullItemDetailDtos");
            });


            var genreCreateDate = new DateTime(2021, 01, 01);

            modelBuilder.Entity<Genre>(x =>
            {
                x.HasData(
                    new Genre() { Id = 1, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Fantasy", CreatedByUserId = _systemUserId, LastModifiedUserId = _systemUserId, LastModifiedDate = genreCreateDate },
                    new Genre() { Id = 2, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Sci/Fi", CreatedByUserId = _systemUserId, LastModifiedUserId = _systemUserId, LastModifiedDate = genreCreateDate },
                    new Genre() { Id = 3, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Horror", CreatedByUserId = _systemUserId, LastModifiedUserId = _systemUserId, LastModifiedDate = genreCreateDate },
                    new Genre() { Id = 4, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Comedy", CreatedByUserId = _systemUserId, LastModifiedUserId = _systemUserId, LastModifiedDate = genreCreateDate },
                    new Genre() { Id = 5, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Drama", CreatedByUserId = _systemUserId, LastModifiedUserId = _systemUserId, LastModifiedDate = genreCreateDate }
                );
            });


        }

    }
}