using System;

namespace BlaisePascal.SmartHouse.Domain.Door
{
    public class Door
    {
        public bool IsLocked { get; private set; }
        public bool IsOpen { get; private set; }
        public string Name { get; private set; }


        public Door(bool isLocked, bool isOpen, string name)
        {
            IsLocked = isLocked;
            IsOpen = isOpen;
            Name = name;
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
