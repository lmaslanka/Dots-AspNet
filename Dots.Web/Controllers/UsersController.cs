namespace Dots.Web.Controllers
{
    using AutoMapper;
    using dots.database;
    using dots.models;
    using dots.viewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class UsersController : Controller
    {
        #region Main List

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

        #endregion

        #region New User

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

                var userVm = new UserItemViewModel();

                return View(userVm);
            }
        }

        [HttpPost]
        public ActionResult New(UserItemViewModel userVm)
        {
            if (this.ModelState.IsValid)
            {
                using (var db = new DotsContext())
                {
                    var currentDateTime = DateTime.Now;
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

                    var userDb = mapper.Map<User>(userVm);

                    userDb.ModifiedBy = user.Username;
                    userDb.ModifiedOn = currentDateTime;
                    userDb.CreatedBy = user.Username;
                    userDb.CreatedOn = currentDateTime;

                    db.Users.Add(userDb);

                    if(userVm.IsAdministrator)
                    {
                        var role = new UserRole();
                        var roleId = db.Roles.FirstOrDefault(r => r.Name == "Administrator")?.RecordId;

                        if (roleId != null)
                        {
                            role.RoleId = roleId.Value;
                            role.User = userDb;
                            role.ModifiedBy = user.Username;
                            role.ModifiedOn = currentDateTime;
                            role.CreatedBy = user.Username;
                            role.CreatedOn = currentDateTime;

                            db.UserRoles.Add(role);
                        }
                    }

                    if (userVm.IsEditor)
                    {
                        var role = new UserRole();
                        var roleId = db.Roles.FirstOrDefault(r => r.Name == "Editor")?.RecordId;

                        if (roleId != null)
                        {
                            role.RoleId = roleId.Value;
                            role.User = userDb;
                            role.ModifiedBy = user.Username;
                            role.ModifiedOn = currentDateTime;
                            role.CreatedBy = user.Username;
                            role.CreatedOn = currentDateTime;

                            db.UserRoles.Add(role);
                        }
                    }

                    if (userVm.IsUpdater)
                    {
                        var role = new UserRole();
                        var roleId = db.Roles.FirstOrDefault(r => r.Name == "Updater")?.RecordId;

                        if (roleId != null)
                        {
                            role.RoleId = roleId.Value;
                            role.User = userDb;
                            role.ModifiedBy = user.Username;
                            role.ModifiedOn = currentDateTime;
                            role.CreatedBy = user.Username;
                            role.CreatedOn = currentDateTime;

                            db.UserRoles.Add(role);
                        }
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(userVm);
        }

        #endregion

        #region Edit User

        public ActionResult Edit(long? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

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

                var userFromDb = db.Users.FirstOrDefault(u => u.RecordId == id);

                if(userFromDb == null)
                {
                    return View("NotFound");
                }

                var adminRole = db.UserRoles.Include("Role").FirstOrDefault(ur => ur.UserId == userFromDb.RecordId && ur.Role.Name == "Administrator");
                var editorRole = db.UserRoles.Include("Role").FirstOrDefault(ur => ur.UserId == userFromDb.RecordId && ur.Role.Name == "Editor");
                var updaterRole = db.UserRoles.Include("Role").FirstOrDefault(ur => ur.UserId == userFromDb.RecordId && ur.Role.Name == "Updater");

                var userVm = mapper.Map<UserItemViewModel>(userFromDb);

                userVm.IsAdministrator = (adminRole != null);
                userVm.IsEditor = (editorRole != null);
                userVm.IsUpdater = (updaterRole != null);

                return View(userVm);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserItemViewModel userVm)
        {
            if (this.ModelState.IsValid)
            {
                using (var db = new DotsContext())
                {
                    var currentDateTime = DateTime.Now;
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

                    var userDb = db.Users.FirstOrDefault(u => u.RecordId == userVm.RecordId);

                    userDb.Username = userVm.Username;
                    userDb.FirstName = userVm.FirstName;
                    userDb.LastName = userVm.LastName;
                    userDb.ModifiedBy = user.Username;
                    userDb.ModifiedOn = currentDateTime;

                    var adminRole = db.UserRoles.Include("Role").FirstOrDefault(ur => ur.UserId == userVm.RecordId && ur.Role.Name == "Administrator");
                    var editorRole = db.UserRoles.Include("Role").FirstOrDefault(ur => ur.UserId == userVm.RecordId && ur.Role.Name == "Editor");
                    var updaterRole = db.UserRoles.Include("Role").FirstOrDefault(ur => ur.UserId == userVm.RecordId && ur.Role.Name == "Updater");

                    if (userVm.IsAdministrator != (adminRole != null))
                    {
                        if(userVm.IsAdministrator)
                        {
                            var role = new UserRole();
                            var roleId = db.Roles.FirstOrDefault(r => r.Name == "Administrator")?.RecordId;

                            if (roleId != null)
                            {
                                role.RoleId = roleId.Value;
                                role.User = userDb;
                                role.ModifiedBy = user.Username;
                                role.ModifiedOn = currentDateTime;
                                role.CreatedBy = user.Username;
                                role.CreatedOn = currentDateTime;

                                db.UserRoles.Add(role);
                            }
                        }
                        else
                        {
                            var roleId = db.Roles.FirstOrDefault(r => r.Name == "Administrator")?.RecordId;

                            if (roleId != null)
                            {
                                var role = db.UserRoles.FirstOrDefault(r => r.RoleId == roleId && r.UserId == userDb.RecordId);
                                db.UserRoles.Remove(role);
                            }
                        }
                    }

                    if (userVm.IsEditor != (editorRole != null))
                    {
                        if (userVm.IsEditor)
                        {
                            var role = new UserRole();
                            var roleId = db.Roles.FirstOrDefault(r => r.Name == "Editor")?.RecordId;

                            if (roleId != null)
                            {
                                role.RoleId = roleId.Value;
                                role.User = userDb;
                                role.ModifiedBy = user.Username;
                                role.ModifiedOn = currentDateTime;
                                role.CreatedBy = user.Username;
                                role.CreatedOn = currentDateTime;

                                db.UserRoles.Add(role);
                            }
                        }
                        else
                        {
                            var roleId = db.Roles.FirstOrDefault(r => r.Name == "Editor")?.RecordId;

                            if (roleId != null)
                            {
                                var role = db.UserRoles.FirstOrDefault(r => r.RoleId == roleId && r.UserId == userDb.RecordId);
                                db.UserRoles.Remove(role);
                            }
                        }
                    }

                    if (userVm.IsUpdater != (updaterRole != null))
                    {
                        if (userVm.IsUpdater)
                        {
                            var role = new UserRole();
                            var roleId = db.Roles.FirstOrDefault(r => r.Name == "Updater")?.RecordId;

                            if (roleId != null)
                            {
                                role.RoleId = roleId.Value;
                                role.User = userDb;
                                role.ModifiedBy = user.Username;
                                role.ModifiedOn = currentDateTime;
                                role.CreatedBy = user.Username;
                                role.CreatedOn = currentDateTime;

                                db.UserRoles.Add(role);
                            }
                        }
                        else
                        {
                            var roleId = db.Roles.FirstOrDefault(r => r.Name == "Updater")?.RecordId;

                            if (roleId != null)
                            {
                                var role = db.UserRoles.FirstOrDefault(r => r.RoleId == roleId && r.UserId == userDb.RecordId);
                                db.UserRoles.Remove(role);
                            }
                        }
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(userVm);
        }

        #endregion

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            using (var db = new DotsContext())
            {
                var user = db.Users.FirstOrDefault(u => u.RecordId == id);

                if(user == null)
                {
                    return View("NotFound");
                }

                var roles = db.UserRoles.Where(ur => ur.UserId == user.RecordId);

                db.UserRoles.RemoveRange(roles);
                db.Users.Remove(user);

                db.SaveChanges();
            }

            return Index();
        }

        private UserItemViewModel GetUser(DotsContext db, string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            
            if (user == null)
            {
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
                cfg.CreateMap<User, UserItemViewModel>();
                cfg.CreateMap<UserItemViewModel, User>();
            });

            this.mapper = automapperConfiguration.CreateMapper();
        }

        private readonly IMapper mapper;
    }
}