using System;
using Whiteboard.Models;

namespace Whiteboard
{
    public class ActiveRoom : Room
    {
        public int ConnectionsCount { get; private set; }
        private readonly object connectionsCountLock = new object();

        public event EventHandler<Connection> OnJoined;
        public event EventHandler<Connection> OnLeft;

        public ActiveRoom(Room room)
        {
            ConnectionsCount = 0;
            MaxConnections = room.MaxConnections;
            Name = room.Name;
            Id = room.Id;
            Canvas = room.Canvas;
        }

        public void Join(Connection connection)
        {
            lock (connectionsCountLock)
            {
                if (ConnectionsCount == MaxConnections)
                    throw new Exception();
                ConnectionsCount++;
            }
            connection.Room = this;
            connection.OnClosed += ConnectionClosed;
            OnJoined?.Invoke(this, connection);
        }

        public void Leave(Connection connection)
        {
            lock (connectionsCountLock)
            {
                if (ConnectionsCount == 0)
                    throw new Exception();
                ConnectionsCount--;
            }
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
