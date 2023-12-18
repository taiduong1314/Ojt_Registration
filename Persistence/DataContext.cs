using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Persistence
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<FptStaff> FptStaffs { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<OjtReports> OjtReports { get; set; }
        public virtual DbSet<RecruimentApply> RecruimentApplies { get; set; }
        public virtual DbSet<RecruitmentInformation> RecruitmentInformations { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<CompanyAccount> CompanyAccounts {get; set; }
        public virtual DbSet<Semester> Semesters {get; set; }
        public virtual DbSet<MajorCompany> MajorCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=OJT_Registration;user id=sa;password=vinhle123;Trusted_Connection=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("tbl_company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.WebSite)
                    .HasColumnType("text")
                    .HasColumnName("web_site");

                entity.Property(e => e.Address)
                    .HasColumnType("ntext")
                    .HasColumnName("address");

                entity.Property(e => e.CompanyName)
                    .HasColumnType("ntext")
                    .HasColumnName("company_name");
            });

            modelBuilder.Entity<CompanyAccount>(entity =>
            {
                entity.ToTable("tbl_company_accounts");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Fullname)
                    .HasColumnType("nvarchar(MAX)")
                    .HasColumnName("Fullname");

                entity.Property(e => e.Email)
                    .HasColumnType("nvarchar(MAX)")
                    .HasColumnName("Email");

                entity.Property(e => e.Code)
                    .HasColumnType("nvarchar(MAX)")
                    .HasColumnName("Code");
            });

            modelBuilder.Entity<FptStaff>(entity =>
            {
                entity.ToTable("tbl_fpt_staff");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(MAX)")
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("fullname");

                entity.Property(e => e.Image)
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("tbl_major");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MajorName)
                    .HasColumnType("varchar(MAX)")
                    .HasColumnName("major_name");
            });

            modelBuilder.Entity<OjtReports>(entity =>
            {
                entity.ToTable("tbl_ojt_report");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Division)
                    .HasColumnType("ntext")
                    .HasColumnName("division");

                entity.Property(e => e.LineManagerName)
                    .HasColumnType("ntext")
                    .HasColumnName("line_manager_name");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.OnWorkDate).HasColumnName("on_work_date");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.WorkSortDescription)
                    .HasColumnType("ntext")
                    .HasColumnName("work_sort_description");

                entity.Property(e => e.Public_Date)
                    .HasColumnType("datetime")
                    .HasColumnName("public_date");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.OjtReports)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ojt_report_tbl_company");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.OjtReports)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ojt_report_tbl_student");
            });

            modelBuilder.Entity<RecruimentApply>(entity =>
            {
                entity.ToTable("tbl_recruiment_apply");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cv)
                    .HasColumnType("varchar(MAX)")
                    .HasColumnName("cv");

                entity.Property(e => e.RecruimentInformationId).HasColumnName("recruiment_information_id");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registration_date");

                entity.Property(e => e.Status)
                    .HasColumnType("text")
                    .HasColumnName("status");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.CoverLetter)
                    .HasColumnType("ntext")
                    .HasColumnName("cover_letter");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.HasOne(d => d.RecruimentInformation)
                    .WithMany(p => p.RecruimentApplies)
                    .HasForeignKey(d => d.RecruimentInformationId)
                    .HasConstraintName("FK_tbl_recruiment_apply_tbl_recruitment_information");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RecruimentApplies)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_tbl_recruiment_apply_tbl_student");
            });

            modelBuilder.Entity<RecruitmentInformation>(entity =>
            {
                entity.ToTable("tbl_recruitment_information");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Content)
                    .HasColumnType("ntext")
                    .HasColumnName("content");

                entity.Property(e => e.Deadline)
                    .HasColumnType("datetime")
                    .HasColumnName("deadline");

                entity.Property(e => e.Area)
                    .HasColumnType("ntext")
                    .HasColumnName("area");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.Salary)
                    .HasColumnType("ntext")
                    .HasColumnName("salary");

                entity.Property(e => e.Topic)
                    .HasColumnType("ntext")
                    .HasColumnName("topic");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.RecruitmentInformations)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_tbl_recruitment_information_tbl_company");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("tbl_student");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.Gpa).HasColumnName("gpa");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.StudentCode)
                    .HasColumnType("varchar(MAX)")
                    .HasColumnName("student_code");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(MAX)")
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasColumnType("text")
                    .HasColumnName("phone");

                entity.Property(e => e.Term).HasColumnName("term");

                entity.Property(e => e.Fullname)
                    .HasColumnType("ntext")
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender)
                    .HasColumnType("text")
                    .HasColumnName("gender");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_tbl_student_tbl_company");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_student_tbl_major");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("tbl_semesters");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("nvarchar(max)");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime2(7)")
                    .HasColumnName("EndDate");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime2(7)")
                    .HasColumnName("StartDate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
