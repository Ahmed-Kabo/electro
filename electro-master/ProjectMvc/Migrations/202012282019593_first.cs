namespace ProjectMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryImage = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        productName = c.String(),
                        ProductDetails = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifedeDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        isFeatured = c.Boolean(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Sale = c.Double(nullable: false),
                        CategoryObj_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.categories", t => t.CategoryObj_CategoryId)
                .Index(t => t.CategoryObj_CategoryId);
            
            CreateTable(
                "dbo.ImagesProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImagesUrl = c.String(),
                        ProductObject_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductObject_ProductId)
                .Index(t => t.ProductObject_ProductId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        ApplicationUserObject_Id = c.String(maxLength: 128),
                        ProductOpject_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserObject_Id)
                .ForeignKey("dbo.Products", t => t.ProductOpject_ProductId)
                .Index(t => t.ApplicationUserObject_Id)
                .Index(t => t.ProductOpject_ProductId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                "dbo.ShippingDetails",
                c => new
                    {
                        shippingId = c.Int(nullable: false, identity: true),
                        memberId = c.Int(nullable: false),
                        address = c.String(),
                        city = c.String(),
                        country = c.String(),
                        orderid = c.Int(nullable: false),
                        amoundpaid = c.Int(nullable: false),
                        paymenttype = c.Boolean(nullable: false),
                        ApplicationUserObj_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.shippingId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserObj_Id)
                .Index(t => t.ApplicationUserObj_Id);
            
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
                "dbo.SliderImages",
                c => new
                    {
                        sliderId = c.Int(nullable: false, identity: true),
                        sliderTile = c.String(),
                        sliderimage = c.String(),
                    })
                .PrimaryKey(t => t.sliderId);
            
            CreateTable(
                "dbo.Subs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubscribEmail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "ProductOpject_ProductId", "dbo.Products");
            DropForeignKey("dbo.ShippingDetails", "ApplicationUserObj_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "ApplicationUserObject_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ImagesProducts", "ProductObject_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryObj_CategoryId", "dbo.categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ShippingDetails", new[] { "ApplicationUserObj_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Reviews", new[] { "ProductOpject_ProductId" });
            DropIndex("dbo.Reviews", new[] { "ApplicationUserObject_Id" });
            DropIndex("dbo.ImagesProducts", new[] { "ProductObject_ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryObj_CategoryId" });
            DropTable("dbo.Subs");
            DropTable("dbo.SliderImages");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reviews");
            DropTable("dbo.ImagesProducts");
            DropTable("dbo.Products");
            DropTable("dbo.categories");
        }
    }
}
