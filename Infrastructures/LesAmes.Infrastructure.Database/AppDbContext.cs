﻿using LesAmes.Domain.Authentication;
using LesAmes.Domain.Hobbies;
using LesAmes.Domain.Souls;
using LesAmes.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LesAmes.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
    IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Soul> Souls { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<HobbyCategory> HobbyCategories { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<ImpactFamily> ImpactFamilies { get; set; }
    public DbSet<AgeRange> AgeRanges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            // Each User can have many entries in the UserRole join table  
            b.HasDiscriminator<string>("UserType")
                .HasValue<ApplicationUser>("User")
                .HasValue<Tutor>("Tutor");
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<ApplicationUserRole>(entity =>
        {
            entity.ToTable("AspNetUserRoles");
            entity.HasKey(r => new { r.UserId, r.RoleId });

            entity.HasOne(r => r.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            entity.HasOne(r => r.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();
        });


        modelBuilder.Entity<Tutor>(t =>
            {
                t.Property(x => x.BirthYear)
                    .HasColumnName("BirthYear")
                    .IsRequired();
                t.HasMany(t => t.Mentees)
                    .WithOne(s => s.Tutor)
                    .HasForeignKey(s => s.TutorId)
                    .IsRequired(false);
                t.HasMany(t => t.Hobbies)
                    .WithMany(h => h.Tutors)
                    .UsingEntity(j =>
                            j.ToTable("TutorHobbies")  // table de jonction explicite (optionnel)
                    );
            });

        modelBuilder.Entity<Soul>(b =>
        {
            b.HasKey(s => s.Id);
            b.HasMany(e => e.Hobbies)
                .WithMany(h => h.Souls);
            b.HasOne(s => s.AgeRange);
        });

        modelBuilder.Entity<Hobby>(b =>
        {
            b.HasKey(s => s.Id);
        });

        modelBuilder.Entity<ImpactFamily>(b =>
        {
            b.HasKey(s => s.Id);
        });

        modelBuilder.Entity<HobbyCategory>(b =>
        {
            b.HasKey(t => t.Id);
            b.HasMany(t => t.Hobbies)
                .WithOne(s => s.HobbyCategory)
                .HasForeignKey(s => s.HobbyCategoryId)
                .IsRequired(true);
        });


    }
}
