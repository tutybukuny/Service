using System.Web.Mvc;
using CoreServiceLib.DAO;
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

        #region Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel info)
        {
            var user = _dao.GetUserByLoginInfo(info.User);

            if (user == null)
            {
                ViewBag.message = "Wrong email or password!";
                return View();
            }

            return View("Home", user);
        }
        #endregion

        public ActionResult ForgotPassword()
        {
            return View();
        }

        #region Register

        public ActionResult Register()
        {
            return View();
        }

        #endregion
    }
}