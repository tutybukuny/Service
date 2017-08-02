using System;
using System.Web;
using System.Web.Mvc;
using BusinessTier.Core;
using BusinessTier.Factory;
using DataTier;
using DataTier.Dao;
using Service.Models;

namespace Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDao _userDao;
        private readonly TokenDao _tokenDao;

        public HomeController()
        {
            _userDao = (UserDao) DaoFactory.GetDao("UserDao");
            _tokenDao = (TokenDao) DaoFactory.GetDao("TokenDao");
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
            if (!_userDao.CheckExistsEmail(user.email))
            {
                _userDao.Insert(user);
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
                var user = _userDao.GetByToken(cookie.Value);

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

            var user = _userDao.Login(info.User.email, info.User.password);

            if (user == null)
            {
                ViewBag.Message = "Wrong email or password!";
                return View();
            }

            var token = TokenGen.AutoGenerate();
            _tokenDao.Insert(new Token {token = token, user_id = user.id, created_date = DateTime.Now});

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