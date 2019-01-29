using Microsoft.AspNet.Identity;
using MySqlCustomDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySqlCustomDemo.Infrastructure
{
    public class MyUserManager: UserManager<MyUser>
    {
        public MyUserManager(IUserStore<MyUser> store) : base(store)
        { }
    }
}