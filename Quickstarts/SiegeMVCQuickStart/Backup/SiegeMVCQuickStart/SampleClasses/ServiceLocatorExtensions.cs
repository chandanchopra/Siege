using Siege.ServiceLocation;
using SiegeMVCQuickStart.Controllers;

namespace SiegeMVCQuickStart.SampleClasses
{
    public static class ServiceLocatorExtensions
    {
        public static IServiceLocator WithControllers(this IServiceLocator locator)
        {
            return locator
                        .Register(Given<HomeController>.Then<HomeController>())
                        .Register(Given<HomeController>
                                    .When<User>(user => user.Role == UserRoles.Standard)
                                    .Then<UserHomeController>())
                        .Register(Given<HomeController>
                                    .When<User>(user => user.Role == UserRoles.Admin)
                                    .Then<AdminHomeController>())
                        .Register(Given<HomeController>
                                    .When<User>(user => user.Role == UserRoles.Webmaster)
                                    .Then<AdminHomeController>());
        }

        public static IServiceLocator WithServices(this IServiceLocator locator)
        {
            return locator
                        .Register(Given<IAdminService>.Then<AdminService>())
                        .Register(Given<IAccountService>
                                        .When<User>(user => user.Account is TrialAccount)
                                        .Then<TrialAccountService>())
                        .Register(Given<IAccountService>
                                        .When<User>(user => user.Account is PaidAccount)
                                        .Then<PaidAccountService>());
        }

        public static IServiceLocator WithFinders(this IServiceLocator locator)
        {
            return locator
                        .Register(Given<IUserFinder>
                                      .When<User>(user => user.Role == UserRoles.Webmaster)
                                      .Then(new UserFinder().WithAdmins().WithWebmasters()))
                        .Register(Given<IUserFinder>
                                       .When<User>(user => user.Role == UserRoles.Admin)
                                       .Then(new UserFinder().WithAdmins()));
        }
    }
}
