using AutoMapper;
using dots.database;
using dots.models;
using dots.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dots.Web.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            using (var db = new DotsContext())
            {
                var username = GetUsername();
                var user = GetUser(db, username);

                if (user == null)
                {
                    this.ViewData["Name"] = "Anonymous";
                    this.ViewData["IsAdministrator"] = false;
                    this.ViewData["IsEditor"] = false;
                    this.ViewData["IsUpdater"] = false;
                }
                else
                {
                    this.ViewData["Name"] = $"{user.FirstName} {user.LastName}";
                    this.ViewData["IsAdministrator"] = user.IsAdministrator;
                    this.ViewData["IsEditor"] = user.IsEditor;
                    this.ViewData["IsUpdater"] = user.IsUpdater;
                }

                var usersFromDb = db.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();

                var usersViewModel = mapper.Map<List<UserListItemViewModel>>(usersFromDb);

                return View(usersViewModel);
            }
        }

        public ActionResult New()
        {
            using (var db = new DotsContext())
            {
                var username = GetUsername();
                var user = GetUser(db, username);

                if (user == null)
                {
                    this.ViewData["Name"] = "Anonymous";
                    this.ViewData["IsAdministrator"] = false;
                    this.ViewData["IsEditor"] = false;
                    this.ViewData["IsUpdater"] = false;
                }
                else
                {
                    this.ViewData["Name"] = $"{user.FirstName} {user.LastName}";
                    this.ViewData["IsAdministrator"] = user.IsAdministrator;
                    this.ViewData["IsEditor"] = user.IsEditor;
                    this.ViewData["IsUpdater"] = user.IsUpdater;
                }

                var usersFromDb = db.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();

                var usersViewModel = mapper.Map<List<UserListItemViewModel>>(usersFromDb);

                return View(usersViewModel);
            }
        }

        private UserItemViewModel GetUser(DotsContext db, string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            var adminRole = db.UserRoles.Include("Role").Include("User").FirstOrDefault(ur => ur.User.Username == username && ur.Role.Name == "Administrator");
            var editorRole = db.UserRoles.Include("Role").Include("User").FirstOrDefault(ur => ur.User.Username == username && ur.Role.Name == "Editor");
            var updaterRole = db.UserRoles.Include("Role").Include("User").FirstOrDefault(ur => ur.User.Username == username && ur.Role.Name == "Updater");

            if (user == null)
            {
                return null;
            }

            var userVm = new UserItemViewModel();

            userVm.RecordId = user.RecordId;
            userVm.FirstName = user.FirstName;
            userVm.LastName = user.LastName;
            userVm.IsAdministrator = adminRole != null;
            userVm.IsEditor = editorRole != null;
            userVm.IsUpdater = updaterRole != null;

            return userVm;
        }

        private string GetUsername()
        {
            return this.User.Identity.IsAuthenticated ? this.User.Identity.Name.Replace(@"ACCOUNTS\", "") : "Anonymous";
        }

        public UsersController()
        {
            var automapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserListItemViewModel>();
            });

            this.mapper = automapperConfiguration.CreateMapper();
        }

        private readonly IMapper mapper;
    }
}