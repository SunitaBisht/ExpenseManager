using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExpenseManager.API.Startup))]
namespace ExpenseManager.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
