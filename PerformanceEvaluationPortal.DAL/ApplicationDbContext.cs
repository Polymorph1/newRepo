using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PerformanceEvaluationPortal.DomainModel;

namespace PerformanceEvaluationPortal.DAL
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext():base("PerformanceEvaluationPortal")
        {
                
        }

        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<PerformanceEvaluation> PerformanceEvaluations { get; set; }
        public DbSet<PerformanceEvaluationSkill> PerformanceEvaluationsSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateDuration> TemplateDurations { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<PerformanceEvaluation>()
            //    .HasOptional(s => s.ConsultantComment)
            //    .WithRequired(ad => ad.PerformanceEvaluation);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.UsersIWriteReviewFor)
                .WithOptional(x => x.Reviewer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(x => x.UsersIManage)
               .WithOptional(x => x.Manager)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(x => x.ConsultantPerformanceEvaluations)
               .WithOptional(x => x.Consultant)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(x => x.ReviewerPerformanceEvaluations)
               .WithOptional(x => x.Reviewer)
               .WillCascadeOnDelete(false);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
