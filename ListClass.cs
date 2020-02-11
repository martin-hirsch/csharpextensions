using System.Collections.Generic;
using System.Linq;

public abstract class ListClass<T>
{
    protected ListClass(IEnumerable<T> items)
    {
        Items = items.ToList();
    }

    protected ListClass()
    {
        Items = new List<T>();
    }

    public int Count => Items.Count;

    public IList<T> Items { get; protected set; }

    public void Add(T other)
    {
        Items.Add(other);
    }

    public void AddRange(IEnumerable<T> other)
    {
        foreach (var item in other)
        {
            Items.Add(item);
        }
    }
}
