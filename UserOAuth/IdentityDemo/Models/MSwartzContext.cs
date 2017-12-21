using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityDemo.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MSwartzContext : IdentityDbContext<MSwartzUser>
    {
        public MSwartzContext() : base("DefaultConnection")
        { }


        public DbSet<IdentityUserRole> UserRoles { get; set; }
        public DbSet<IdentityUserClaim> Claims { get; set; }
        public DbSet<IdentityUserLogin> Logins { get; set; }



        static MSwartzContext()
        {
            Database.SetInitializer<MSwartzContext>(new InitData());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }
            ////不使用默认的建表约束，因为默认源码如下:
            //EntityTypeConfiguration<TUser> configuration = modelBuilder.Entity<TUser>().ToTable("AspNetUsers");
            //configuration.HasMany<TUserRole>(u => u.Roles).WithRequired().HasForeignKey<TKey>(ur => ur.UserId);
            //configuration.HasMany<TUserClaim>(u => u.Claims).WithRequired().HasForeignKey<TKey>(uc => uc.UserId);
            //configuration.HasMany<TUserLogin>(u => u.Logins).WithRequired().HasForeignKey<TKey>(ul => ul.UserId);
            //IndexAttribute attribute = new IndexAttribute("UserNameIndex");
            //attribute.set_IsUnique(true);
            //configuration.Property((Expression<Func<TUser, string>>)(u => u.UserName)).IsRequired().HasMaxLength(0x100).HasColumnAnnotation("Index", new IndexAnnotation(attribute));
            //configuration.Property((Expression<Func<TUser, string>>)(u => u.Email)).HasMaxLength(0x100);
            //modelBuilder.Entity<TUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");
            //modelBuilder.Entity<TUserLogin>().HasKey(l => new { LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey, UserId = l.UserId }).ToTable("AspNetUserLogins");
            //modelBuilder.Entity<TUserClaim>().ToTable("AspNetUserClaims");
            //EntityTypeConfiguration<TRole> configuration2 = modelBuilder.Entity<TRole>().ToTable("AspNetRoles");
            //IndexAttribute attribute2 = new IndexAttribute("RoleNameIndex");
            //attribute2.set_IsUnique(true);
            //configuration2.Property((Expression<Func<TRole, string>>)(r => r.Name)).IsRequired().HasMaxLength(0x100).HasColumnAnnotation("Index", new IndexAnnotation(attribute2));
            //configuration2.HasMany<TUserRole>(r => r.Users).WithRequired().HasForeignKey<TKey>(ur => ur.RoleId);
            ////可以看出，建立了几个索引，且索引的长度都超过了mysql的最大值，索引使用默认的建表约束Mysql是会报错的


            ///先不考虑外键和索引、建表 (但是他的实体里面还是有 引用类型啊，所以还是把外键搞出来了啊......真心不想用外键啊)
            modelBuilder.Entity<MSwartzUser>().ToTable("SwartzUser");
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("SwartzUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey, UserId = l.UserId }).ToTable("SwartzUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("SwartzUserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("SwartzRoles");

        }
    }

    public class InitData : DropCreateDatabaseIfModelChanges<MSwartzContext>
    {
        protected override void Seed(MSwartzContext context)
        {

            base.Seed(context);
        }
    }
}