using System.Collections.Generic;

namespace SiegeMVCQuickStart.SampleClasses
{
    public class UserFinder : IUserFinder
    {
        private bool withSuperUsers = false;
        private bool withPowerUsers = false;

        public List<User> Find()
        {
            List<User> users = new List<User> {new User("Joe User", UserRoles.Standard), new User("Jane User", UserRoles.Standard)};

            if(withPowerUsers) users.Add(new User("Admin User", UserRoles.Admin));
            if(withSuperUsers) users.Add(new User("Webmaster", UserRoles.Webmaster));

            return users;
        }

        public UserFinder WithWebmasters()
        {
            withSuperUsers = true;
            return this;
        }

        public UserFinder WithAdmins()
        {
            withPowerUsers = true;
            return this;
        }
    }
}