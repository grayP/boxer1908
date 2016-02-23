namespace bw01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ASPNet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrewMembers",
                c => new
                    {
                        CrewMemberID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MobilePhone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CrewMemberID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CrewMembers");
        }
    }
}
