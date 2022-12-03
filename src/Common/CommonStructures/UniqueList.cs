using System.Collections;

namespace CommonStructures;

public class UniqueList<T> : ICollection<T>, IList<T>, ICloneable
{
    private readonly List<T> _data;

    public UniqueList()
    {
        _data = new List<T>();
    }

    public UniqueList(IEnumerable<T> values) : this()
    {
        foreach (var item in values)
        {
            _data.Add(item);
        }
    }

    public T this[int index]
    {
        get
        {
            return _data[index];
        }
        set
        {
            if (_data.Contains(value))
            {
                throw new InvalidOperationException("Item is already contains in list");
            }
            _data[index] = value;
        }
    }

    public int Count => _data.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        if (!_data.Contains(item))
        {
            _data.Add(item);
        }
    }

    public void Clear()
    {
        _data.Clear();
    }

    public object Clone()
    {
        UniqueList<T> clone = new();
        foreach (var item in _data)
        {
            clone.Add(item);
        }
        return clone;
    }

    public bool Contains(T item)
    {
        return _data.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _data.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return _data.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        if (!_data.Contains(item))
        {
            _data.Insert(index, item);
        }
    }

    public bool Remove(T item)
    {
        return _data.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _data.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    public static implicit operator UniqueList<T>(List<T> values)
    {
        UniqueList<T> result = new();
        foreach (var item in values)
        {
            result.Add(item);
        }
        return result;
    }

    public static implicit operator List<T>(UniqueList<T> values)
    {
        List<T> result = new();
        foreach (var item in values)
        {
            result.Add(item);
        }
        return result;
    }

    public static implicit operator UniqueList<T>(T[] values)
    {
        UniqueList<T> result = new();
        foreach (var item in values)
        {
            result.Add(item);
        }
        return result;
    }

    public static implicit operator T[](UniqueList<T> values)
    {
        T[] result = new T[values.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = values[i];
        }
        return result;
    }
}
