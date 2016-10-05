using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_Maverick.Startup))]
namespace Project_Maverick
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
