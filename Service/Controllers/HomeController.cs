using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BusinessTier.Factory;
using BusinessTier.Repository;
using DataTier;
using DataTier.Dao;
using Ninject;
using Service.Models;

namespace Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepo _repo;

        public HomeController()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IRepo>().To<UserRepo>();
            _repo = (UserRepo) kernel.Get<IRepo>();
        }

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

        public ActionResult Index()
        {
            if (IsLoggedIn()) return View("Home", Session["User"]);

            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

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

        #region Register

        public ActionResult Register()
        {
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
            var dic = _repo.Register(info);
            if (!(bool) dic["success"])
            {
                var messages = (List<string>) dic["messages"];
                var html = "";
                foreach (var message in messages)
                    html += "<p class=\"red-text\">" + message + "</p>";
                ViewBag.Html = html;
            }

            return View("Register");
        }

        #endregion

        #region Login

        public ActionResult Login()
        {
            if (IsLoggedIn()) return View("Home", Session["User"]);

            return View();
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

            var dic = _repo.Login(info.User);
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

            Session["user"] = user;
            Session["token"] = token;

            return View("Home", user);
        }

        #endregion
    }
}