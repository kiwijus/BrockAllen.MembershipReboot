//namespace BrockAllen.MembershipReboot.Ef.Migrations
//{
//    using Microsoft.Data.Entity.Migrations;
//    using System;
//    using JetBrains.Annotations;

//    public partial class v7_Indexes : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateIndex("GroupIDIndex", "Groups", "ID", unique: true);
//            migrationBuilder.CreateIndex("GroupTenantNameIndex", "Groups", new[] { "Tenant", "Name" }, unique: true);
//            migrationBuilder.Sql("ALTER TABLE GroupChilds ADD CONSTRAINT UK_ParentKey_ChildGroupID UNIQUE NONCLUSTERED (ParentKey, ChildGroupID)");

//            migrationBuilder.CreateIndex("UserAccountIDIndex", "UserAccounts", "ID", unique: true);
//            migrationBuilder.CreateIndex("UserAccountsTenantUsernameIndex", "UserAccounts", new[] { "Tenant", "Username" }, unique: true);
//            migrationBuilder.CreateIndex("UserAccountsTenantEmailIndex", "UserAccounts", new[] { "Tenant", "Email" }, unique: false);
//            migrationBuilder.CreateIndex("UserAccountsVerificationKeyIndex", "UserAccounts", "VerificationKey", unique: false);
//            migrationBuilder.CreateIndex("UserAccountsUsernameIndex", "UserAccounts", "Username", unique: false);
//            migrationBuilder.CreateIndex("LinkedAccountsProviderNameProviderAccountIDIndex", "LinkedAccounts", new[] { "ProviderName", "ProviderAccountID" }, unique: false);
//            migrationBuilder.CreateIndex("UserCertificateThumbprintIndex", "UserCertificates", "Thumbprint", unique: false);
//            migrationBuilder.Sql("ALTER TABLE LinkedAccounts ADD CONSTRAINT UK_ParentKey_ProviderName_ProviderAccountID UNIQUE NONCLUSTERED (ParentKey, ProviderName, ProviderAccountID)");
//            migrationBuilder.Sql("ALTER TABLE LinkedAccountClaims ADD CONSTRAINT UK_ParentKey_ProviderName_ProviderAccountID_Type_Value UNIQUE NONCLUSTERED (ParentKey, ProviderName, ProviderAccountID, Type, Value)");
//            migrationBuilder.Sql("ALTER TABLE PasswordResetSecrets ADD CONSTRAINT UK_ParentKey_Question UNIQUE NONCLUSTERED (ParentKey, Question)");
//            migrationBuilder.Sql("ALTER TABLE UserCertificates ADD CONSTRAINT UK_ParentKey_Thumbprint UNIQUE NONCLUSTERED (ParentKey, Thumbprint)");
//            migrationBuilder.Sql("ALTER TABLE UserClaims ADD CONSTRAINT UK_ParentKey_Type_Value UNIQUE NONCLUSTERED (ParentKey, Type, Value)");
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropIndex("GroupIDIndex", "Groups");
//            migrationBuilder.DropIndex("GroupTenantNameIndex", "Groups");
//            migrationBuilder.Sql("ALTER TABLE GroupChilds DROP CONSTRAINT UK_ParentKey_ChildGroupID");

//            migrationBuilder.DropIndex("UserAccountIDIndex", "UserAccounts");
//            migrationBuilder.DropIndex("UserAccountsTenantUsernameIndex", "UserAccounts");
//            migrationBuilder.DropIndex("UserAccountsTenantEmailIndex", "UserAccounts");
//            migrationBuilder.DropIndex("UserAccountsVerificationKeyIndex", "UserAccounts");
//            migrationBuilder.DropIndex("UserAccountsUsernameIndex", "UserAccounts");
//            migrationBuilder.DropIndex("LinkedAccountsProviderNameProviderAccountIDIndex", "LinkedAccounts");
//            migrationBuilder.DropIndex("UserCertificateThumbprintIndex", "UserCertificates");
//            migrationBuilder.Sql("ALTER TABLE LinkedAccounts DROP CONSTRAINT UK_ParentKey_ProviderName_ProviderAccountID");
//            migrationBuilder.Sql("ALTER TABLE LinkedAccountClaims DROP CONSTRAINT UK_ParentKey_ProviderName_ProviderAccountID_Type_Value");
//            migrationBuilder.Sql("ALTER TABLE PasswordResetSecrets DROP CONSTRAINT UK_ParentKey_Question");
//            migrationBuilder.Sql("ALTER TABLE UserCertificates DROP CONSTRAINT UK_ParentKey_Thumbprint");
//            migrationBuilder.Sql("ALTER TABLE UserClaims DROP CONSTRAINT UK_ParentKey_Type_Value");
//        }
//    }
//}
