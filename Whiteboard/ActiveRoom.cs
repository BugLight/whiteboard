using System;
using System.Threading;
using Whiteboard.Models;

namespace Whiteboard
{
    public class ActiveRoom : Room
    {
        public int ConnectionsCount { get; private set; }
        private readonly Mutex mutex = new Mutex(); 

        public event EventHandler<Connection> OnJoined;
        public event EventHandler<Connection> OnLeft;

        public void Join(Connection connection)
        {
            mutex.WaitOne();
            if (ConnectionsCount == MaxConnections)
                throw new Exception();
            ConnectionsCount++;
            mutex.ReleaseMutex();
            connection.Room = this;
            connection.OnClosed += (sender, args) =>
            {
                Leave((Connection)sender);
            };
            OnJoined?.Invoke(this, connection);
        }

        public void Leave(Connection connection)
        {
            mutex.WaitOne();
            if (ConnectionsCount == 0)
                throw new Exception();
            ConnectionsCount--;
            mutex.ReleaseMutex();
            connection.Room = null;
            OnLeft?.Invoke(this, connection);
        }
    }
}
