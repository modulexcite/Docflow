using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RapidDoc.Startup))]
namespace RapidDoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
