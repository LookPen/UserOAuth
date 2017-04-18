using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace FormAuthenticationDemo.Models
{
    public class MyPrincipal<TUser> : IPrincipal
        where TUser : class, new()
    {
        public MyPrincipal(FormsAuthenticationTicket ticket, TUser userData)
        {
            this.userData = userData;
            this.Identity = new FormsIdentity(ticket);
        }

        public TUser userData
        {
            get; private set;
        }

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        /// <summary>
        /// 登录验证通过写入cookie（FormsAuthentication.SetAuthCookie("xx",true) 操作扩展）
        /// </summary>
        /// <param name="userID">用户标示</param>
        /// <param name="user">用户信息</param>
        /// <param name="expiration">过期时间 单位秒</param>
        public void SignIn(string userID, TUser user, int expiration)
        {
            ////1:序列化用户数据
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            string data = JsonConvert.SerializeObject(user);

            ////2:生成ticket; 登录状态是有过期限制的。Cookie有 有效期，FormsAuthenticationTicket对象也有 有效期。 这二者任何一个过期时，都将导致登录状态无效。 按照默认设置，FormsAuthenticationModule将采用slidingExpiration=true的策略来处理FormsAuthenticationTicket过期问题。
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userID, DateTime.Now, DateTime.Now.AddSeconds(expiration), true, data);

            ////3:加密ticket
            string cookieValue = FormsAuthentication.Encrypt(ticket);

            ////4:创建登录cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
            cookie.HttpOnly = true;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (expiration > 0)
                cookie.Expires = DateTime.Now.AddSeconds(expiration);

            ////5:写入登录cookie
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 解析用户信息
        /// </summary>
        /// <param name="context">请求上下文</param>
        public bool TrySetUserInfo(HttpContext context)
        {
            // 1. 读登录Cookie
            HttpCookie cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return false;

            try
            {
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !string.IsNullOrEmpty(ticket.UserData))
                {
                    userData = JsonConvert.DeserializeObject<TUser>(ticket.UserData);
                    context.User = new MyPrincipal<TUser>(ticket, userData);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}