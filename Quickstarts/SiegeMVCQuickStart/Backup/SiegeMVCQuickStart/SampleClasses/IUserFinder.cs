using System.Collections.Generic;

namespace SiegeMVCQuickStart.SampleClasses
{
    public interface IUserFinder
    {
        List<User> Find();
    }
}