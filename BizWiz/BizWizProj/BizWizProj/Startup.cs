using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BizWizProj.Startup))]
namespace BizWizProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
