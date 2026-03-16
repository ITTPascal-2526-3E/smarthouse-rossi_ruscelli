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
    // Repository che persiste le lampade su file CSV
    public class CsvLampRepository : ILampRepository
    {
        // Percorso del file CSV usato per persistere le lampade
        private string _filePath;
        // Lock per operazioni thread-safe sul file
        private  object _lock = new();
        // Header del CSV
        private const string Header = "Id,Name,IsOn,Color,Brightness,LampType";

        // Costruttore: se non viene passato un percorso, usa la directory base 
        public CsvLampRepository(string? filePath)
        {
            _filePath = string.IsNullOrWhiteSpace(filePath)
                ? Path.Combine(AppContext.BaseDirectory, "lamps.csv")
                : filePath!;

            // Assicura che il file esista e contenga l'header
            lock (_lock)
            {
                if (!File.Exists(_filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? string.Empty);
                    File.WriteAllText(_filePath, Header + Environment.NewLine);
                }
                else
                {
                    // Se il file esiste ma è vuoto, scrive l'header
                    var info = new FileInfo(_filePath);
                    if (info.Length == 0) File.WriteAllText(_filePath, Header + Environment.NewLine);
                }
            }
        }

        // Aggiunge una lampada al CSV (in coda)
        public void Add(Lamp lamp)
        {
            if (lamp == null) throw new ArgumentNullException(nameof(lamp));

            lock (_lock)
            {
                var lines = File.ReadAllLines(_filePath).ToList();

                // Aggiunge una nuova riga CSV rappresentante la lampada
                var row = ToCsv(lamp);
                lines.Add(row);
                File.WriteAllLines(_filePath, lines);
            }
        }

        // Rimuove tutte le righe relative alla lamp specificata (confronto per Id)
        public void Remove(Lamp lamp)
        {
            if (lamp == null) throw new ArgumentNullException(nameof(lamp));

            lock (_lock)
            {
                var lamps = ReadAll();
                var remaining = lamps.Where(l => l.Idproperty != lamp.Idproperty).Select(ToCsv).ToList();
                // Scrive header + righe rimanenti
                var output = new List<string> { Header };
                output.AddRange(remaining);
                File.WriteAllLines(_filePath, output);
            }
        }

        // Restituisce la lamp con l'Id specificato oppure null
        public Lamp GetById(Guid id)
        {
            var lamps = ReadAll();
            return lamps.FirstOrDefault(l => l.Idproperty == id)!;
        }

        // Restituisce tutte le lamp dal CSV
        public List<Lamp> GetAll()
        {
            return ReadAll();
        }

        // Legge tutto il file e costruisce le istanze di Lamp
        private List<Lamp> ReadAll()
        {
            lock (_lock)
            {
                var result = new List<Lamp>();
                if (!File.Exists(_filePath)) return result;

                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines.Skip(1)) // salta l'header
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = SplitCsv(line);
                    if (parts.Length < 6) continue;

                    // Formato: Id,Name,IsOn,Color,Brightness,LampType
                    if (!Guid.TryParse(parts[0], out var id)) continue;
                    var name = parts[1];
                    if (!bool.TryParse(parts[2], out var isOn)) isOn = false;
                    if (!Enum.TryParse<ColorType>(parts[3], true, out var color)) color = ColorType.Daylight;
                    if (!int.TryParse(parts[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out var brightness)) brightness = 0;
                    if (!Enum.TryParse<LampType>(parts[5], true, out var lampType)) lampType = LampType.LED;

                    var lamp = new Lamp(isOn, new NameDevice(name), color, new Brightness(brightness), lampType);
                    // Ripristina Id e nome pubblici per compatibilità con il resto dell'app
                    lamp.Idproperty = id;
                    lamp.NameProperty = name;
                    try { lamp.GetType().GetProperty("Nameproperty")?.SetValue(lamp, name); } catch { }

                    result.Add(lamp);
                }

                return result;
            }
        }

        // Converte una Lamp in una riga CSV
        private static string ToCsv(Lamp lamp)
        {
            // Id,Name,IsOn,Color,Brightness,LampType
            var id = lamp.Idproperty;
            var name = Escape(lamp.NameProperty ?? lamp.Nameproperty ?? string.Empty);
            var isOn = lamp.IsOnProperty;
            var color = lamp.ColorProperty.ToString();
            var brightness = lamp.BrightnessProperty?.Value ?? 0;
            var lampType = lamp.LampTypeProperty.ToString();

            return string.Join(',', id.ToString(), name, isOn.ToString(), color, brightness.ToString(CultureInfo.InvariantCulture), lampType);
        }

        // Escape minimale per campi CSV (gestisce virgole e doppi apici)
        private static string Escape(string s)
        {
            if (s.Contains(',') || s.Contains('"') || s.Contains('\n') || s.Contains('\r'))
            {
                var escaped = s.Replace("\"", "\"\"");
                return '"' + escaped + '"';
            }
            return s;
        }

        // Split CSV che rispetta i campi tra virgolette
        private static string[] SplitCsv(string line)
        {
            var parts = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        // doppia virgoletta -> carattere quote
                        sb.Append('"');
                        i++; // salta il successivo
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                    continue;
                }

                if (c == ',' && !inQuotes)
                {
                    parts.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }
            parts.Add(sb.ToString());
            return parts.ToArray();
        }
    }
}
