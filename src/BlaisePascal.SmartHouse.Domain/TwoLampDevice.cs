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
        /// <summary>
        /// set the same brightness to the 2 lamps, but you have to check if in the eco lamp you can have that brightness if you are in eco mode, true then they get set to the lamp
        /// false they get set to the eco lamp
        /// </summary>
        /// <param name="wichLamp"></param>
        public void SetSameBrightness(bool whichLamp)
        {
            if (!whichLamp)
            {
                Lamp.BrightnessProperty = EcoLamp.BrightnessProperty;
                return;
            }
            if (EcoLamp.IsInEco(DateTime.Now))
            {
                if (Lamp.BrightnessProperty > EcoLamp.EcoMaxBrightnessProperty)
                { Console.WriteLine("Eco lamp cannot be set to the lamp brightness because it is in eco mode"); return; }
                
            }

            EcoLamp.BrightnessProperty=Lamp.BrightnessProperty;
        }
        public void SetSameColor(bool whichLamp)
        {
            

            EcoLamp.BrightnessProperty = Lamp.BrightnessProperty;
        }

    }
}
