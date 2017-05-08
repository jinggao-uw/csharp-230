using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningCenter.Business;
using LearningCenter.Website.Models;

namespace LearningCenter.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserManager userManager;
        private readonly IClassManager classManager;

        public HomeController(IUserManager userManager, IClassManager classManager)
        {
            this.userManager = userManager;
            this.classManager = classManager;
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new LearningCenter.Website.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var registerResult = userManager.Register(registerModel.UserName, registerModel.Password);

                if (registerResult == null)
                {
                    ModelState.AddModelError("", "Register failed");
                }
                else
                {
                    var loginResult = userManager.LogIn(registerModel.UserName, registerModel.Password);
                    if (loginResult == null)
                    {
                        ModelState.AddModelError("", "Login failed");
                    }
                    else
                    {
                        Session["User"] = new LearningCenter.Website.Models.UserModel { Id = loginResult.Id, Name = loginResult.Name };

                        System.Web.Security.FormsAuthentication.SetAuthCookie(registerModel.UserName, false);
                        return Redirect(returnUrl ?? "~/");
                    }
                }
            }

            return View(registerModel);
        }

        public ActionResult EnrollInClass()
        {
            var allClasses = classManager.ListAll();
            var classListViewModel = new ClassListViewModel()
            {
                classes = Array.ConvertAll(allClasses, (c) => {
                    return new LearningCenter.Website.Models.ClassModel()
                    {
                        ClassId = c.ClassId,
                        ClassName = c.ClassName,
                        ClassDescription = c.ClassDescription,
                        ClassPrice = c.ClassPrice
                    };
                })
            };

            return View(classListViewModel);
        }

        [HttpPost]
        public ActionResult EnrollInClass(int selectedClass)
        {
            if (ModelState.IsValid)
            {
                LearningCenter.Website.Models.UserModel userModel = Session["User"] as Models.UserModel;

                if (userModel != null)
                {
                    bool ret = classManager.AddClassForUser(selectedClass, userModel.Id);
                    if (ret)
                    {
                        return Redirect("~/Home/StudentClasses");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error! Class already added");
                        var allClasses = classManager.ListAll();
                        var classListViewModel = new ClassListViewModel()
                        {
                            classes = Array.ConvertAll(allClasses, (c) => {
                                return new LearningCenter.Website.Models.ClassModel()
                                {
                                    ClassId = c.ClassId,
                                    ClassName = c.ClassName,
                                    ClassDescription = c.ClassDescription,
                                    ClassPrice = c.ClassPrice
                                };
                            })
                        };

                        return View(classListViewModel);
                    }

                }
            }

            return Redirect("~/Home/Login");
        }

        public ActionResult StudentClasses()
        {
            LearningCenter.Website.Models.UserModel userModel = Session["User"] as Models.UserModel;

            if (userModel != null)
            {
                var allClasses = classManager.ListForUser(userModel.Id);

                var classListViewModel = new ClassListViewModel()
                {
                    classes = Array.ConvertAll(allClasses, (c) => {
                        return new LearningCenter.Website.Models.ClassModel()
                        {
                            ClassId = c.ClassId,
                            ClassName = c.ClassName,
                            ClassDescription = c.ClassDescription,
                            ClassPrice = c.ClassPrice
                        };
                    })
                };
                return View(classListViewModel.classes);
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        public ActionResult ClassList()
        {
            var allClasses = classManager.ListAll();
            var classListViewModel = new ClassListViewModel()
            {
                classes = Array.ConvertAll(allClasses, (c) => {
                    return new LearningCenter.Website.Models.ClassModel()
                    {
                        ClassId = c.ClassId,
                        ClassName = c.ClassName,
                        ClassDescription = c.ClassDescription,
                        ClassPrice = c.ClassPrice
                    };
                })
            };

            return View(classListViewModel.classes);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Classes Descriptions";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Informations";

            return View();
        }
    }
}