using CommonRepository.Models;
using CommonStructures;
using MongoDB.Bson.Serialization.Attributes;

namespace Courses.Domain.Entities.CourseInfo;

public class CourseInfoDbModel : MongoBaseRepositoryEntity
{
    public string ModulesString { get; private set; } = "";

    [BsonIgnore]
    public UniqueList<int> ModulesId
    {
        get
        {
            UniqueList<int> result = new();
            if (!string.IsNullOrEmpty(ModulesString))
            {
                result = ModulesString.Split(',').AsParallel().Select(e => int.Parse(e)).ToList();
            }
            return result;
        }
    }

    public bool TryInsertModule(int moduleId, int index)
    {
        UniqueList<int> result = ModulesId;
        if (result.Contains(moduleId)) return false;
        if (index < 0)
        {
            result.Add(moduleId);
            ModulesString = string.Join(',', result);
            return true;
        }
        else
        {
            result.Insert(index, moduleId);
            ModulesString = string.Join(',', result);
            return true;
        }
    }

    public List<int> TryInsertModules(UniqueList<int> modulesId, int index)
    {
        UniqueList<int> result = ModulesId;
        List<int> modulesCopies = new();
        foreach (int moduleId in modulesId)
        {
            if (result.Contains(moduleId))
            {
                modulesCopies.Add(moduleId);
                modulesId.Remove(moduleId);
            }
        }

        if (index < 0)
        {
            foreach (var item in modulesId)
            {
                result.Add(item);
            }
            ModulesString = string.Join(',', result);
            return modulesCopies;
        }
        else
        {
            for (int i = index; i < i+modulesId.Count; i++)
            {
                result.Insert(i, modulesId[i]);
            }
            ModulesString = string.Join(',', result);
            return modulesCopies;
        }
    }

    public void SetModules(UniqueList<int> modulesId)
    {
        ModulesString = string.Join(',', modulesId);
    }

    public void DeleteModule(int moduleId)
    {
        UniqueList<int> modulesId = ModulesId;
        modulesId.Remove(moduleId);
        ModulesString = string.Join(',', modulesId);
    }
}
