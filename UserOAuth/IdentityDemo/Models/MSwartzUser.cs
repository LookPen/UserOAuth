using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Models
{
    public class MSwartzUser : IdentityUser
    {

        public string HeadImg { get; set; }
    }
}