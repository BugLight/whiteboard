using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using Whiteboard.Models;

namespace Whiteboard.Test
{
    [TestFixture]
    public class TestActiveRoomStorage
    {
        private AppContext context;
        private ActiveRoomStorage activeRoomStorage;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppContext>()
                .UseInMemoryDatabase("test")
                .Options;
            activeRoomStorage = new ActiveRoomStorage(options);
        }

        [Test]
        public void TestAddAndGet()
        {
            var id = new Guid();
            var room = new Room
            {
                Id = id
            };
            var activeRoom = new ActiveRoom(room);
            activeRoomStorage.Add(id, activeRoom);
            Assert.That(activeRoomStorage.GetById(id) == activeRoom);
        }

        [Test]
        public void TestNotExisting()
        {
            var id = new Guid("22345200-abe8-4f60-90c8-0d43c5f6c0f6");
            Assert.That(activeRoomStorage.GetById(id) == null);
        }

        [Test]
        public void TestRemove()
        {
            var id = new Guid();
            var room = new Room
            {
                Id = id
            };
            var activeRoom = new ActiveRoom(room);
            activeRoomStorage.Add(id, activeRoom);
            activeRoomStorage.Remove(id);
            Assert.That(activeRoomStorage.GetById(id) == null);
        }
    }
}
