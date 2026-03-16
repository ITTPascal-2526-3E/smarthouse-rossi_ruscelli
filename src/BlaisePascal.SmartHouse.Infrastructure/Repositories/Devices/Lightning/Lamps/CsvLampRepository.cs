using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Lightning;
using BlaisePascal.SmartHouse.Domain.Lightning.Repositories;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps
{
    public class CsvLampRepository
    {
        private readonly string _filePath = "lamps.csv";

        public CsvLampRepository()
        {
            var solutionRoot = LocalPathHelper.GetSolutionRoot();
            var dataFolder = Path.Combine(solutionRoot, "Data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "lamps.csv");
            if (!File.Exists(_filePath))
            {
                Save(new List<Lamp>());
            }
        }

        public List<Lamp> GetAll()
        {
            return Load();
        }

        public Lamp GetById(Guid id)
        {
            return Load().First(l => l.Idproperty == id);
        }

        private void Save(List<Lamp> lamps)
        {
            var lines = new List<string>
            {
                "Id,Name,Color,Brightness,IsOn,LampType"
            };

            foreach (var lamp in lamps)
            {
                lines.Add(string.Join(",",
                    lamp.Idproperty,
                    EscapeCsv(lamp.Nameproperty),
                    lamp.ColorProperty.ToString(),
                    lamp.BrightnessProperty.Value.ToString(CultureInfo.InvariantCulture),
                    lamp.IsOnProperty.ToString(),
                    lamp.LampTypeProperty.ToString()));
            }

            File.WriteAllLines(_filePath, lines, Encoding.UTF8);
        }

        private static string EscapeCsv(string value)
        {
            if (value == null) return string.Empty;
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                return '"' + value.Replace("\"", "\"\"") + '"';
            }
            return value;
        }

        private List<Lamp> Load()
        {
            if (!File.Exists(_filePath)) return new List<Lamp>();

            var lines = File.ReadAllLines(_filePath);
            var lamps = new List<Lamp>();
            if (lines.Length <= 1) return lamps; // no data rows

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = SplitCsvLine(line);
                if (parts.Length < 6) continue;

                // Parse fields with fallbacks
                if (!Guid.TryParse(parts[0], out var id)) id = Guid.NewGuid();
                var name = parts[1];

                if (!Enum.TryParse<ColorType>(parts[2], true, out var color)) color = ColorType.White;
                if (!int.TryParse(parts[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out var brightness)) brightness = 100;
                if (!bool.TryParse(parts[4], out var isOn)) isOn = false;
                if (!Enum.TryParse<LampType>(parts[5], true, out var lampType)) lampType = LampType.LED;

                var lamp = new Lamp(isOn, new NameDevice(name), color, new Brightness(brightness), lampType);
                // preserve original id
                lamp.Idproperty = id;
                lamps.Add(lamp);
            }
            return lamps;
        }

        private static string[] SplitCsvLine(string line)
        {
            var parts = new List<string>();
            var current = new StringBuilder();
            var inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        // escaped quote
                        current.Append('"');
                        i++; // skip next
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    parts.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            parts.Add(current.ToString());
            return parts.ToArray();
        }

        public void Update(Lamp lamp)
        {
            var lamps = Load();
            var index = lamps.FindIndex(l => l.Idproperty == lamp.Idproperty);
            if (index >= 0)
            {
                lamps[index] = lamp;
                Save(lamps);
            }
        }

        public void Add(Lamp lamp)
        {
            var lamps = Load();
            lamps.Add(lamp);
            Save(lamps);
        }


        public void Remove(Lamp lamp)
        {
            var lamps = Load();
            lamps.RemoveAll(l => l.Idproperty == lamp.Idproperty);
            Save(lamps);
        }
    }
}
