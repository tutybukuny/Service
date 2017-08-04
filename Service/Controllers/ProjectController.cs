using System.Web.Mvc;
using BusinessTier.Factory;
using BusinessTier.Repository;
using DataTier;
using DataTier.Dao;
using DataTier.Factory;

namespace Service.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectRepo _repo;

        public ProjectController()
        {
            _repo = (ProjectRepo) RepoFactory.GetRepo("ProjectRepo");
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
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var projects = _repo.GetUserProjects(((User)Session["User"]).id)["projects"];
            return View(projects);
        }
    }
}