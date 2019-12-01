namespace PropertyManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationPackageID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationPackages", t => t.AccommodationPackageID, cascadeDelete: true)
                .Index(t => t.AccommodationPackageID);
            
            CreateTable(
                "dbo.AccommodationPackages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PropertyTypeID = c.Int(nullable: false),
                        Name = c.String(),
                        NoOfRooms = c.Int(nullable: false),
                        FeePerMonth = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropertyTypes", t => t.PropertyTypeID, cascadeDelete: true)
                .Index(t => t.PropertyTypeID);
            
            CreateTable(
                "dbo.PropertyTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Leases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationID = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationID, cascadeDelete: true)
                .Index(t => t.AccommodationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leases", "AccommodationID", "dbo.Accommodations");
            DropForeignKey("dbo.Accommodations", "AccommodationPackageID", "dbo.AccommodationPackages");
            DropForeignKey("dbo.AccommodationPackages", "PropertyTypeID", "dbo.PropertyTypes");
            DropIndex("dbo.Leases", new[] { "AccommodationID" });
            DropIndex("dbo.AccommodationPackages", new[] { "PropertyTypeID" });
            DropIndex("dbo.Accommodations", new[] { "AccommodationPackageID" });
            DropTable("dbo.Leases");
            DropTable("dbo.PropertyTypes");
            DropTable("dbo.AccommodationPackages");
            DropTable("dbo.Accommodations");
        }
    }
}
