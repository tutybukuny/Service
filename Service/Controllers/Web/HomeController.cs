using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BusinessTier.Factory;
using BusinessTier.Repository;
using DataTier;
using DataTier.Dao;
using DataTier.Factory;
using Service.Models;

namespace Service.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly ProjectRepo _projectRepo;
        private readonly UserRepo _userRepo;

        public HomeController()
        {
            _userRepo = (UserRepo) RepoFactory.GetRepo("UserRepo");
            _projectRepo = (ProjectRepo) RepoFactory.GetRepo("ProjectRepo");
        }

        #region Check logged in

        /// <summary>
        ///     Is User logged in?
        /// </summary>
        /// <returns></returns>
        private bool IsLoggedIn()
        {
            if (Session["User"] != null) return true;

            var cookie = Request.Cookies["TheProjectToken"];
            var userDao = (UserDao) DaoFactory.GetDao("UserDao");

            if (cookie != null)
            {
                var user = userDao.GetByToken(cookie.Value);

                if (user != null)
                {
                    Session["User"] = user;

                    return true;
                }
            }

            return false;
        }

        #endregion

        public ActionResult Index()
        {
            if (!IsLoggedIn()) return View();

            var dic = _projectRepo.GetUserFirstProject(((User) Session["User"]).id);
            var project = (Project) dic["project"];
            var model = new HomeViewModel {Project = project, User = (User) Session["User"]};

            return View("Home", model);
        }

        #region Forgot Password

        public ActionResult ForgotPassword()
        {
            return View();
        }

        #endregion

        #region Log out

        public ActionResult Logout()
        {
            Session["User"] = null;

            if (Request.Cookies["TheProjectToken"] != null)
            {
                var cookie = new HttpCookie("TheProjectToken") {Expires = DateTime.Now.AddDays(-1)};
                Response.Cookies.Add(cookie);
            }

            return View("Index");
        }

        #endregion

        #region User profile

        [HttpGet]
        public ActionResult UserProfile(int user_id)
        {
            IsLoggedIn();
            var dic = _userRepo.UserProfile(user_id);
            var user = (User) dic["user"];

            if (user == null) return Index();

            return View(user);
        }

        #endregion

        #region Register

        public ActionResult Register()
        {
            if (IsLoggedIn()) return View("Home", Session["User"]);

            return View();
        }

        /// <summary>
        ///     Register
        /// </summary>
        /// <param name="info">user's info</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User info)
        {
            var dic = _userRepo.Register(info);
            if (!(bool) dic["success"])
            {
                var messages = (List<string>) dic["messages"];
                var html = "";
                foreach (var message in messages)
                    html += "<p class=\"red-text\">" + message + "</p>";
                ViewBag.Html = html;
            }
            else
            {
                ViewBag.Success = true;
            }

            return View("Register");
        }

        #endregion

        #region Login

        public ActionResult Login()
        {
            return IsLoggedIn() ? Index() : View();
        }

        /// <summary>
        ///     Login
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel info)
        {
            if (string.IsNullOrEmpty(info.User.email) || string.IsNullOrEmpty(info.User.password)) return View();

            var dic = _userRepo.Login(info.User);
            var user = (User) dic["user"];

            if (user == null)
            {
                ViewBag.Message = "Wrong email or password!";
                return View();
            }

            var token = (string) dic["token"];

            if (info.Remember)
            {
                var cookie = new HttpCookie("TheProjectToken") {Value = token};
                Response.Cookies.Add(cookie);
            }

            Session["User"] = user;
            Session["Token"] = token;

            return Index();
        }

        #endregion
    }
}