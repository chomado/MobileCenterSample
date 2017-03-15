using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MobileCenterSample.MobileAppService.Startup))]

namespace MobileCenterSample.MobileAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}