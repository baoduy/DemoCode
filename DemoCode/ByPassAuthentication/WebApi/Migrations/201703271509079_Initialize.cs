namespace WebApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        CreatedBy = c.String(nullable: false, maxLength: 400),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 400),
                        UpdatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Models");
        }
    }
}
