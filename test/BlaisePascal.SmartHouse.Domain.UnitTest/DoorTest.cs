using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.Door;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class DoorTest
    {
        [Fact]
        public void Constructor_ShouldInitializeCorrectly()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, false, new NameDevice("testDoor"));

            Assert.False(door.IsOpenProperty);
            Assert.Equal("testDoor", door.NameProperty.Value);
        }

        // Applica lo stesso fix a tutti i punti dove usi 'Door'
        [Fact]
        public void Constructor_ShouldSetInitialState()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(true, true, new NameDevice("door1"));

            Assert.True(door.IsLocked);
            Assert.True(door.IsOpen);
        }

        [Fact]
        public void Lock_ShouldLockDoor_WhenClosed()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, false, new NameDevice("door"));

            door.Lock();

            Assert.True(door.IsLocked);
        }

        [Fact]
        public void Lock_ShouldNotLockDoor_WhenOpen()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, true, new NameDevice("door"));

            door.Lock();

            Assert.False(door.IsLocked);
        }

        [Fact]
        public void Unlock_ShouldUnlockDoor()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(true, false, new NameDevice("door"));

            door.Unlock();

            Assert.False(door.IsLocked);
        }

        [Fact]
        public void Open_ShouldOpenDoor_WhenUnlocked()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, false, new NameDevice("door"));

            door.Open();

            Assert.True(door.IsOpen);
        }

        [Fact]
        public void Open_ShouldNotOpenDoor_WhenLocked()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(true, false, new NameDevice("door"));

            door.Open();

            Assert.False(door.IsOpen);
        }

        [Fact]
        public void Close_ShouldCloseDoor()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, true, new NameDevice("door"));

            door.Close();

            Assert.False(door.IsOpen);
            Assert.False(door.IsOpenProperty);
        }

        [Fact]
        public void Close_ShouldKeepDoorClosed_IfAlreadyClosed()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, false, new NameDevice("door"));

            door.Close();

            Assert.False(door.IsOpen);
        }

        [Fact]
        public void Open_ThenClose_ShouldUpdateStateCorrectly()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, false, new NameDevice("door"));

            door.Open();
            door.Close();

            Assert.False(door.IsOpen);
        }

        [Fact]
        public void Unlock_ThenOpen_ShouldOpenDoor()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(true, false, new NameDevice("door"));

            door.Unlock();
            door.Open();

            Assert.True(door.IsOpen);
        }

        [Fact]
        public void Lock_AfterClose_ShouldLockDoor()
        {
            var door = new BlaisePascal.SmartHouse.Domain.Door.Door(false, true, new NameDevice("door"));

            door.Close();
            door.Lock();

            Assert.True(door.IsLocked);
        }
    }
}