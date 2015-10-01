using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetoIntranet.Startup))]
namespace ProjetoIntranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
