using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartSuper.Startup))]
namespace SmartSuper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
