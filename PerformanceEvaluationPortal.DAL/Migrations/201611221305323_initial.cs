namespace PerformanceEvaluationPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "EmploymentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "EmploymentDate", c => c.DateTime(nullable: false));
        }
    }
}
