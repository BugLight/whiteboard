namespace Whiteboard
{
    public interface IStorage<T, K>
    {
        T GetById(K id);
        void Add(K id, T item);
        void Remove(K id);
    }
}
