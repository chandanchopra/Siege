namespace SiegeMVCQuickStart.SampleClasses
{
    public class User
    {
        public User() {}
        public User(string UserName, UserRoles role)
        {
            this.UserName = UserName;
            this.Role = role;
        }
        public UserAccount Account { get; set; }
        public string UserName { get; set; }
        public UserRoles Role { get; set; }
    }

    public abstract class UserAccount
    {
        
    }

    public class TrialAccount : UserAccount {}
    public class PaidAccount : UserAccount {}
    
    public enum UserRoles
    {
        Webmaster,
        Admin,
        Standard
    }
}
