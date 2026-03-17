using System;
using System.Linq;
using BlaisePascal.SmartHouse.Domain.Lightning;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps;

namespace BlaisePascal.SmartHouse.App
{
    internal static class Program
    {
        private static void PrintLampStatus(Lamp lamp)
        {
            Console.WriteLine("=== Lamp status ===");
            Console.WriteLine($"Name: {lamp.NameProperty}");
            Console.WriteLine($"Is On: {lamp.IsOnProperty}");
            Console.WriteLine($"Brightness: {lamp.BrightnessProperty.Value}");
            Console.WriteLine($"Color: {lamp.ColorProperty}");
            Console.WriteLine($"Lamp Type: {lamp.LampTypeProperty}");
            Console.WriteLine($"Power Consumption: {lamp.PowerConsumption} W");
            Console.WriteLine("====================\n");
        }
        private static void Main()
        {
            var lamp = new Lamp(false, new NameDevice("Living Lamp"), ColorType.Daylight, new Brightness(75), LampType.LED);

            // No repository used by default; operate on the lamp in-memory without persistence.

            while (true)
            {
                Console.Clear();
                PrintLampStatus(lamp);

                Console.WriteLine("Commands:");
                Console.WriteLine("1) Turn On");
                Console.WriteLine("2) Turn Off");
                Console.WriteLine("3) Change Brightness");
                Console.WriteLine("4) Change Color");
                Console.WriteLine("5) Change Lamp Type");
                Console.WriteLine("X) Exit");
                Console.Write("Choose: ");

                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.Equals("X", StringComparison.OrdinalIgnoreCase))
                {
                    // Ask user if they want to save the lamp to CSV using the existing repository
                    Console.Write("Vuoi salvare la lamp in CSV? (S/N): ");
                    var saveInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(saveInput) && (saveInput.Equals("S", StringComparison.OrdinalIgnoreCase) || saveInput.Equals("Y", StringComparison.OrdinalIgnoreCase)))
                    {
                        try
                        {
                            var repo = new CsvLampRepository();
                            var all = repo.GetAll();
                            if (all.Any(l => l.Idproperty == lamp.Idproperty))
                            {
                                repo.Update(lamp);
                                Console.WriteLine("Lamp aggiornata nel file CSV.");
                            }
                            else
                            {
                                repo.Add(lamp);
                                Console.WriteLine("Lamp salvata nel file CSV.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Errore durante il salvataggio: {ex.Message}");
                        }
                    }

                    break;
                }

                switch (input.Trim())
                {
                    case "1":
                        lamp.TurnOn();
                        Console.WriteLine("Lamp turned on.");
                        break;
                    case "2":
                        lamp.TurnOff();
                        Console.WriteLine("Lamp turned off.");
                        break;
                    case "3":
                        Console.Write("Enter brightness (0-100): ");
                        if (int.TryParse(Console.ReadLine(), out var b))
                        {
                            lamp.ChangeBrightness(new Brightness(b));
                            Console.WriteLine($"Brightness set to {lamp.BrightnessProperty.Value}.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid brightness.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Available colors: " + string.Join(", ", Enum.GetNames(typeof(ColorType))));
                        Console.Write("Enter color: ");
                        var col = Console.ReadLine();
                        if (Enum.TryParse<ColorType>(col, true, out var ct))
                        {
                            lamp.ChangeColor(ct);
                            Console.WriteLine($"Color set to {lamp.ColorProperty}.");
                        }
                        else Console.WriteLine("Invalid color.");
                        break;
                    case "5":
                        Console.WriteLine("Available lamp types: " + string.Join(", ", Enum.GetNames(typeof(LampType))));
                        Console.Write("Enter lamp type: ");
                        var lt = Console.ReadLine();
                        if (Enum.TryParse<LampType>(lt, true, out var ltype))
                        {
                            lamp.ChangeLampType(ltype);
                            Console.WriteLine($"Lamp type set to {lamp.LampTypeProperty}.");
                        }
                        else Console.WriteLine("Invalid lamp type.");
                        break;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
