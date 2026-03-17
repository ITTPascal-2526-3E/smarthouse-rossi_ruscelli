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
    /// <summary>
    /// Repository semplice per la persistenza delle lamp in formato CSV.
    /// Questa classe gestisce la lettura/scrittura di un file "lamps.csv" nella cartella Data
    /// posizionata nella root della soluzione.
    /// Utilizza un formato CSV con intestazione: Id,Name,Color,Brightness,IsOn,LampType
    /// </summary>
    public class CsvLampRepository
    {
        // Percorso del file CSV usato per salvare le lamp
        private readonly string _filePath = "lamps.csv";

        /// <summary>
        /// Costruttore: crea la cartella Data se necessaria e assicura l'esistenza del file CSV.
        /// </summary>
        public CsvLampRepository()
        {
            var solutionRoot = LocalPathHelper.GetSolutionRoot();
            var dataFolder = Path.Combine(solutionRoot, "Data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "lamps.csv");
            if (!File.Exists(_filePath))
            {
                // Crea un file vuoto con intestazione
                Save(new List<Lamp>());
            }
        }

        /// <summary>
        /// Restituisce tutte le lamp presenti nel file CSV.
        /// </summary>
        public List<Lamp> GetAll()
        {
            return Load();
        }

        /// <summary>
        /// Recupera una lamp per Id. Se non trovata genera un'eccezione (comportamento identico a First).
        /// </summary>
        public Lamp GetById(Guid id)
        {
            return Load().First(l => l.Idproperty == id);
        }

        /// <summary>
        /// Salva l'intera lista di lamp nel file CSV sovrascrivendo il file esistente.
        /// Metodo privato usato internamente da Add/Update/Remove.
        /// </summary>
        private void Save(List<Lamp> lamps)
        {
            var lines = new List<string>
            {
                // Intestazione CSV
                "Id,Name,Color,Brightness,IsOn,LampType"
            };

            foreach (var lamp in lamps)
            {
                // Costruisce la riga CSV per ogni lamp
                lines.Add(string.Join(",",
                    lamp.Idproperty,
                    EscapeCsv(lamp.Nameproperty),
                    lamp.ColorProperty.ToString(),
                    lamp.BrightnessProperty.Value.ToString(CultureInfo.InvariantCulture),
                    lamp.IsOnProperty.ToString(),
                    lamp.LampTypeProperty.ToString()));
            }

            // Scrive tutte le righe nel file in UTF8
            File.WriteAllLines(_filePath, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Esegue escaping dei valori testuali per essere validi in CSV.
        /// Aggiunge virgolette e raddoppia le virgolette interne se necessario.
        /// </summary>
        private static string EscapeCsv(string value)
        {
            if (value == null) return string.Empty;
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                return '"' + value.Replace("\"", "\"\"") + '"';
            }
            return value;
        }

        /// <summary>
        /// Carica le lamp dal file CSV. Se il file non esiste ritorna lista vuota.
        /// Gestisce valori malformati con fallback (id nuovo, brightness di default, ecc.).
        /// </summary>
        private List<Lamp> Load()
        {
            if (!File.Exists(_filePath)) return new List<Lamp>();

            var lines = File.ReadAllLines(_filePath);
            var lamps = new List<Lamp>();
            if (lines.Length <= 1) return lamps; // nessuna riga di dati

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = SplitCsvLine(line);
                if (parts.Length < 6) continue;

                // Parse dei campi con fallback sensati
                if (!Guid.TryParse(parts[0], out var id)) id = Guid.NewGuid();
                var name = parts[1];

                if (!Enum.TryParse<ColorType>(parts[2], true, out var color)) color = ColorType.White;
                if (!int.TryParse(parts[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out var brightness)) brightness = 100;
                if (!bool.TryParse(parts[4], out var isOn)) isOn = false;
                if (!Enum.TryParse<LampType>(parts[5], true, out var lampType)) lampType = LampType.LED;

                // Crea un'istanza di Lamp mantenendo l'id originale se valido
                var lamp = new Lamp(isOn, new NameDevice(name), color, new Brightness(brightness), lampType);
                lamp.Idproperty = id; // preserva id originale
                lamps.Add(lamp);
            }
            return lamps;
        }

        /// <summary>
        /// Effettua lo split di una riga CSV rispettando campi racchiusi tra virgolette.
        /// Restituisce un array di campi.
        /// </summary>
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
                        // virgolette doppie all'interno di un campo: interpreto come virgolette escape
                        current.Append('"');
                        i++; // salto il carattere successivo
                    }
                    else
                    {
                        // cambio stato di in/out quotes
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    // separatore campo trovato
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

        /// <summary>
        /// Aggiorna una lamp esistente nel CSV se il suo Id è presente.
        /// </summary>
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

        /// <summary>
        /// Aggiunge una nuova lamp al file CSV.
        /// </summary>
        public void Add(Lamp lamp)
        {
            var lamps = Load();
            lamps.Add(lamp);
            Save(lamps);
        }

        /// <summary>
        /// Rimuove la lamp con lo stesso Id dal CSV.
        /// </summary>
        public void Remove(Lamp lamp)
        {
            var lamps = Load();
            lamps.RemoveAll(l => l.Idproperty == lamp.Idproperty);
            Save(lamps);
        }
    }
}
