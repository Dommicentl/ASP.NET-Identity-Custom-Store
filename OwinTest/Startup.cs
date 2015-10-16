using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OwinTest.Startup))]
namespace OwinTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
