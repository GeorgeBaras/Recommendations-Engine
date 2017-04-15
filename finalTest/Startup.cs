using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(finalTest.Startup))]
namespace finalTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
