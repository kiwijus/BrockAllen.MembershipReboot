namespace BrockAllen.MembershipReboot.Ef.Migrations
{
    using Microsoft.Data.Entity.Migrations;
    using System;
    using JetBrains.Annotations;

    public partial class v7_PhantomIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //CreateIndex("dbo.GroupChilds", "ParentKey");
            //CreateIndex("dbo.UserClaims", "ParentKey");
            //CreateIndex("dbo.LinkedAccountClaims", "ParentKey");
            //CreateIndex("dbo.LinkedAccounts", "ParentKey");
            //CreateIndex("dbo.PasswordResetSecrets", "ParentKey");
            //CreateIndex("dbo.TwoFactorAuthTokens", "ParentKey");
            //CreateIndex("dbo.UserCertificates", "ParentKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //DropIndex("dbo.UserCertificates", new[] { "ParentKey" });
            //DropIndex("dbo.TwoFactorAuthTokens", new[] { "ParentKey" });
            //DropIndex("dbo.PasswordResetSecrets", new[] { "ParentKey" });
            //DropIndex("dbo.LinkedAccounts", new[] { "ParentKey" });
            //DropIndex("dbo.LinkedAccountClaims", new[] { "ParentKey" });
            //DropIndex("dbo.UserClaims", new[] { "ParentKey" });
            //DropIndex("dbo.GroupChilds", new[] { "ParentKey" });
        }
    }
}
