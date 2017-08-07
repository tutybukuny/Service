using System.Collections.Generic;
using DataTier.Dao;
using DataTier.Factory;

namespace BusinessTier.Repository
{
    public class ProjectRepo : IRepo
    {
        private readonly CategoryDao _categoryDao;
        private readonly ProjectDao _projectDao;

        public ProjectRepo()
        {
            _projectDao = (ProjectDao) DaoFactory.GetDao("ProjectDao");
            _categoryDao = (CategoryDao) DaoFactory.GetDao("CategoryDao");
        }

        #region Get By Id

        public Dictionary<string, object> GetById(int project_id)
        {
            var dic = new Dictionary<string, object>();
            var project = _projectDao.GetById(project_id);

            if (project == null) dic.Add("message", "No project likes this!");
            dic.Add("project", project);

            return dic;
        }

        #endregion

        #region Get All

        public Dictionary<string, object> GetAll()
        {
            var dic = new Dictionary<string, object>();
            var projects = _projectDao.GetAll();

            if (projects == null) dic.Add("message", "There are no project!");

            dic.Add("projects", projects);

            return dic;
        }

        #endregion

        #region Get First Project

        public Dictionary<string, object> GetUserFirstProject(int user_id)
        {
            var dic = new Dictionary<string, object>();

            var project = _projectDao.GetUserFirstProject(user_id);

            if (project == null) dic.Add("message", "User doesn't have project!");

            dic.Add("project", project);

            return dic;
        }

        #endregion

        #region Get User Projects

        public Dictionary<string, object> GetUserProjects(int user_id)
        {
            var dic = new Dictionary<string, object>();
            var projects = _projectDao.GetUserProjects(user_id);

            if (projects == null) dic.Add("message", "User does'nt have project!");
            dic.Add("projects", projects);

            return dic;
        }

        #endregion

        #region Get Categories

        public Dictionary<string, object> GetCategories()
        {
            var dic = new Dictionary<string, object>();
            var list = _categoryDao.GetAll();

            dic.Add("message", list == null ? "There is no category!" : "Successful!");
            dic.Add("categories", list);

            return dic;
        }

        #endregion

        #region Get Filtered Projects

        public Dictionary<string, object> GetFilteredProject(int category_id, int sort_id, int role_id)
        {
            var dic = new Dictionary<string, object>();
            var list = _projectDao.GetFilteredProjects(category_id, sort_id, role_id);

            if (list == null) dic.Add("message", "No project likes this!");
            dic.Add("projects", list);

            return dic;
        }

        #endregion
    }
}