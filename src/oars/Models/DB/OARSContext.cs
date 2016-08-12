using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace oars.Models.DB
{
    public partial class OARSContext : DbContext
    {
        public OARSContext(DbContextOptions<OARSContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.HasKey(e => e.AptNo)
                    .HasName("PK__apartmen__01F2B60607B8CDFA");

                entity.ToTable("apartment");

                entity.Property(e => e.AptNo)
                    .HasColumnName("apt_no")
                    .ValueGeneratedNever();

                entity.Property(e => e.AptDepositAmt).HasColumnName("apt_deposit_amt");

                entity.Property(e => e.AptRentAmt)
                    .HasColumnName("apt_rent_amt")
                    .HasColumnType("decimal");

                entity.Property(e => e.AptStatus)
                    .IsRequired()
                    .HasColumnName("apt_status")
                    .HasColumnType("char(1)");

                entity.Property(e => e.AptType).HasColumnName("apt_type");

                entity.Property(e => e.AptUtility)
                    .IsRequired()
                    .HasColumnName("apt_utility")
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetUserRoles_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserRoles_UserId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Complaints>(entity =>
            {
                entity.HasKey(e => e.ComplaintNo)
                    .HasName("PK__complain__A771DE771067018B");

                entity.ToTable("complaints");

                entity.Property(e => e.ComplaintNo).HasColumnName("complaint_no");

                entity.Property(e => e.AptComplaint)
                    .HasColumnName("apt_complaint")
                    .HasColumnType("text");

                entity.Property(e => e.AptNo).HasColumnName("apt_no");

                entity.Property(e => e.ComplaintDate)
                    .HasColumnName("complaint_date")
                    .HasColumnType("date");

                entity.Property(e => e.RentalComplaint)
                    .HasColumnName("rental_complaint")
                    .HasColumnType("text");

                entity.Property(e => e.RentalNo).HasColumnName("rental_no");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("char(1)");

                entity.HasOne(d => d.AptNoNavigation)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.AptNo)
                    .HasConstraintName("fk_complaints_apartment_no");

                entity.HasOne(d => d.RentalNoNavigation)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.RentalNo)
                    .HasConstraintName("fk_complaints_rental_no");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => e.RentalNo)
                    .HasName("PK__rental__67DA26D193211089");

                entity.ToTable("rental");

                entity.Property(e => e.RentalNo).HasColumnName("rental_no");

                entity.Property(e => e.AptNo).HasColumnName("apt_no");

                entity.Property(e => e.CancelDate)
                    .HasColumnName("cancel_date")
                    .HasColumnType("date");

                entity.Property(e => e.LeaseEnd)
                    .HasColumnName("lease_end")
                    .HasColumnType("date");

                entity.Property(e => e.LeaseStart)
                    .HasColumnName("lease_start")
                    .HasColumnType("date");

                entity.Property(e => e.LeaseType).HasColumnName("lease_type");

                entity.Property(e => e.RenewalDate)
                    .HasColumnName("renewal_date")
                    .HasColumnType("date");

                entity.Property(e => e.RentalDate)
                    .HasColumnName("rental_date")
                    .HasColumnType("date");

                entity.Property(e => e.RentalStatus)
                    .IsRequired()
                    .HasColumnName("rental_status")
                    .HasColumnType("char(1)");

                entity.Property(e => e.StaffNo).HasColumnName("staff_no");

                entity.HasOne(d => d.AptNoNavigation)
                    .WithMany(p => p.Rental)
                    .HasForeignKey(d => d.AptNo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rental_apt_no");

                entity.HasOne(d => d.StaffNoNavigation)
                    .WithMany(p => p.Rental)
                    .HasForeignKey(d => d.StaffNo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rental_staff_no");
            });

            modelBuilder.Entity<RentalInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo)
                    .HasName("PK__rental_i__F58CA1E397983B90");

                entity.ToTable("rental_invoice");

                entity.Property(e => e.InvoiceNo).HasColumnName("invoice_no");

                entity.Property(e => e.CcAmt)
                    .HasColumnName("cc_amt")
                    .HasColumnType("decimal");

                entity.Property(e => e.CcExpDate)
                    .HasColumnName("cc_exp_date")
                    .HasColumnType("date");

                entity.Property(e => e.CcNo)
                    .IsRequired()
                    .HasColumnName("cc_no")
                    .HasColumnType("char(16)");

                entity.Property(e => e.CcType)
                    .IsRequired()
                    .HasColumnName("cc_type")
                    .HasColumnType("varchar(16)");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnName("invoice_date")
                    .HasColumnType("date");

                entity.Property(e => e.InvoiceDue)
                    .HasColumnName("invoice_due")
                    .HasColumnType("decimal");

                entity.Property(e => e.RentalNo).HasColumnName("rental_no");

                entity.HasOne(d => d.RentalNoNavigation)
                    .WithMany(p => p.RentalInvoice)
                    .HasForeignKey(d => d.RentalNo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rental_invoice_rental_no");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.StaffNo)
                    .HasName("PK__staff__1962B213512CEE13");

                entity.ToTable("staff");

                entity.Property(e => e.StaffNo).HasColumnName("staff_no");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("fname")
                    .HasColumnType("varchar(35)");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("char(1)");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("lname")
                    .HasColumnType("varchar(35)");

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(e => e.TenantSs)
                    .HasName("PK__tenant__D6F2E9FCE68892D7");

                entity.ToTable("tenant");

                entity.Property(e => e.TenantSs)
                    .HasColumnName("tenant_ss")
                    .HasColumnType("char(9)");

                entity.Property(e => e.EmployerName)
                    .HasColumnName("employer_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("char(1)");

                entity.Property(e => e.HomePhone)
                    .HasColumnName("home_phone")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Marital)
                    .IsRequired()
                    .HasColumnName("marital")
                    .HasColumnType("char(1)");

                entity.Property(e => e.RentalNo).HasColumnName("rental_no");

                entity.Property(e => e.TenantDob)
                    .HasColumnName("tenant_dob")
                    .HasColumnType("date");

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasColumnName("tenant_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.WorkPhone)
                    .HasColumnName("work_phone")
                    .HasColumnType("char(10)");

                entity.HasOne(d => d.RentalNoNavigation)
                    .WithMany(p => p.Tenant)
                    .HasForeignKey(d => d.RentalNo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_tenant_rental_no");
            });

            modelBuilder.Entity<TenantAuto>(entity =>
            {
                entity.HasKey(e => e.LicenseNo)
                    .HasName("PK__tenant_a__BBBB6DA73393E3AC");

                entity.ToTable("tenant_auto");

                entity.Property(e => e.LicenseNo)
                    .HasColumnName("license_no")
                    .HasColumnType("char(7)");

                entity.Property(e => e.AutoColor)
                    .HasColumnName("auto_color")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.AutoMake)
                    .IsRequired()
                    .HasColumnName("auto_make")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.AutoModel)
                    .IsRequired()
                    .HasColumnName("auto_model")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.AutoYear).HasColumnName("auto_year");

                entity.Property(e => e.TenantSs)
                    .IsRequired()
                    .HasColumnName("tenant_ss")
                    .HasColumnType("char(9)");

                entity.HasOne(d => d.TenantSsNavigation)
                    .WithMany(p => p.TenantAuto)
                    .HasForeignKey(d => d.TenantSs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_tenant_auto_tenant_ss");
            });

            modelBuilder.Entity<TenantFamily>(entity =>
            {
                entity.HasKey(e => e.FamilySs)
                    .HasName("PK__tenant_f__28828CEA0DC2384E");

                entity.ToTable("tenant_family");

                entity.Property(e => e.FamilySs)
                    .HasColumnName("family_ss")
                    .HasColumnType("char(9)");

                entity.Property(e => e.Child)
                    .IsRequired()
                    .HasColumnName("child")
                    .HasColumnType("char(1)");

                entity.Property(e => e.Divorced)
                    .IsRequired()
                    .HasColumnName("divorced")
                    .HasColumnType("char(1)");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("char(1)");

                entity.Property(e => e.Single)
                    .IsRequired()
                    .HasColumnName("single")
                    .HasColumnType("char(1)");

                entity.Property(e => e.Spouse)
                    .IsRequired()
                    .HasColumnName("spouse")
                    .HasColumnType("char(1)");

                entity.Property(e => e.TenantSs)
                    .IsRequired()
                    .HasColumnName("tenant_ss")
                    .HasColumnType("char(9)");

                entity.HasOne(d => d.TenantSsNavigation)
                    .WithMany(p => p.TenantFamily)
                    .HasForeignKey(d => d.TenantSs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_tenant_family_tenant_ss");
            });

            modelBuilder.Entity<Testimonials>(entity =>
            {
                entity.HasKey(e => e.TestimonialId)
                    .HasName("PK__testimon__2E3B190C10D7506E");

                entity.ToTable("testimonials");

                entity.Property(e => e.TestimonialId).HasColumnName("testimonial_id");

                entity.Property(e => e.TenantSs)
                    .HasColumnName("tenant_ss")
                    .HasColumnType("char(9)");

                entity.Property(e => e.TestimonialContent)
                    .IsRequired()
                    .HasColumnName("testimonial_content")
                    .HasColumnType("text");

                entity.Property(e => e.TestimonialDate)
                    .HasColumnName("testimonial_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.TenantSsNavigation)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.TenantSs)
                    .HasConstraintName("fk_testimonials_tenant_ss");
            });
        }

        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Complaints> Complaints { get; set; }
        public virtual DbSet<Rental> Rental { get; set; }
        public virtual DbSet<RentalInvoice> RentalInvoice { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Tenant> Tenant { get; set; }
        public virtual DbSet<TenantAuto> TenantAuto { get; set; }
        public virtual DbSet<TenantFamily> TenantFamily { get; set; }
        public virtual DbSet<Testimonials> Testimonials { get; set; }
    }
}