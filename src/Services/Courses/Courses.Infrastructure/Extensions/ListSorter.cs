using CommonStructures;

namespace Courses.Infrastructure.Extensions;

public static class ListSorter
{
    
    public static bool ContainsOnOf(this UniqueList<int> list, List<int> modulesIds)
    {
        bool result = false;
        foreach (int id in modulesIds)
        {
            if (list.Contains(id)) result = true;
        }

        return result;
    }
}