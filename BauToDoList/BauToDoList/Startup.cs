using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BauToDoList.Startup))]
namespace BauToDoList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
