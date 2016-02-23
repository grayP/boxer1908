using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bw01.Startup))]
namespace bw01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
