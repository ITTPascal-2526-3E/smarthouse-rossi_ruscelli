using BlaisePascal.SmartHouse.Domain;

class Program
{
    static void Main(string[] args)
    {
        //Example usage of Lamp class with parameters defined by the user
        Console.WriteLine("Insert the lamp name:");
        string lampName = Console.ReadLine();
        Console.WriteLine("Insert the lamp color (e.g., Red, Green, Blue):");
        string lampColorInput = Console.ReadLine();
        ColorType lampColor = Enum.TryParse(lampColorInput, true, out ColorType parsedColor) ? parsedColor : ColorType.White;
    }
}