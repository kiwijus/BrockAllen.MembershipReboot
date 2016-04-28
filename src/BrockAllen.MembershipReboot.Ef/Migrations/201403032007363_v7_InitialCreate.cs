//namespace BrockAllen.MembershipReboot.Ef.Migrations
//{
//    using Microsoft.Data.Entity.Migrations;
//    using System;
//    using JetBrains.Annotations;
//    using Microsoft.Data.Entity.Migrations.Operations;
//    using System.Collections.Generic;

//    public partial class v7_InitialCreate : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                "dbo.Groups",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ID = c.Column<Guid>(nullable: false),
//                        Tenant = c.Column<string>(nullable: false, maxLength: 50),
//                        Name = c.Column<string>(nullable: false, maxLength: 100),
//                        Created = c.Column<DateTime>(nullable: false),
//                        LastUpdated = c.Column<DateTime>(nullable: false),
//                    })
//                .PrimaryKey(t => t.Key);

//            migrationBuilder.CreateTable(
//                "dbo.GroupChilds",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ParentKey = c.Column<int>(nullable: false),
//                        ChildGroupID = c.Guid(nullable: false),
//                    })
//                .PrimaryKey(t => t.Key)
//                .ForeignKey("dbo.Groups", t => t.ParentKey, cascadeDelete: true)
//                .Index(t => t.ParentKey);

//            migrationBuilder.CreateTable(
//                "dbo.UserAccounts",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ID = c.Guid(nullable: false),
//                        Tenant = c.String(nullable: false, maxLength: 50),
//                        Username = c.String(nullable: false, maxLength: 254),
//                        Created = c.DateTime(nullable: false),
//                        LastUpdated = c.DateTime(nullable: false),
//                        IsAccountClosed = c.Boolean(nullable: false),
//                        AccountClosed = c.DateTime(),
//                        IsLoginAllowed = c.Boolean(nullable: false),
//                        LastLogin = c.DateTime(),
//                        LastFailedLogin = c.DateTime(),
//                        FailedLoginCount = c.Column<int>(nullable: false),
//                        PasswordChanged = c.DateTime(),
//                        RequiresPasswordReset = c.Boolean(nullable: false),
//                        Email = c.String(maxLength: 254),
//                        IsAccountVerified = c.Boolean(nullable: false),
//                        LastFailedPasswordReset = c.DateTime(),
//                        FailedPasswordResetCount = c.Column<int>(nullable: false),
//                        MobileCode = c.String(maxLength: 100),
//                        MobileCodeSent = c.DateTime(),
//                        MobilePhoneNumber = c.String(maxLength: 20),
//                        MobilePhoneNumberChanged = c.DateTime(),
//                        AccountTwoFactorAuthMode = c.Column<int>(nullable: false),
//                        CurrentTwoFactorAuthStatus = c.Column<int>(nullable: false),
//                        VerificationKey = c.String(maxLength: 100),
//                        VerificationPurpose = c.Column<int>(),
//                        VerificationKeySent = c.DateTime(),
//                        VerificationStorage = c.String(maxLength: 100),
//                        HashedPassword = c.String(maxLength: 200),
//                    })
//                .PrimaryKey(t => t.Key);

//            migrationBuilder.CreateTable(
//                "dbo.UserClaims",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ParentKey = c.Column<int>(nullable: false),
//                        Type = c.String(nullable: false, maxLength: 150),
//                        Value = c.String(nullable: false, maxLength: 150),
//                    })
//                .PrimaryKey(t => t.Key)
//                .ForeignKey("dbo.UserAccounts", t => t.ParentKey, cascadeDelete: true)
//                .Index(t => t.ParentKey);

//            migrationBuilder.CreateTable(
//                "dbo.LinkedAccountClaims",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ParentKey = c.Column<int>(nullable: false),
//                        ProviderName = c.String(nullable: false, maxLength: 30),
//                        ProviderAccountID = c.String(nullable: false, maxLength: 100),
//                        Type = c.String(nullable: false, maxLength: 150),
//                        Value = c.String(nullable: false, maxLength: 150),
//                    })
//                .PrimaryKey(t => t.Key)
//                .ForeignKey("dbo.UserAccounts", t => t.ParentKey, cascadeDelete: true)
//                .Index(t => t.ParentKey);

//            migrationBuilder.CreateTable(
//                "dbo.LinkedAccounts",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ParentKey = c.Column<int>(nullable: false),
//                        ProviderName = c.String(nullable: false, maxLength: 30),
//                        ProviderAccountID = c.String(nullable: false, maxLength: 100),
//                        LastLogin = c.DateTime(nullable: false),
//                    })
//                .PrimaryKey(t => t.Key)
//                .ForeignKey("dbo.UserAccounts", t => t.ParentKey, cascadeDelete: true)
//                .Index(t => t.ParentKey);

//            migrationBuilder.CreateTable(
//                "dbo.PasswordResetSecrets",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ParentKey = c.Column<int>(nullable: false),
//                        PasswordResetSecretID = c.Guid(nullable: false),
//                        Question = c.String(nullable: false, maxLength: 150),
//                        Answer = c.String(nullable: false, maxLength: 150),
//                    })
//                .PrimaryKey(t => t.Key)
//                .ForeignKey("dbo.UserAccounts", t => t.ParentKey, cascadeDelete: true)
//                .Index(t => t.ParentKey);

//            migrationBuilder.CreateTable(
//                "dbo.TwoFactorAuthTokens",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ParentKey = c.Column<int>(nullable: false),
//                        Token = c.String(nullable: false, maxLength: 100),
//                        Issued = c.DateTime(nullable: false),
//                    })
//                .PrimaryKey(t => t.Key)
//                .ForeignKey("dbo.UserAccounts", t => t.ParentKey, cascadeDelete: true)
//                .Index(t => t.ParentKey);

//            migrationBuilder.CreateTable(
//                "dbo.UserCertificates",
//                c => new
//                    {
//                        Key = c.Column<int>(nullable: false, identity: true),
//                        ParentKey = c.Column<int>(nullable: false),
//                        Thumbprint = c.String(nullable: false, maxLength: 150),
//                        Subject = c.String(maxLength: 250),
//                    })
//                .PrimaryKey(t => t.Key)
//                .ForeignKey("dbo.UserAccounts", t => t.ParentKey, cascadeDelete: true)
//                .Index(t => t.ParentKey);
            
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey("dbo.UserCertificates", "ParentKey", "dbo.UserAccounts");
//            migrationBuilder.DropForeignKey("dbo.TwoFactorAuthTokens", "ParentKey", "dbo.UserAccounts");
//            migrationBuilder.DropForeignKey("dbo.PasswordResetSecrets", "ParentKey", "dbo.UserAccounts");
//            migrationBuilder.DropForeignKey("dbo.LinkedAccounts", "ParentKey", "dbo.UserAccounts");
//            migrationBuilder.DropForeignKey("dbo.LinkedAccountClaims", "ParentKey", "dbo.UserAccounts");
//            migrationBuilder.DropForeignKey("dbo.UserClaims", "ParentKey", "dbo.UserAccounts");
//            migrationBuilder.DropForeignKey("dbo.GroupChilds", "ParentKey", "dbo.Groups");
//            migrationBuilder.DropIndex("dbo.UserCertificates", new[] { "ParentKey" });
//            migrationBuilder.DropIndex("dbo.TwoFactorAuthTokens", new[] { "ParentKey" });
//            migrationBuilder.DropIndex("dbo.PasswordResetSecrets", new[] { "ParentKey" });
//            migrationBuilder.DropIndex("dbo.LinkedAccounts", new[] { "ParentKey" });
//            migrationBuilder.DropIndex("dbo.LinkedAccountClaims", new[] { "ParentKey" });
//            migrationBuilder.DropIndex("dbo.UserClaims", new[] { "ParentKey" });
//            migrationBuilder.DropIndex("dbo.GroupChilds", new[] { "ParentKey" });
//            migrationBuilder.DropTable("dbo.UserCertificates");
//            migrationBuilder.DropTable("dbo.TwoFactorAuthTokens");
//            migrationBuilder.DropTable("dbo.PasswordResetSecrets");
//            migrationBuilder.DropTable("dbo.LinkedAccounts");
//            migrationBuilder.DropTable("dbo.LinkedAccountClaims");
//            migrationBuilder.DropTable("dbo.UserClaims");
//            migrationBuilder.DropTable("dbo.UserAccounts");
//            migrationBuilder.DropTable("dbo.GroupChilds");
//            migrationBuilder.DropTable("dbo.Groups");
//        }
//    }
//}
