using System;
using System.Collections.Generic;

namespace BlaisePascal.SmartHouse.Domain
{
    internal static class Program
    {
        private static void Main()
        {
            // test of lamp class
            Console.WriteLine("=================================");
            Lamp lamp1 = new Lamp(false, "Lampada 1", Lamp.ColorType.CoolWhite, 75, Lamp.LampType.LED);
            Console.WriteLine($"Lamp is on: {lamp1.IsOnProperty}");
            Console.WriteLine($"Name is: {lamp1.NameProperty}");
            Console.WriteLine($"Color is: {lamp1.ColorProperty}");
            Console.WriteLine($"Brightness is: {lamp1.BrightnessProperty}");
            Console.WriteLine($"Lamp type is: {lamp1.LampTypeProperty}");
            Console.WriteLine($"Power consumption is: {lamp1.PowerConsumption} W");
            lamp1.TurnOn();
            lamp1.BrightnessProperty = 90;
            lamp1.ChangeColor(Lamp.ColorType.WarmWhite);
            Console.WriteLine($"Lamp is on: {lamp1.IsOnProperty}");
            Console.WriteLine($"Name is: {lamp1.NameProperty}");
            Console.WriteLine($"Color is: {lamp1.ColorProperty}");
            Console.WriteLine($"Brightness is: {lamp1.BrightnessProperty}");
            Console.WriteLine($"Lamp type is: {lamp1.LampTypeProperty}");
            Console.WriteLine($"Power consumption is: {lamp1.PowerConsumption} W");
            lamp1.TurnOff();
            //test of ecolamp class
            Console.WriteLine("=================================");
            EcoLamp ecoLamp1 = new EcoLamp(false, "EcoLamp 1", EcoLamp.ColorType.WarmWhite, 60, EcoLamp.LampType.CFL);
            Console.WriteLine($"EcoLamp is on: {ecoLamp1.IsOnProperty}");
            Console.WriteLine($"Name is: {ecoLamp1.NameProperty}");
            Console.WriteLine($"Color is: {ecoLamp1.ColorProperty}");
            Console.WriteLine($"Brightness is: {ecoLamp1.BrightnessProperty}");
            Console.WriteLine($"Lamp type is: {ecoLamp1.LampTypeProperty}");
            Console.WriteLine($"Power consumption is: {ecoLamp1.PowerConsumption} W");
            TimeSpan defaultAutoOff = TimeSpan.FromMinutes(120);
            TimeSpan ecoAutoOff = TimeSpan.FromMinutes(60);
            ecoLamp1.ChangeTimer(defaultAutoOff, ecoAutoOff);
            bool enebleEcoMode = true;
            TimeOnly start = new TimeOnly(18, 0);
            TimeOnly end = new TimeOnly(13, 0);
            int maxEcoBrightness = 50;
            ecoLamp1.ChangeEcoMode(enebleEcoMode, start, end, maxEcoBrightness);
            ecoLamp1.TurnOn();
                
            Console.WriteLine($"Brightness is: {ecoLamp1.BrightnessProperty}");
            Console.WriteLine($"Power consumption is: {ecoLamp1.PowerConsumption} W");
            Console.WriteLine($"Auto-off scheduled at: {ecoLamp1.ScheduledOffAt}");
            ecoLamp1.TurnOff();

        }
    }
}
