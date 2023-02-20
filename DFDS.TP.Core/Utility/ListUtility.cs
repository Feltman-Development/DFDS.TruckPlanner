namespace DFDS.TP.Core.Utility;

public static class ListUtility
{
    public static void AddIfNotNull<TItem>(this IList<TItem> items, TItem item)
    {
        if (item != null) items.Add(item);
    }

    public static void AddAndSort<TItem>(this IList<TItem> list, TItem item) where TItem : IComparable<TItem>, IEquatable<TItem>
    {
        if (list.Contains(item)) return;

        list.Add(item);
        Sort(list);
    }

    public static void Sort<TItem>(this IList<TItem> list) where TItem : IComparable<TItem>, IEquatable<TItem>
    {
        var sorted = list.OrderBy(x => x).ToList();

        var pointer = 0;
        while (pointer < sorted.Count)
        {
            if (!list[pointer].Equals(sorted[pointer]))
            {
                var t = list[pointer];
                list.RemoveAt(pointer);
                list.Insert(sorted.IndexOf(t), t);
            }
            else
            {
                pointer++;
            }
        }
    }
}