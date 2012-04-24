using System.Collections.Generic;

namespace SiegeMVCQuickStart.SampleClasses
{
    public class AdminService : IAdminService
    {
        private readonly IUserFinder finder;

        public AdminService(IUserFinder finder)
        {
            this.finder = finder;
        }

        public List<User> GetUsers()
        {
            return finder.Find();
        }
    }
}