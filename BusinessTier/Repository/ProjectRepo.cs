using System.Collections.Generic;
using DataTier;
using DataTier.Dao;
using DataTier.Factory;

namespace BusinessTier.Repository
{
    public class ProjectRepo : IRepo
    {
        private readonly CategoryDao _categoryDao;
        private readonly LikeDao _likeDao;
        private readonly ProjectDao _projectDao;
        private readonly UserDao _userDao;
        private readonly TokenDao _tokenDao;

        public ProjectRepo()
        {
            _projectDao = (ProjectDao) DaoFactory.GetDao("ProjectDao");
            _categoryDao = (CategoryDao) DaoFactory.GetDao("CategoryDao");
            _userDao = (UserDao) DaoFactory.GetDao("UserDao");
            _likeDao = (LikeDao) DaoFactory.GetDao("LikeDao");
            _tokenDao = (TokenDao) DaoFactory.GetDao("TokenDao");
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

        public Dictionary<string, object> GetUserProjects(int user_id, int limit)
        {
            var dic = new Dictionary<string, object>();
            var projects = _projectDao.GetUserProjects(user_id, limit);

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

        #region Join Project

        public Dictionary<string, object> JoinProject(JoinedProject joinedProject)
        {
            var dic = new Dictionary<string, object>();
            var messages = new List<string>();
            var success = true;
            var project = _projectDao.GetById(joinedProject.project_id);
            var user = _userDao.GetById(joinedProject.user_id);

            if (project == null)
            {
                messages.Add("Project is not exist!");
                success = false;
            }

            if (user == null)
            {
                messages.Add("User is not exist!");
                success = false;
            }

            if (!_projectDao.ValidateRole(joinedProject.project_id, joinedProject.role_id))
            {
                messages.Add("This project is not open this role!");
                success = false;
            }

            if (project != null && project.joined_people + 1 > project.people)
            {
                messages.Add("Project is full!");
                success = false;
            }

            if (success)
            {
                success = _projectDao.JoinProject(joinedProject);
                messages.Add(success ? "Joined success!" : "Something went wrong!");
            }

            dic.Add("success", success);
            dic.Add("messages", messages);

            return dic;
        }

        #endregion

        #region Like and Unlike

        public Dictionary<string, object> Like(string token, int project_id)
        {
            var dic = new Dictionary<string, object>();
            var user_id = _tokenDao.GetUserId(token);
            bool success;
            string message;

            if (user_id == -1)
            {
                success = false;
                message = "Token is invalid!";
            }
            else
            {
                success = _likeDao.Insert(new Like {user_id = user_id, project_id = project_id});
                message = success ? "Success!" : "Something went wrong!";
            }

            dic.Add("success", success);
            dic.Add("message", message);

            return dic;
        }

        public Dictionary<string, object> Unlike(string token, int project_id)
        {
            var dic = new Dictionary<string, object>();
            var user_id = _tokenDao.GetUserId(token);
            bool success;
            string message;

            if (user_id == -1)
            {
                success = false;
                message = "Token is invalid!";
            }
            else
            {
                success = _likeDao.Delete(user_id, project_id);
                message = success ? "Success!" : "Something went wrong!";
            }

            dic.Add("success", success);
            dic.Add("message", message);

            return dic;
        }

        public Dictionary<string, object> IsLikedByUser(int project_id, int user_id)
        {
            return new Dictionary<string, object> {{"like", _likeDao.IsLikedByUser(project_id, user_id)}};
        }

        #endregion
    }
}