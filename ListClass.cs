public abstract class ListClass<T>
{
    protected ListClass(IEnumerable<T> items)
    {
        Items = items.ToList();
    }

    public void AddRange(IEnumerable<T> other)
    {
        foreach (var item in other)
        {
            Items.Add(item);
        }
    }
    
    protected ListClass()
    {
        Items = new List<T>();
    }

    public int Count => Items.Count;
    public IList<T> Items { get; protected set; }
}
