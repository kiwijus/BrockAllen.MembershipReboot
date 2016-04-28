/*
 * Copyright (c) Brock Allen.  All rights reserved.
 * see license.txt
 */

using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Relational;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;

namespace System.Data.Entity
{
    public static class DbModelBuilderExtensions
    {
        public static void RegisterUserAccountChildTablesForDelete<TKey, TAccount, TUserClaim, TLinkedAccount, TLinkedAccountClaim, TPasswordResetSecret, TTwoFactorAuthToken, TUserCertificate>(this DbContext ctx)
            where TAccount : RelationalUserAccount<TKey, TUserClaim, TLinkedAccount, TLinkedAccountClaim, TPasswordResetSecret, TTwoFactorAuthToken, TUserCertificate>
            where TUserClaim : RelationalUserClaim<TKey>, new()
            where TLinkedAccount : RelationalLinkedAccount<TKey>, new()
            where TLinkedAccountClaim : RelationalLinkedAccountClaim<TKey>, new()
            where TPasswordResetSecret : RelationalPasswordResetSecret<TKey>, new()
            where TTwoFactorAuthToken : RelationalTwoFactorAuthToken<TKey>, new()
            where TUserCertificate : RelationalUserCertificate<TKey>, new()
        {
            var data = ctx.ChangeTracker.Entries<TAccount>().Select(e => e.Entity);
            var collection = new ObservableCollection<TAccount>(data);

            collection.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (TAccount account in e.NewItems)
                        {
                            account.ClaimCollection.RegisterDeleteOnRemove(ctx);
                            account.LinkedAccountClaimCollection.RegisterDeleteOnRemove(ctx);
                            account.LinkedAccountCollection.RegisterDeleteOnRemove(ctx);
                            account.PasswordResetSecretCollection.RegisterDeleteOnRemove(ctx);
                            account.TwoFactorAuthTokenCollection.RegisterDeleteOnRemove(ctx);
                            account.UserCertificateCollection.RegisterDeleteOnRemove(ctx);
                        }
                    }
                };
        }

        public static void RegisterUserAccountChildTablesForDelete<TAccount>(this DbContext ctx)
            where TAccount : RelationalUserAccount
        {
            ctx.RegisterUserAccountChildTablesForDelete<int, TAccount, RelationalUserClaim, RelationalLinkedAccount, RelationalLinkedAccountClaim, RelationalPasswordResetSecret, RelationalTwoFactorAuthToken, RelationalUserCertificate>();
        }

        public static void RegisterGroupChildTablesForDelete<TGroup>(this DbContext ctx)
            where TGroup : RelationalGroup
        {
            var data = ctx.ChangeTracker.Entries<TGroup>().Select(e => e.Entity);
            var collection = new ObservableCollection<TGroup>(data);

            collection.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (TGroup group in e.NewItems)
                        {
                            group.ChildrenCollection.RegisterDeleteOnRemove(ctx);
                        }
                    }
                };
        }

        internal static void RegisterDeleteOnRemove<TChild>(this ICollection<TChild> collection, DbContext ctx)
            where TChild : class
        {
            var entities = collection as ObservableCollection<TChild>;// EntityCollection<TChild>;
            if (entities != null)
            {
                entities.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach(var item in e.OldItems)
                        {
                            var entity = item as TChild;
                            if (entity != null)
                            {
                                ctx.Entry<TChild>(entity).State = EntityState.Deleted;
                            }
                        }
                    }
                };
            }
        }

        public static void ConfigureMembershipRebootUserAccounts<TKey, TAccount, TUserClaim, TLinkedAccount, TLinkedAccountClaim, TPasswordResetSecret, TTwoFactorAuthToken, TUserCertificate>(this ModelBuilder modelBuilder)
            where TAccount : RelationalUserAccount<TKey, TUserClaim, TLinkedAccount, TLinkedAccountClaim, TPasswordResetSecret, TTwoFactorAuthToken, TUserCertificate>
            where TUserClaim : RelationalUserClaim<TKey>, new()
            where TLinkedAccount : RelationalLinkedAccount<TKey>, new()
            where TLinkedAccountClaim : RelationalLinkedAccountClaim<TKey>, new()
            where TPasswordResetSecret : RelationalPasswordResetSecret<TKey>, new()
            where TTwoFactorAuthToken : RelationalTwoFactorAuthToken<TKey>, new()
            where TUserCertificate : RelationalUserCertificate<TKey>, new()
        {
            modelBuilder.ConfigureMembershipRebootUserAccounts<TKey, TAccount, TUserClaim, TLinkedAccount, TLinkedAccountClaim, TPasswordResetSecret, TTwoFactorAuthToken, TUserCertificate>(null);
        }

        public static void ConfigureMembershipRebootUserAccounts<TKey, TAccount, TUserClaim, TLinkedAccount, TLinkedAccountClaim, TPasswordResetSecret, TTwoFactorAuthToken, TUserCertificate>(this ModelBuilder modelBuilder, string schemaName)
            where TAccount : RelationalUserAccount<TKey, TUserClaim, TLinkedAccount, TLinkedAccountClaim, TPasswordResetSecret, TTwoFactorAuthToken, TUserCertificate>
            where TUserClaim : RelationalUserClaim<TKey>, new()
            where TLinkedAccount : RelationalLinkedAccount<TKey>, new()
            where TLinkedAccountClaim : RelationalLinkedAccountClaim<TKey>, new()
            where TPasswordResetSecret : RelationalPasswordResetSecret<TKey>, new()
            where TTwoFactorAuthToken : RelationalTwoFactorAuthToken<TKey>, new()
            where TUserCertificate : RelationalUserCertificate<TKey>, new()
        {
            modelBuilder.Entity<TAccount>().ToTable("UserAccounts", schemaName)
                .HasKey(x => x.Key);

            modelBuilder.Entity<TAccount>().HasMany(x => x.PasswordResetSecretCollection)
                .WithOne().HasForeignKey(x => x.ParentKey).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<TPasswordResetSecret>().ToTable("PasswordResetSecrets", schemaName)
                .HasKey(x => x.Key);

            modelBuilder.Entity<TAccount>().HasMany(x => x.TwoFactorAuthTokenCollection)
                .WithOne().HasForeignKey(x => x.ParentKey).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<TTwoFactorAuthToken>().ToTable("TwoFactorAuthTokens", schemaName)
                .HasKey(x => x.Key);

            modelBuilder.Entity<TAccount>().HasMany(x => x.UserCertificateCollection)
                .WithOne().HasForeignKey(x => x.ParentKey).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<TUserCertificate>().ToTable("UserCertificates", schemaName)
                .HasKey(x => x.Key);

            modelBuilder.Entity<TAccount>().HasMany(x => x.ClaimCollection)
                .WithOne().HasForeignKey(x => x.ParentKey).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<TUserClaim>().ToTable("UserClaims", schemaName)
                .HasKey(x => x.Key);

            modelBuilder.Entity<TAccount>().HasMany(x => x.LinkedAccountCollection)
                .WithOne().HasForeignKey(x => x.ParentKey).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<TLinkedAccount>().ToTable("LinkedAccounts", schemaName)
                .HasKey(x => x.Key);

            modelBuilder.Entity<TAccount>().HasMany(x => x.LinkedAccountClaimCollection)
                .WithOne().HasForeignKey(x => x.ParentKey).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<TLinkedAccountClaim>().ToTable("LinkedAccountClaims", schemaName)
                .HasKey(x => x.Key);
        }
        
        public static void ConfigureMembershipRebootUserAccounts<TAccount>(this ModelBuilder modelBuilder)
            where TAccount : RelationalUserAccount
        {
            modelBuilder.ConfigureMembershipRebootUserAccounts<int, TAccount, RelationalUserClaim, RelationalLinkedAccount, RelationalLinkedAccountClaim, RelationalPasswordResetSecret, RelationalTwoFactorAuthToken, RelationalUserCertificate>(null);
        }

        public static void ConfigureMembershipRebootUserAccounts<TAccount>(this ModelBuilder modelBuilder, string schemaName)
            where TAccount : RelationalUserAccount
        {
            modelBuilder.ConfigureMembershipRebootUserAccounts<int, TAccount, RelationalUserClaim, RelationalLinkedAccount, RelationalLinkedAccountClaim, RelationalPasswordResetSecret, RelationalTwoFactorAuthToken, RelationalUserCertificate>(schemaName);
        }

        public static void ConfigureMembershipRebootGroups<TGroup>(this ModelBuilder modelBuilder)
            where TGroup : RelationalGroup
        {
            modelBuilder.ConfigureMembershipRebootGroups<TGroup>(null);
        }
        
        public static void ConfigureMembershipRebootGroups<TGroup>(this ModelBuilder modelBuilder, string schemaName)
            where TGroup : RelationalGroup
        {
            modelBuilder.Entity<TGroup>().ToTable("Groups", schemaName)
                .HasKey(x => x.Key);

            modelBuilder.Entity<TGroup>().HasMany(x => x.ChildrenCollection)
                .WithOne().HasForeignKey(x=>x.ParentKey).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade);
            modelBuilder.Entity<RelationalGroupChild>().ToTable("GroupChilds", schemaName)
                .HasKey(x => x.Key);
        }
    }
}
