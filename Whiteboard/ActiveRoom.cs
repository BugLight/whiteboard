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

        public ActiveRoom(Room room)
        {
            ConnectionsCount = 0;
            MaxConnections = room.MaxConnections;
            Name = room.Name;
            Id = room.Id;
        }

        public void Join(Connection connection)
        {
            mutex.WaitOne();
            if (ConnectionsCount == MaxConnections)
                throw new Exception();
            ConnectionsCount++;
            mutex.ReleaseMutex();
            connection.Room = this;
            connection.OnClosed += ConnectionClosed;
            OnJoined?.Invoke(this, connection);
        }

        public void Leave(Connection connection)
        {
            mutex.WaitOne();
            if (ConnectionsCount == 0)
                throw new Exception();
            ConnectionsCount--;
            mutex.ReleaseMutex();
            connection.OnClosed -= ConnectionClosed;
            connection.Room = null;
            OnLeft?.Invoke(this, connection);
        }

        private void ConnectionClosed(object sender, EventArgs args)
        {
            Leave((Connection)sender);
        }
    }
}
