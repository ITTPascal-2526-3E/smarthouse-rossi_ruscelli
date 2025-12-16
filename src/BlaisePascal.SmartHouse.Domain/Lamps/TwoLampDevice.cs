using System;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class TwoLampDevice
    {
        private Guid Id; // Unique identifier for the two lamp device
        private Lamp Lamp;
        private EcoLamp EcoLamp;
        
        // Expose the internal lamp instances through properties so tests and callers
        // can access the underlying lamp objects provided to the constructor.
        public Lamp lampProperty { get; set; }
        public EcoLamp ecolampProperty { get; set; }

        /// <summary>
        /// Constructor for Lamp class
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        public TwoLampDevice(Lamp lamp, EcoLamp ecolamp)
        {
            Id = Guid.NewGuid();
            Lamp = lamp;
            EcoLamp = ecolamp;

            // ensure the public properties point to the same instances
            lampProperty = lamp;
            ecolampProperty = ecolamp;
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
                Lamp.ChangeBrightness(EcoLamp.BrightnessProperty);
                return;
            }
            if (EcoLamp.IsInEco(DateTime.Now))
            {
                if (Lamp.BrightnessProperty > EcoLamp.EcoMaxBrightnessProperty)
                {  return; }

            }

            EcoLamp.ChangeBrightness(Lamp.BrightnessProperty);
        }
        public void TurnOnBoth()
        {
            Lamp.TurnOn();
            EcoLamp.TurnOn();
        }
        public void TurnOffBoth()
        {
            Lamp.TurnOff();
            EcoLamp.TurnOff();
        }
    }
}
