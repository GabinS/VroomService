namespace VroomService.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VroomServiceModel : DbContext
    {
        public VroomServiceModel()
            : base("name=VroomServiceModel")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Cars)
                .WithRequired(e => e.Brand)
                .HasForeignKey(e => e.Brand_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Car)
                .HasForeignKey(e => e.Car_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
