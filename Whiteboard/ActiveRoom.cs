using System;
using Whiteboard.Models;

namespace Whiteboard
{
    public class ActiveRoom : Room
    {
        private int connectionsCount = 0;
        public int ConnectionsCount
        {
            get
            {
                lock (roomLock)
                    return connectionsCount;
            }
        }
        private readonly object roomLock = new object();

        public event EventHandler<Connection> OnJoined;
        public event EventHandler<Connection> OnLeft;

        public ActiveRoom(Room room)
        {
            MaxConnections = room.MaxConnections;
            Name = room.Name;
            Id = room.Id;
            Canvas = room.Canvas;
        }

        public void Join(Connection connection)
        {
            lock (roomLock)
            {
                if (connectionsCount == MaxConnections)
                    throw new Exception();
                connectionsCount++;
                connection.Room = this;
                connection.OnClosed += ConnectionClosed;
            }
            var handler = OnJoined;
            handler?.Invoke(this, connection);
        }

        public void Leave(Connection connection)
        {
            lock (roomLock)
            {
                if (connectionsCount == 0)
                    throw new Exception();
                connectionsCount--;
                connection.OnClosed -= ConnectionClosed;
                connection.Room = null;
            }
            var handler = OnLeft;
            handler?.Invoke(this, connection);
        }

        private void ConnectionClosed(object sender, EventArgs args)
        {
            Leave((Connection)sender);
        }
    }
}
