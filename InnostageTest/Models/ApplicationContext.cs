using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnostageTest.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<Request> Requests { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>(e =>
            {
                e.HasKey(r => r.RequestId);

                e.Property(r => r.ClientName)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(r => r.Phone)
                    .IsRequired()
                    .HasMaxLength(12);

                e.Property(r => r.RequestTime)
                    .IsRequired();

                e.Property(r => r.Text)
                    .IsRequired();

                e.HasOne(r => r.RequestStatus)
                    .WithMany(s => s.Requests)
                    .IsRequired();

                e.HasOne(r => r.Creator)
                    .WithMany(u => u.Requests)
                    .IsRequired();
            });

            modelBuilder.Entity<RequestStatus>(e =>
            {
                e.HasKey(s => s.RequestStatusId);

                e.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                e.HasData(new RequestStatus[]
                {
                    new RequestStatus() { Name = "Зарегистрировано", RequestStatusId = 1 },
                    new RequestStatus() { Name = "Исполнено", RequestStatusId = 2 },
                    new RequestStatus() { Name = "Не исполнено", RequestStatusId = 3 }
                }); 
            });
        }
    }
}
