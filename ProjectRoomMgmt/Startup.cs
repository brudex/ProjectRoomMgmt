using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectRoomMgmt.Startup))]
namespace ProjectRoomMgmt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
