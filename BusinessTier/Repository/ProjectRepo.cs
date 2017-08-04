using System.Collections.Generic;
using DataTier.Dao;
using DataTier.Factory;

namespace BusinessTier.Repository
{
    public class ProjectRepo : IRepo
    {
        private readonly ProjectDao _dao;

        public ProjectRepo()
        {
            _dao = (ProjectDao) DaoFactory.GetDao("ProjectDao");
        }

        #region Get All

        public Dictionary<string, object> GetAll()
        {
            var dic = new Dictionary<string, object>();
            var projects = _dao.GetAll();

            if (projects == null) dic.Add("message", "There are no project!");
            else dic.Add("projects", projects);

            return dic;
        }

        #endregion

        #region Get First Project

        public Dictionary<string, object> GetUserFirstProject(int user_id)
        {
            var dic = new Dictionary<string, object>();

            var project = _dao.GetUserFirstProject(user_id);

            if (project == null)
                dic.Add("message", "User doesn't have project!");

            dic.Add("project", project);

            return dic;
        }

        #endregion

        #region Get User Projects

        public Dictionary<string, object> GetUserProjects(int user_id)
        {
            var dic = new Dictionary<string, object>();
            var projects = _dao.GetUserProjects(user_id);

            if (projects == null)
                dic.Add("message", "User does'nt have project!");
            else dic.Add("projects", projects);

            return dic;
        }

        #endregion
    }
}