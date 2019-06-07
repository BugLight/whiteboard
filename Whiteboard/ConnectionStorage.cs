using System.Collections.Concurrent;

namespace Whiteboard
{
    public class ConnectionStorage : IConnectionStorage
    {
        private readonly ConcurrentDictionary<string, Connection> connections = new ConcurrentDictionary<string, Connection>();

        public void Add(string id, Connection item)
        {
            item.OnClosed += (sender, args) =>
            {
                Remove(id);
            };
            connections.TryAdd(id, item);
        }

        public Connection GetById(string id)
        {
            if (!connections.ContainsKey(id))
                return null;
            if (connections.TryGetValue(id, out var connection))
                return connection;
            else
                return null;

        }

        public void Remove(string id)
        {
            connections.TryRemove(id, out _);
        }
    }
}
