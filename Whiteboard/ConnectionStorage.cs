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
            connections[id] = item;
        }

        public Connection GetById(string id)
        {
            if (!connections.ContainsKey(id))
                return null;
            return connections[id];
        }

        public void Remove(string id)
        {
            connections.TryRemove(id, out _);
        }
    }
}
