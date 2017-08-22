using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VolunteerTracker.Startup))]
namespace VolunteerTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
