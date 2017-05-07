using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningCenter.Website.Startup))]
namespace LearningCenter.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
