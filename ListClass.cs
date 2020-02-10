public abstract class ListClass<T>
{
    public int Count => Items.Count;

    public List<T> Items { get; protected set; }
    
    protected ListClass(IEnumerable<T> items)
    {
        Items = items.ToList();
    }

    protected ListClass()
    {
        Items = new List<T>();
    }

    public void AddRange(IEnumerable<T> other)
    {
        foreach (var item in other)
        {
            Items.Add(item);
        }
    }
}
