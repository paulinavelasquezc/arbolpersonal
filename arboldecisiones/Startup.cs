using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(arboldecisiones.Startup))]
namespace arboldecisiones
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }
    }
}
