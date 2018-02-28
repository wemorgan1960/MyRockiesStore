using Store.Core.Models;
using Store.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Store.WebUI.Startup))]
namespace Store.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
            //CreateRoles();
        }
        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists(RoleName.AskAdmin))
            {
                var role = new IdentityRole();
                role.Name = RoleName.AskUser;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleName.AskAdmin))
            {
                var role = new IdentityRole();
                role.Name = RoleName.AskAdmin;
                roleManager.Create(role);

            }
            if (roleManager.RoleExists("AskAdmin"))
            {

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@myrockies.network";

                string userPWD = "@Spring1960";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "AskAdmin");
                    var result2 = UserManager.AddToRole(user.Id, "AskUser");

                }
            }
            if (roleManager.RoleExists("AskAdmin"))
            {

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "BillM";
                user.Email = "bmorgan@telusplanet.net";

                string userPWD = "@Spring1960";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "AskAdmin");
                    var result2 = UserManager.AddToRole(user.Id, "AskUser");

                }
            }
        }
    }
}
