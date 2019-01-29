using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySqlDemo.Startup))]
namespace MySqlDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
