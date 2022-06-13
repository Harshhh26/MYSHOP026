using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MYSHOP.WebUI.Startup))]
namespace MYSHOP.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
