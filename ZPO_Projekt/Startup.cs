using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ZPO_Projekt.Startup))]
namespace ZPO_Projekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}