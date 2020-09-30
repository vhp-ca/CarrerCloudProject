using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
       // public ILoggerFactory MyLoggerFactory { get; private set; }
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobsDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseLoggerFactory(MyLoggerFactory).
                optionsBuilder.UseSqlServer("Server = DESKTOP-61NCPEQ\\HUMBERBRIDGING; Initial Catalog = JOB_PORTAL_DB; Integrated Security = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicantEducationPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<ApplicantJobApplicationPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<ApplicantProfilePoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<ApplicantSkillPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<ApplicantWorkHistoryPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<CompanyDescriptionPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<CompanyJobDescriptionPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<CompanyJobEducationPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<CompanyJobSkillPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<CompanyJobPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<CompanyLocationPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<CompanyProfilePoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<SecurityLoginPoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());
            modelBuilder.Entity<SecurityLoginsRolePoco>(e => e.Property(p => p.TimeStamp).IsRowVersion());

            base.OnModelCreating(modelBuilder);


        }
    }
}
