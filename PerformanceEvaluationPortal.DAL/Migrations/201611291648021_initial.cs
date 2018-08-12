namespace PerformanceEvaluationPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPositions",
                c => new
                    {
                        JobPositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.JobPositionId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmploymentDate = c.DateTime(),
                        ReviewerId = c.String(maxLength: 128),
                        ManagerId = c.String(maxLength: 128),
                        JobTitleId = c.Int(),
                        JobPositionId = c.Int(),
                        TemplateId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobTitles", t => t.JobTitleId)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .ForeignKey("dbo.JobPositions", t => t.JobPositionId)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ReviewerId)
                .Index(t => t.ReviewerId)
                .Index(t => t.ManagerId)
                .Index(t => t.JobTitleId)
                .Index(t => t.JobPositionId)
                .Index(t => t.TemplateId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PerformanceEvaluations",
                c => new
                    {
                        PerformanceEvaluationId = c.Int(nullable: false, identity: true),
                        ConsultantId = c.String(maxLength: 128),
                        ReviewerId = c.String(maxLength: 128),
                        JobTitleId = c.Int(nullable: false),
                        JobPositionId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsSubmitted = c.Boolean(nullable: false),
                        SubmittedDate = c.DateTime(),
                        ConsultantFirstName = c.String(),
                        ConsultantLastName = c.String(),
                        ReviewerFirstName = c.String(),
                        ReviewerLastName = c.String(),
                        ConsultantComment = c.String(),
                        SignedOnDateByConsultant = c.DateTime(),
                    })
                .PrimaryKey(t => t.PerformanceEvaluationId)
                .ForeignKey("dbo.JobPositions", t => t.JobPositionId, cascadeDelete: true)
                .ForeignKey("dbo.JobTitles", t => t.JobTitleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ConsultantId)
                .ForeignKey("dbo.AspNetUsers", t => t.ReviewerId)
                .Index(t => t.ConsultantId)
                .Index(t => t.ReviewerId)
                .Index(t => t.JobTitleId)
                .Index(t => t.JobPositionId);
            
            CreateTable(
                "dbo.JobTitles",
                c => new
                    {
                        JobTitleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.JobTitleId);
            
            CreateTable(
                "dbo.PerformanceEvaluationSkills",
                c => new
                    {
                        PerformanceEvaluationSkillId = c.Int(nullable: false, identity: true),
                        PESkillComment = c.String(),
                        Grade = c.String(),
                        SkillId = c.Int(nullable: false),
                        PerformanceEvaluationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PerformanceEvaluationSkillId)
                .ForeignKey("dbo.PerformanceEvaluations", t => t.PerformanceEvaluationId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.SkillId)
                .Index(t => t.PerformanceEvaluationId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                        SkillDescription = c.String(),
                    })
                .PrimaryKey(t => t.SkillId);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        TemplateId = c.Int(nullable: false, identity: true),
                        TemplateName = c.String(),
                        IsArchived = c.Boolean(nullable: false),
                        TemplateDurationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TemplateId)
                .ForeignKey("dbo.TemplateDurations", t => t.TemplateDurationId, cascadeDelete: true)
                .Index(t => t.TemplateDurationId);
            
            CreateTable(
                "dbo.TemplateDurations",
                c => new
                    {
                        TemplateDurationId = c.Int(nullable: false, identity: true),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TemplateDurationId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TemplateSkills",
                c => new
                    {
                        Template_TemplateId = c.Int(nullable: false),
                        Skill_SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Template_TemplateId, t.Skill_SkillId })
                .ForeignKey("dbo.Templates", t => t.Template_TemplateId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skill_SkillId, cascadeDelete: true)
                .Index(t => t.Template_TemplateId)
                .Index(t => t.Skill_SkillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "ReviewerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PerformanceEvaluations", "ReviewerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "JobPositionId", "dbo.JobPositions");
            DropForeignKey("dbo.PerformanceEvaluations", "ConsultantId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Templates", "TemplateDurationId", "dbo.TemplateDurations");
            DropForeignKey("dbo.TemplateSkills", "Skill_SkillId", "dbo.Skills");
            DropForeignKey("dbo.TemplateSkills", "Template_TemplateId", "dbo.Templates");
            DropForeignKey("dbo.AspNetUsers", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.PerformanceEvaluationSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.PerformanceEvaluationSkills", "PerformanceEvaluationId", "dbo.PerformanceEvaluations");
            DropForeignKey("dbo.PerformanceEvaluations", "JobTitleId", "dbo.JobTitles");
            DropForeignKey("dbo.AspNetUsers", "JobTitleId", "dbo.JobTitles");
            DropForeignKey("dbo.PerformanceEvaluations", "JobPositionId", "dbo.JobPositions");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TemplateSkills", new[] { "Skill_SkillId" });
            DropIndex("dbo.TemplateSkills", new[] { "Template_TemplateId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Templates", new[] { "TemplateDurationId" });
            DropIndex("dbo.PerformanceEvaluationSkills", new[] { "PerformanceEvaluationId" });
            DropIndex("dbo.PerformanceEvaluationSkills", new[] { "SkillId" });
            DropIndex("dbo.PerformanceEvaluations", new[] { "JobPositionId" });
            DropIndex("dbo.PerformanceEvaluations", new[] { "JobTitleId" });
            DropIndex("dbo.PerformanceEvaluations", new[] { "ReviewerId" });
            DropIndex("dbo.PerformanceEvaluations", new[] { "ConsultantId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "TemplateId" });
            DropIndex("dbo.AspNetUsers", new[] { "JobPositionId" });
            DropIndex("dbo.AspNetUsers", new[] { "JobTitleId" });
            DropIndex("dbo.AspNetUsers", new[] { "ManagerId" });
            DropIndex("dbo.AspNetUsers", new[] { "ReviewerId" });
            DropTable("dbo.TemplateSkills");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.TemplateDurations");
            DropTable("dbo.Templates");
            DropTable("dbo.Skills");
            DropTable("dbo.PerformanceEvaluationSkills");
            DropTable("dbo.JobTitles");
            DropTable("dbo.PerformanceEvaluations");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.JobPositions");
        }
    }
}
