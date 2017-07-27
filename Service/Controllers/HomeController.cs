using System;
using System.Web;
using System.Web.Mvc;
using CoreServiceLib.DAO;
using CoreServiceLib.Models;
using Service.Models;
using Service.Properties;

namespace Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDao _dao;

        public HomeController()
        {
            _dao = new UserDao(Settings.Default.ConnectionString);
        }

        public ActionResult Index()
        {
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
                var cookie = new HttpCookie("TheProjectToken");
                cookie.Expires = DateTime.Now.AddDays(-1);
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

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!_dao.CheckExistsEmail(user.email))
            {
                _dao.Insert(user);
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Message = "Email is existed!";
            }

            return View("Register");
        }

        #endregion

        #region Login

        public ActionResult Login()
        {
            if (Session["User"] != null) return View("Home", Session["User"]);

            var cookie = Request.Cookies["TheProjectToken"];

            if (cookie != null)
            {
                var user = _dao.GetUserByToken(cookie.Value);

                if (user != null)
                {
                    Session["User"] = user;

                    return View("Home", user);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel info)
        {
            if (string.IsNullOrEmpty(info.User.email) || string.IsNullOrEmpty(info.User.password)) return View();

            var user = _dao.GetUserByLoginInfo(info.User);

            if (user == null)
            {
                ViewBag.Message = "Wrong email or password!";
                return View();
            }

            if (info.Remember)
            {
                var cookie = new HttpCookie("TheProjectToken");
                cookie.Value = user.token;
                Response.Cookies.Add(cookie);
            }

            Session["User"] = user;


            return View("Home", user);
        }

        #endregion
    }
}