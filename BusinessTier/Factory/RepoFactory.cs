using BusinessTier.Module;
using BusinessTier.Repository;
using Ninject;

namespace BusinessTier.Factory
{
    public class RepoFactory
    {
        private static IKernel _kernel;

        public static IRepo GetRepo(string name)
        {
            if (_kernel == null) _kernel = new StandardKernel(new RepoModule());

            return _kernel.Get<IRepo>(name);
        }
    }
}