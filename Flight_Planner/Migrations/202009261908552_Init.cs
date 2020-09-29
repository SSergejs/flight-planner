namespace Flight_Planner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        AirportName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Carrier = c.String(nullable: false),
                        DepartureTime = c.String(nullable: false),
                        ArrivalTime = c.String(nullable: false),
                        From_Id = c.Int(nullable: false),
                        To_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airports", t => t.From_Id)
                .ForeignKey("dbo.Airports", t => t.To_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "To_Id", "dbo.Airports");
            DropForeignKey("dbo.Flights", "From_Id", "dbo.Airports");
            DropIndex("dbo.Flights", new[] { "To_Id" });
            DropIndex("dbo.Flights", new[] { "From_Id" });
            DropTable("dbo.Flights");
            DropTable("dbo.Airports");
        }
    }
}
