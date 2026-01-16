using BlaisePascal.SmartHouse.Domain.Abstractions;
using System;
using BlaisePascal.SmartHouse.Domain.Interfaces;

namespace BlaisePascal.SmartHouse.Domain.Door
{
    public sealed class Door : AbstractDevice, ILockable, IOpenable
    {
        public bool IsLocked { get; private set; }
        public bool IsOpen { get; private set; }

        public Door(bool isLocked, bool isOpen, string name) : base(name)
        {
            IsLocked = isLocked;
            IsOpen = isOpen;
            
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
        }
    }
}
