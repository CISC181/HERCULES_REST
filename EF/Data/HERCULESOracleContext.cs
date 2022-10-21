using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HERCULES.EF.Models;

#nullable disable

namespace HERCULES.EF
{
    public partial class HERCULESOracleContext : DbContext
    {
        public HERCULESOracleContext()
        {
        }

        public HERCULESOracleContext(DbContextOptions<HERCULESOracleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<DeviceCode> DeviceCodes { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HERCULES")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.Property(e => e.Id).HasPrecision(10);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.AccessFailedCount).HasPrecision(10);

                entity.Property(e => e.EmailConfirmed).HasPrecision(1);

                entity.Property(e => e.LockoutEnabled).HasPrecision(1);

                entity.Property(e => e.LockoutEnd).HasPrecision(7);

                entity.Property(e => e.PhoneNumberConfirmed).HasPrecision(1);

                entity.Property(e => e.TwoFactorEnabled).HasPrecision(1);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.Id).HasPrecision(10);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseNo)
                    .HasName("CRSE_PK");

                entity.Property(e => e.CourseNo).HasPrecision(8);

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.Prerequisite).HasPrecision(8);

                entity.Property(e => e.PrerequisiteSchoolId).HasPrecision(8);

                entity.Property(e => e.SchoolId).HasPrecision(8);

                entity.HasOne(d => d.PrerequisiteNavigation)
                    .WithMany(p => p.InversePrerequisiteNavigation)
                    .HasForeignKey(d => d.Prerequisite)
                    .HasConstraintName("CRSE_CRSE_FK");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COURSE_FK2");
            });

            modelBuilder.Entity<DeviceCode>(entity =>
            {
                entity.Property(e => e.CreationTime).HasPrecision(7);

                entity.Property(e => e.Expiration).HasPrecision(7);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SectionId })
                    .HasName("ENR_PK");

                entity.Property(e => e.StudentId).HasPrecision(8);

                entity.Property(e => e.SectionId).HasPrecision(8);

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.FinalGrade).HasPrecision(3);

                entity.Property(e => e.ModifiedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.SchoolId).HasPrecision(8);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENROLLMENT_FK3");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENR_SECT_FK");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENR_STU_FK");
            });

            modelBuilder.Entity<PersistedGrant>(entity =>
            {
                entity.Property(e => e.ConsumedTime).HasPrecision(7);

                entity.Property(e => e.CreationTime).HasPrecision(7);

                entity.Property(e => e.Expiration).HasPrecision(7);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.SchoolId)
                    .HasPrecision(8)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.ModifiedBy).IsUnicode(false);

                entity.Property(e => e.SchoolName).IsUnicode(false);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionId).HasPrecision(8);

                entity.Property(e => e.Capacity).HasPrecision(3);

                entity.Property(e => e.CourseNo).HasPrecision(8);

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.InstructorId).HasPrecision(8);

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.SchoolId).HasPrecision(8);

                entity.Property(e => e.SectionNo).HasPrecision(3);

                entity.HasOne(d => d.CourseNoNavigation)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.CourseNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECT_CRSE_FK");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECTION_FK2");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasPrecision(8);

                entity.Property(e => e.CreatedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.Employer).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Salutation).IsUnicode(false);

                entity.Property(e => e.SchoolId).HasPrecision(8);

                entity.Property(e => e.StreetAddress).IsUnicode(false);

                entity.Property(e => e.Zip).IsUnicode(false);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("STUDENT_FK1");
            });

            modelBuilder.HasSequence("COURSE_SEQ");

            modelBuilder.HasSequence("SECTION_SEQ");

            modelBuilder.HasSequence("STUDENT_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
