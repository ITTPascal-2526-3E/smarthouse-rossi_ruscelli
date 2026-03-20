using BlaisePascal.SmartHouse.Domain.Abstractions;
using System;
using BlaisePascal.SmartHouse.Domain.Door.DoorInterfaces;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.Door
{
    public sealed class Door : AbstractDevice, ILockable, IOpenable
    {
        public bool IsLocked { get; private set; }
        public bool IsOpen { get; private set; }
        public bool IsOpenProperty { get; private set; }
        public NameDevice NameProperty { get; private set; }

        public Door(bool isLocked, bool isOpen, NameDevice name) : base(name)
        {
            IsLocked = isLocked;
            IsOpen = isOpen;
            IsOpenProperty = isOpen; // expose public property for tests
            NameProperty = name; // expose NameDevice for tests
        }

        public void Lock()
        {
            if(IsOpen)
                return;
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }

        public void Open()
        {
            if (!IsLocked)
            {
                IsOpen = true;
            }
        }

        public void Close()
        {
            IsOpen = false;
            IsOpenProperty = false;
        }

        public void Update(bool isLocked, bool isOpen)
        {
            throw new NotImplementedException();
        }
    }
}
