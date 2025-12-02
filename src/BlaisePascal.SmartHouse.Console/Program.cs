using BlaisePascal.SmartHouse.Domain.Lamps;
using AF = BlaisePascal.SmartHouse.Domain.AirFryer;
using System;
using System.Collections.Generic;
using System.Threading;


namespace BlaisePascal.SmartHouse.Domain
{
    internal static class Program
    {
        private static void Main()
        {
            // test of lamp class
            Console.WriteLine("=================================");
            Lamp lamp1 = new Lamp(false, "Lampada 1", ColorType.CoolWhite, 75, LampType.LED);
            Console.WriteLine($"Lamp is on: {lamp1.IsOnProperty}");
            Console.WriteLine($"Name is: {lamp1.NameProperty}");
            Console.WriteLine($"Color is: {lamp1.ColorProperty}");
            Console.WriteLine($"Brightness is: {lamp1.BrightnessProperty}");
            Console.WriteLine($"Lamp type is: {lamp1.LampTypeProperty}");
            Console.WriteLine($"Power consumption is: {lamp1.PowerConsumption} W");
            lamp1.TurnOn();
            lamp1.BrightnessProperty = 90;
            lamp1.ChangeColor(ColorType.WarmWhite);
            Console.WriteLine($"Lamp is on: {lamp1.IsOnProperty}");
            Console.WriteLine($"Name is: {lamp1.NameProperty}");
            Console.WriteLine($"Color is: {lamp1.ColorProperty}");
            Console.WriteLine($"Brightness is: {lamp1.BrightnessProperty}");
            Console.WriteLine($"Lamp type is: {lamp1.LampTypeProperty}");
            Console.WriteLine($"Power consumption is: {lamp1.PowerConsumption} W");
            lamp1.TurnOff();
            //test of ecolamp class
            Console.WriteLine("=================================");
            EcoLamp ecoLamp1 = new EcoLamp(false, "EcoLamp 1", ColorType.WarmWhite, 60, LampType.CFL);
            Console.WriteLine($"EcoLamp is on: {ecoLamp1.IsOnProperty}");
            Console.WriteLine($"Name is: {ecoLamp1.NameProperty}");
            Console.WriteLine($"Color is: {ecoLamp1.ColorProperty}");
            Console.WriteLine($"Brightness is: {ecoLamp1.BrightnessProperty}");
            Console.WriteLine($"Lamp type is: {ecoLamp1.LampTypeProperty}");
            Console.WriteLine($"Power consumption is: {ecoLamp1.PowerConsumption} W");
            TimeSpan defaultAutoOff = TimeSpan.FromMinutes(120);
            TimeSpan ecoAutoOff = TimeSpan.FromMinutes(60);
            ecoLamp1.ChangeTimers(defaultAutoOff, ecoAutoOff);
            bool enebleEcoMode = true;
            TimeOnly start = new TimeOnly(18, 0);
            TimeOnly end = new TimeOnly(13, 0);
            int maxEcoBrightness = 50;
            ecoLamp1.ChangeEcoMode(enebleEcoMode, start, end, maxEcoBrightness);
            ecoLamp1.TurnOnEco();

            Console.WriteLine($"Brightness is: {ecoLamp1.BrightnessProperty}");
            Console.WriteLine($"Power consumption is: {ecoLamp1.PowerConsumption} W");
            Console.WriteLine($"Auto-off scheduled at: {ecoLamp1.ScheduledOffAt}");
            ecoLamp1.TurnOff();

            // test of AirFryer class
            Console.WriteLine("=================================");
            Console.WriteLine("AirFryer test:");

            // create an AirFryer instance and initialize (turned off initially)
            AF.AirFryer airFryer = new AF.AirFryer();
            airFryer.AirFry(0, 200, false, 10000.0f);

            // set the mode and turn the air fryer on
            airFryer.SetMode(AF.Mode.frying);
            airFryer.TurnOn();

            Console.WriteLine("AirFryer turned on. Simulating 5 seconds of runtime...");
            Thread.Sleep(5000); // simulate the air fryer being on for 5 seconds

            // turn off the air fryer (records the turn-off time internally)
            airFryer.TurnOff();

            // ensure TurnedOffAtProperty is updated (mainly for consistency)
            airFryer.TurnedOffAtProperty = DateTime.Now;

            // display state and usage data
            Console.WriteLine($"AirFryer is on: {airFryer.IsOnProperty}");
            TimeSpan time = airFryer.TimeOn();
            Console.WriteLine($"Time on: {time:hh\\:mm\\:ss}"); // to take only secondsss

            // calculate consumption and cost
            double wh = airFryer.ConsumptionWattHours(AF.Mode.frying);
            double kwh = airFryer.ConsumptionKiloWattHours(AF.Mode.frying);
            double cost = airFryer.CostOfConsumption(AF.Mode.frying);

            Console.WriteLine($"Consumption (Wh): {wh:F2}");
            Console.WriteLine($"Consumption (kWh): {kwh:F4}");
            Console.WriteLine($"Estimated cost: {cost:C}");


        }
    }
}