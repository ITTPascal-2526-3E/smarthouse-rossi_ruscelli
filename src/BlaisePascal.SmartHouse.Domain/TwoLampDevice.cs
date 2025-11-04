using System;

namespace BlaisePascal.SmartHouse.Domain
{
    public class TwoLampDevice
    {
        private Lamp Lamp;
        private EcoLamp EcoLamp;


        /// <summary>
        /// Constructor for Lamp class
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        public TwoLampDevice(Lamp lamp, EcoLamp ecolamp)
        {
            Lamp = lamp;
            EcoLamp = ecolamp;
        }



      


       

      

    }
}
