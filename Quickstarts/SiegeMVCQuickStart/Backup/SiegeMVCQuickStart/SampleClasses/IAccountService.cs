namespace SiegeMVCQuickStart.SampleClasses
{
    public interface IAccountService
    {
        string GetAccountDetails();
    }

    public class TrialAccountService : IAccountService
    {
        public string GetAccountDetails()
        {
            return "This is a trial account.";
        }
    }

    public class PaidAccountService : IAccountService
    {
        public string GetAccountDetails()
        {
            return "This is a paid account.";
        }
    }
}
