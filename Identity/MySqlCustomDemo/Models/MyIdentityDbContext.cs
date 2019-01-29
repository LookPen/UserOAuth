using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MySqlCustomDemo.Models
{
    public class MyIdentityDbContext : IdentityDbContext<MyUser>
    {
        public MyIdentityDbContext() : base("DefaultConnection") { }

        static MyIdentityDbContext()
        {
            //初始化数据库
            Database.SetInitializer<MyIdentityDbContext>(new MyIdentityDbInit());
        }

        public static MyIdentityDbContext Create()
        {
            // 创建一个实例，用于Owin的的上下文创建
            return new MyIdentityDbContext();
        }
    }

    public class MyIdentityDbInit : DropCreateDatabaseIfModelChanges<MyIdentityDbContext>
    {
        protected override void Seed(MyIdentityDbContext context)
        {
            this.InitData(context);
            base.Seed(context);
        }

        public void InitData(MyIdentityDbContext context)
        {
            //CodeFirst初始化数据

        }
    }
}