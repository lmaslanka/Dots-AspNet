namespace Dots.Web.Controllers
{
    using dots.database;
    using dots.viewModels;
    using System.Linq;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        protected void UpdateLastUpdatedValue(DotsContext db)
        {
            var lastUpdated = db.Outbreaks.OrderByDescending(o => o.ModifiedOn).Select(o => o.ModifiedOn).FirstOrDefault();
            this.ViewData["LastUpdated"] = lastUpdated.ToShortDateString();
        }

        protected UserItemViewModel GetUser(DotsContext db)
        {
            var username = GetUsername();
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                UpdateUserRoleViewData("Anonymous", false, false, false);

                return null;
            }

            var adminRole = db.UserRoles.Include("Role").Include("User").FirstOrDefault(ur => ur.User.Username == username && ur.Role.Name == "Administrator");
            var editorRole = db.UserRoles.Include("Role").Include("User").FirstOrDefault(ur => ur.User.Username == username && ur.Role.Name == "Editor");
            var updaterRole = db.UserRoles.Include("Role").Include("User").FirstOrDefault(ur => ur.User.Username == username && ur.Role.Name == "Updater");

            var userVm = new UserItemViewModel();

            userVm.RecordId = user.RecordId;
            userVm.Username = user.Username;
            userVm.FirstName = user.FirstName;
            userVm.LastName = user.LastName;
            userVm.IsAdministrator = adminRole != null;
            userVm.IsEditor = editorRole != null;
            userVm.IsUpdater = updaterRole != null;

            UpdateUserRoleViewData($"{user.FirstName} {user.LastName}", userVm.IsAdministrator, userVm.IsEditor, userVm.IsUpdater);
            UpdateLastUpdatedValue(db);


            return userVm;
        }

        private void UpdateUserRoleViewData(string name, bool isAdministrator, bool isEditor, bool isUpdater)
        {
            this.ViewData["Name"] = name;
            this.ViewData["IsAdministrator"] = isAdministrator;
            this.ViewData["IsEditor"] = isEditor;
            this.ViewData["IsUpdater"] = isUpdater;
        }

        private string GetUsername()
        {
            return this.User.Identity.IsAuthenticated ? this.User.Identity.Name.Replace(@"ACCOUNTS\", "") : "Anonymous";
        }
    }
}
