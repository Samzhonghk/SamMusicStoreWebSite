using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SamMusicStoreWebSite.Startup))]
namespace SamMusicStoreWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
