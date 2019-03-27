using NUnit.Framework;

namespace Whiteboard.Test
{
    [TestFixture]
    public class TestConnectionStorage
    {
        private ConnectionStorage connectionStorage;

        [SetUp]
        public void SetUp()
        {
            connectionStorage = new ConnectionStorage();
        }

        [Test]
        public void TestAddandGet()
        {
            var id = "1";
            var c = new Connection(id);
            connectionStorage.Add(id, c);
            Assert.That(c == connectionStorage.GetById(id));
        }

        [Test]
        public void TestNotExisting()
        {
            Assert.That(connectionStorage.GetById("nothing") == null);
        }

        [Test]
        public void TestRemove()
        {
            var id = "2";
            var c = new Connection(id);
            connectionStorage.Add(id, c);
            connectionStorage.Remove(id);
            Assert.That(connectionStorage.GetById(id) == null);
        }
    }
}
