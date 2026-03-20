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
            var lamp = new Lamp(true, new NameDevice("Living Lamp"), ColorType.Daylight, new Brightness(10), LampType.LED);

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
                Console.WriteLine("6) Load Lamp from CSV");
                Console.WriteLine("7) Delete Lamp from CSV");
                Console.WriteLine("8) Create new Lamp and save to CSV");
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
                    case "6":
                        try
                        {
                            var repo = new CsvLampRepository();
                            var all = repo.GetAll();
                            if (!all.Any())
                            {
                                Console.WriteLine("No lamps found in CSV.");
                                break;
                            }

                            Console.WriteLine("Select a lamp to load:");
                            for (int i = 0; i < all.Count; i++)
                            {
                                var l = all[i];
                                Console.WriteLine($"{i + 1}) {l.NameProperty} - {l.Idproperty}");
                            }

                            Console.Write("Enter number (or blank to cancel): ");
                            var sel = Console.ReadLine();
                            if (int.TryParse(sel, out var idx) && idx >= 1 && idx <= all.Count)
                            {
                                lamp = all[idx - 1];
                                Console.WriteLine("Lamp loaded from CSV.");
                            }
                            else
                            {
                                Console.WriteLine("Cancelled or invalid selection.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Errore: {ex.Message}");
                        }
                        break;
                    case "7":
                        try
                        {
                            var repo = new CsvLampRepository();
                            var all = repo.GetAll();
                            if (!all.Any())
                            {
                                Console.WriteLine("No lamps found in CSV to delete.");
                                break;
                            }

                            Console.WriteLine("Select a lamp to delete:");
                            for (int i = 0; i < all.Count; i++)
                            {
                                var l = all[i];
                                Console.WriteLine($"{i + 1}) {l.NameProperty} - {l.Idproperty}");
                            }

                            Console.Write("Enter number (or blank to cancel): ");
                            var selDel = Console.ReadLine();
                            if (int.TryParse(selDel, out var delIdx) && delIdx >= 1 && delIdx <= all.Count)
                            {
                                var toDelete = all[delIdx - 1];
                                Console.Write($"Sei sicuro di voler cancellare '{toDelete.NameProperty}'? (S/N): ");
                                var confirm = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(confirm) && (confirm.Equals("S", StringComparison.OrdinalIgnoreCase) || confirm.Equals("Y", StringComparison.OrdinalIgnoreCase)))
                                {
                                    repo.Remove(toDelete);
                                    Console.WriteLine("Lamp rimossa dal file CSV.");
                                    // If the currently loaded lamp is the deleted one, reset to a default lamp
                                    if (lamp.Idproperty == toDelete.Idproperty)
                                    {
                                        lamp = new Lamp(false, new NameDevice("New Lamp"), ColorType.White, new Brightness(100), LampType.LED);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Cancellazione annullata.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Cancelled or invalid selection.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Errore: {ex.Message}");
                        }
                        break;
                    case "8":
                        try
                        {
                            Console.Write("Enter name: ");
                            var nameInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(nameInput))
                            {
                                Console.WriteLine("Name is required.");
                                break;
                            }

                            Console.WriteLine("Available colors: " + string.Join(", ", Enum.GetNames(typeof(ColorType))));
                            Console.Write("Enter color: ");
                            var colorInput = Console.ReadLine();
                            if (!Enum.TryParse<ColorType>(colorInput, true, out var colorValue))
                            {
                                Console.WriteLine("Invalid color.");
                                break;
                            }

                            Console.Write("Enter brightness (0-100): ");
                            if (!int.TryParse(Console.ReadLine(), out var brightnessVal))
                            {
                                Console.WriteLine("Invalid brightness.");
                                break;
                            }

                            Console.Write("Is On? (S/N): ");
                            var isOnInput = Console.ReadLine();
                            var isOnVal = !string.IsNullOrWhiteSpace(isOnInput) && (isOnInput.Equals("S", StringComparison.OrdinalIgnoreCase) || isOnInput.Equals("Y", StringComparison.OrdinalIgnoreCase));

                            Console.WriteLine("Available lamp types: " + string.Join(", ", Enum.GetNames(typeof(LampType))));
                            Console.Write("Enter lamp type: ");
                            var ltInput = Console.ReadLine();
                            if (!Enum.TryParse<LampType>(ltInput, true, out var ltValue))
                            {
                                Console.WriteLine("Invalid lamp type.");
                                break;
                            }

                            var newLamp = new Lamp(isOnVal, new NameDevice(nameInput), colorValue, new Brightness(brightnessVal), ltValue);
                            var repo2 = new CsvLampRepository();
                            repo2.Add(newLamp);
                            lamp = newLamp; // make it the current lamp
                            Console.WriteLine("Lamp creata e salvata nel CSV.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Errore durante la creazione: {ex.Message}");
                        }
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
