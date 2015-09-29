using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TESET.Startup))]
namespace TESET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
