using NUnit.Framework;

namespace Whiteboard.Test
{
    [TestFixture]
    public class TestActiveRoom
    {
        private Connection connection;

        [SetUp]
        public void SetUp()
        {
            connection = new Connection("");
        }

        [Test]
        public void TestInitialCount()
        {
            var room = new ActiveRoom();
            Assert.That(room.ConnectionsCount == 0);
        }

        [Test]
        public void TestIncCount()
        {
            var room = new ActiveRoom
            {
                MaxConnections = 1
            };
            var count = room.ConnectionsCount;
            room.Join(connection);
            Assert.That(room.ConnectionsCount == count + 1);
        }

        [Test]
        public void TestDecCount()
        {
            var room = new ActiveRoom
            {
                MaxConnections = 1
            };
            var count = room.ConnectionsCount;
            room.Join(connection);
            room.Leave(connection);
            Assert.That(room.ConnectionsCount == count);
        }

        [Test]
        public void TestEmptyRoom()
        {
            var room = new ActiveRoom();
            Assert.That(() => room.Leave(connection), Throws.Exception);
        }

        [Test]
        public void TestFullRoom()
        {
            var room = new ActiveRoom
            {
                MaxConnections = 1
            };
            for (int i = 0; i < room.MaxConnections; i++)
                room.Join(new Connection(""));
            Assert.That(() => room.Join(connection), Throws.Exception);
        }
    }
}
