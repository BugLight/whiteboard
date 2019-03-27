using System;

namespace Whiteboard
{
    public class Connection
    {
        public string Id { get; }
        public ActiveRoom Room { get; set; }
        public event EventHandler OnClosed;

        public Connection(string id)
        {
            Id = id;
        }

        public void Close()
        {
            OnClosed?.Invoke(this, null);
        }
    }
}
