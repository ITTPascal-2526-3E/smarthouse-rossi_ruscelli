using System;
using System.Drawing;

namespace BlaisePascal.SmartHouse.Domain
{
    public class EcoLamp
    {
        private float powerConsumption; // Current power consumption in watts
        private bool IsOn; // State of the lamp
        private int Brightness; // Brightness of ther lamp
        private ColorType Color; // Color of the lamp light
        private string Name; // Name of the lamp
        private int MaxConsumption; // Maximum power consumption in watts
        private LampType lampType; // Type of the lamp
        private TimeSpan DefaultAutoOff; // Default time to auto turn off
        private bool EcoEnabled; // Eco mode enabled or not
        private TimeOnly EcoStart;  //Inizio fascia Eco (ora del giorno).
        private TimeOnly EcoEnd; // NEW: Fine fascia Eco (può oltrepassare mezzanotte).
        private int EcoMaxBrightness; // NEW: Cap luminosità in Eco (0..100).
        private TimeSpan EcoAutoOff;  // NEW: Timer di autospegnimento quando accesa in Eco.
        private DateTime? TurnedOnAt;      // Istante di accensione
        private DateTime? ScheduledOffAt;  // Istante di spegnimento calcolato
        private bool WasInEco;             // Per rilevare ingresso in Eco e applicare cap una volta

        public enum ColorType
        {
            WarmWhite,
            CoolWhite,
            Daylight,
            Red,
            Green,
            Blue,
            Yellow,
            Purple,
            Orange,
            Pink
        }
        public enum LampType
        {
            LED,
            CFL,                    // Fluorescente compatta
            Halogen,               // Alogena
            Incandescent,          // Incandescente
            FluorescentLinear,     // Neon lineare
            HighPressureSodium,    // Al sodio alta pressione
            Induction,             // A induzione
            VintageLED            // LED filamento vintage
        }
        private static readonly Dictionary<LampType, (int maxConsumption, float alpha)> lampTypeProperties = new()
        {
            { LampType.LED, (25, 0.2f) },
            { LampType.CFL, (40, 0.4f) },
            { LampType.Halogen, (150, 0.8f) },
            { LampType.Incandescent, (300, 1.0f) },
            { LampType.FluorescentLinear, (80, 0.35f) },
            { LampType.HighPressureSodium, (400, 0.25f) },
            { LampType.Induction, (200, 0.30f) },
            { LampType.VintageLED, (10, 0.25f) }
        };
        public static int GetMaxConsumption(LampType lampType) //Returns the max consumption of the lamp type
        {
            return lampTypeProperties[lampType].maxConsumption;
        }
        public static float GetAlpha(LampType lampType) //Returns the efficiency factor of the lamp type
        {
            return lampTypeProperties[lampType].alpha;
        }
        /// <summary>
        /// Propertys for EcoLamp class
        /// </summary>
        public LampType LampTypeProperty
        {
            get { return lampType; }
            set { lampType = value; }
        }
        public bool IsOnProperty
        {
            get { return IsOn; }
            set
            {
                if (value == true)
                {
                    IsOn = true;
                    TurnOn(); // Call TurnOn method when setting IsOn to true
                }
                else
                {
                    IsOn = false;
                    TurnOff();
                }
            }
        }
        public int BrightnessProperty
        {
            get { return Brightness; }
            set
            {
                int brighntessValue = Math.Clamp(value, 0, 100);
                if (EcoEnabled && IsInEco(DateTime.Now))
                    Brightness = Math.Min(brighntessValue, EcoMaxBrightness); // If in Eco mode, limit brightness
                else
                    Brightness = brighntessValue; // Normal brightness setting
            }
        }
        public ColorType ColorProperty
        {
            get { return Color; }
            set { Color = value; }
        }
        public string NameProperty
        {
            get { return Name; }
            set { Name = value; }
        }
        public float PowerConsumption
        {
            get
            {
                float alpha = lampTypeProperties[lampType].alpha;
                if (IsOn==false) return 0;
                return MaxConsumption * (Brightness / 100.0f) * alpha;
            }
        }
        /// <summary>
        /// Constructor for EcoLamp class
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        public EcoLamp(bool isOn, string name, ColorType color, int brightness, LampType lampType)
        {
            IsOn = isOn;
            Name = name;
            Color = color;
            Brightness = brightness;
            this.lampType = lampType;

            // MaxConsumption viene automaticamente impostato dall'enum
            MaxConsumption = GetMaxConsumption(lampType);

            if (IsOn==true)
            {
                TurnedOnAt = DateTime.Now; // Set turn-on time to now
                WasInEco = IsInEco(TurnedOnAt.Value); // Check if in Eco at turn-on
                if (EcoEnabled && WasInEco)
                    Brightness = Math.Min(Brightness, EcoMaxBrightness); // Apply brightness cap if in Eco
                ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt.Value);
                Console.WriteLine($"Lamp turned on at {TurnedOnAt.Value}, scheduled to turn off at {ScheduledOffAt.Value}.");
            }
        }

        // Turn on the lamp
        public void TurnOn()
        {
            if (IsOn==false)
            IsOn = true;
            TurnedOnAt = DateTime.Now;                                                 // Registra l’istante di accensione [web:105]
            WasInEco = IsInEco(TurnedOnAt.Value);                                      // Controlla se l’accensione avviene in fascia Eco [web:12]
            if (EcoEnabled && WasInEco)                                                // Se Eco attivo e si è in Eco [web:12]
                Brightness = Math.Min(Brightness, EcoMaxBrightness);
            ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt.Value);
        }

        // Turn off the lamp
        public void TurnOff()
        {
            if (IsOn==true)
            IsOn = false;
            Console.WriteLine($"Lamp turned off at {DateTime.Now}.");
            TurnedOnAt = null; // Reset turn-on time
            ScheduledOffAt = null; // Reset scheduled off time
            WasInEco = false;
        }

        // Change the brightness of the lamp
        public void ChangeBrightness(int newBrightness)
        {
            if (newBrightness >= 0 && newBrightness <= 100)
            {
                Brightness = newBrightness;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Brightness must be between 0 and 100.");
            }

        }

        // Change the color of the lamp
        public void ChangeColor(ColorType newColor)
        {
            Color = newColor;
        }

        public void ChangeTimer(TimeSpan defaultAutoOff, TimeSpan ecoAutoOff)
        {
            if (defaultAutoOff < TimeSpan.Zero) throw new ArgumentOutOfRangeException("DefaultAutoOff must be >= 0.");
            if (ecoAutoOff < TimeSpan.Zero) throw new ArgumentOutOfRangeException("EcoAutoOff must be >= 0.");
            DefaultAutoOff = defaultAutoOff;
            EcoAutoOff = ecoAutoOff;
            if (IsOn && TurnedOnAt.HasValue) ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt.Value);
        }
        public void ChangeEcoMode(bool enebled, TimeOnly start, TimeOnly end, int maxBrightness)
        {
            if(maxBrightness < 0 || maxBrightness > 100) throw new ArgumentOutOfRangeException("EcoMaxBrightness must be between 0 and 100.");
            EcoEnabled = enebled;
            EcoStart = start;
            EcoEnd = end;
            EcoMaxBrightness = maxBrightness;

            if (IsOn && EcoEnabled && IsInEco(DateTime.Now))
                Brightness = Math.Min(Brightness, EcoMaxBrightness);

            if (IsOn && TurnedOnAt.HasValue) ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt.Value);
        }

        public void Tick(DateTime now)
        {
            if (!IsOn) return;

            bool inEco = IsInEco(now);
            // Applica cap nel momento in cui si entra in Eco
            if (EcoEnabled && inEco && !WasInEco)
                Brightness = Math.Min(Brightness, EcoMaxBrightness);

            WasInEco = inEco;

            // Spegni alla scadenza programmata
            if (ScheduledOffAt.HasValue && now >= ScheduledOffAt.Value)
            {
                TurnOff();
            }
        }

        private bool IsInEco(DateTime dateTime)
        {
            TimeOnly time = TimeOnly.FromDateTime(dateTime);
            if (EcoStart <= EcoEnd)            
                return time >= EcoStart && time < EcoEnd; 
            return time >= EcoStart || time < EcoEnd;
        }

        private static DateTime NextOccurrence(DateTime from, TimeOnly time) //
        {
            DateTime candidate = new DateTime(from.Year, from.Month, from.Day, time.Hour, time.Minute, time.Second, time.Millisecond, from.Kind);
            if (candidate <= from) candidate = candidate.AddDays(1);
            return candidate;
        }

        private DateTime ComputeFinalOffInstant(DateTime onTime) //Calculates when the lamp should turn off based on Eco settings
        {
            DateTime offByDefault = onTime + DefaultAutoOff;

            if (!EcoEnabled)
                return offByDefault;

            if (IsInEco(onTime))
            {
                // Accesa in Eco: usa timer Eco e non estendere quando la fascia termina
                return onTime + EcoAutoOff;
            }
            else
            {
                // Accesa fuori Eco: se la fascia inizia prima dell'off predefinito,
                // termine = min(offPredef, nextEcoStart + ecoAutoOff)
                DateTime nextEcoStart = NextOccurrence(onTime, EcoStart);
                DateTime ecoBound = nextEcoStart + EcoAutoOff;
                return (ecoBound < offByDefault) ? ecoBound : offByDefault;
            }
        }
    }
}