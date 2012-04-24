using System.Collections.Generic;

namespace SiegeMVCQuickStart.SampleClasses
{
    public interface IAdminService
    {
        List<User> GetUsers();
    }
}
