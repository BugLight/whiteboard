using System.Collections.Generic;

namespace Whiteboard
{
    public class ConnectionStorage : IConnectionStorage
    {
        private static readonly Dictionary<string, Connection> connections = new Dictionary<string, Connection>();

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
            connections.Remove(id);
        }
    }
}
