using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySqlCustomDemo.Models
{
    public class MyUser: IdentityUser
    {
        /// <summary>
        /// 其他扩展的信息
        /// </summary>
        public string OtherInfo { get; set; }
    }
}