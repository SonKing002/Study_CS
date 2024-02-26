namespace ConsoleApp1.Interfaces
{
    public interface ICollection_EX<T> : IEnumerable<T>
    {
       int Count { get; }
        bool IsReadOnly { get; }

        void Add(T item);

        void Clear();

        bool Contains(T item);

        void CopyTo(T[] array, int arrayIndex);

        bool Remove(T item);
    }

}

