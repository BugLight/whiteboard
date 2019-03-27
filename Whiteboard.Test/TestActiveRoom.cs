using NUnit.Framework;
using Whiteboard.Models;

namespace Whiteboard.Test
{
    [TestFixture]
    public class TestActiveRoom
    {
        private Connection connection;
        private Room room;

        [SetUp]
        public void SetUp()
        {
            connection = new Connection("");
            room = new Room();
        }

        [Test]
        public void TestInitialCount()
        {
            var activeRoom = new ActiveRoom(room);
            Assert.That(activeRoom.ConnectionsCount == 0);
        }

        [Test]
        public void TestIncCount()
        {
            var activeRoom = new ActiveRoom(room)
            {
                MaxConnections = 1
            };
            var count = activeRoom.ConnectionsCount;
            activeRoom.Join(connection);
            Assert.That(activeRoom.ConnectionsCount == count + 1);
        }

        [Test]
        public void TestDecCount()
        {
            var activeRoom = new ActiveRoom(room)
            {
                MaxConnections = 1
            };
            var count = activeRoom.ConnectionsCount;
            activeRoom.Join(connection);
            activeRoom.Leave(connection);
            Assert.That(activeRoom.ConnectionsCount == count);
        }

        [Test]
        public void TestEmptyRoom()
        {
            var activeRoom = new ActiveRoom(room);
            Assert.That(() => activeRoom.Leave(connection), Throws.Exception);
        }

        [Test]
        public void TestFullRoom()
        {
            var activeRoom = new ActiveRoom(room)
            {
                MaxConnections = 1
            };
            for (int i = 0; i < activeRoom.MaxConnections; i++)
                activeRoom.Join(new Connection(""));
            Assert.That(() => activeRoom.Join(connection), Throws.Exception);
        }
    }
}
