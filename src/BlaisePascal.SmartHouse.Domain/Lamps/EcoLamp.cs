using System;
using System.Drawing;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class EcoLamp
    {
        private Guid Id; // Unique identifier for the lamp
        private float powerConsumption; // Current power consumption in watts
        private bool IsOn; // State of the lamp
        private int Brightness; // Brightness of ther lamp
        private ColorType Color; // Color of the lamp light
        private string Name; // Name of the lamp
        private int MaxConsumption; // Maximum power consumption in watts
        private LampType lampType; // Type of the lamp
        private TimeSpan DefaultAutoOff; // Default time to auto turn off
        private bool EcoEnabled; // Eco mode enabled or not
        private TimeOnly EcoStart;  //Eco mode start time (hour of the day).
        private TimeOnly EcoEnd; // End of eco mode time (può oltrepassare mezzanotte).
        private int EcoMaxBrightness; // max brightness in eco mode.
        private TimeSpan EcoAutoOff;  // auto turn off light when in eco mode
        private DateTime? TurnedOnAt;      // time when the lamp was turned on
        public DateTime? ScheduledOffAt { get;  set; }  // calcolated time when the lamp is going to get turned off automatically

       public int EcoMaxBrightnessProperty { get; set; }

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
                int brighntessValue = Math.Clamp(value, 0, 100);  //
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
            Id = Guid.NewGuid();
            IsOn = isOn;
            Name = name;
            Color = color;
            Brightness = brightness;
            this.lampType = lampType;

            //MaxConsumption is automatically set by the enum
            MaxConsumption = GetMaxConsumption(lampType);

            if (IsOn==true)
            {
                TurnedOnAt = DateTime.Now; // Set turn-on time to now
                
                if (EcoEnabled)
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
            TurnedOnAt = DateTime.Now;                                                 // registers the moment when the lamp is turned on
                                                 // checks if it gets turned on while in eco mode
            if (EcoEnabled)                                                // if it is i enable eco mode
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
                Console.WriteLine("Brightness must be between 0 and 100.");
            }

        }

        // Change the color of the lamp
        public void ChangeColor(ColorType newColor)
        {
            Color = newColor;
        }

        public void ChangeTimers(TimeSpan defaultAutoOff, TimeSpan ecoAutoOff)
        {
            if (defaultAutoOff < TimeSpan.Zero) Console.WriteLine("DefaultAutoOff must be >= 0.");
            if (ecoAutoOff < TimeSpan.Zero) Console.WriteLine("EcoAutoOff must be >= 0.");
            DefaultAutoOff = defaultAutoOff;
            EcoAutoOff = ecoAutoOff;
            if (IsOn && TurnedOnAt.HasValue) ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt.Value);
        }

        /// <summary>
        /// change the time when the eco mode is going to be on
        /// </summary>
        /// <param name="enebled"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="maxBrightness"></param>
        public void ChangeEcoMode(bool enebled, TimeOnly start, TimeOnly end, int maxBrightness) 
        {
            if(maxBrightness < 0 || maxBrightness > 100) Console.WriteLine("EcoMaxBrightness must be between 0 and 100.");
            EcoEnabled = enebled;
            EcoStart = start;
            EcoEnd = end;
            EcoMaxBrightness = maxBrightness;

            if (IsOn && EcoEnabled && IsInEco(DateTime.Now))
                Brightness = Math.Min(Brightness, EcoMaxBrightness);

            if (IsOn && TurnedOnAt.HasValue) ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt.Value);
        }
        /// <summary>
        /// Checks if the given time is in the eco mode
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool IsInEco(DateTime dateTime) 
        {
            TimeOnly time = TimeOnly.FromDateTime(dateTime);
            if (EcoStart <= EcoEnd)            
                return time >= EcoStart && time < EcoEnd; 
            return time >= EcoStart || time < EcoEnd;
        }
        /// <summary>
        /// returns when the next eco mode will be
        /// </summary>
        /// <param name="from"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private static DateTime NextOccurrence(DateTime from, TimeOnly time) 
        {
            DateTime candidate = new DateTime(from.Year, from.Month, from.Day, time.Hour, time.Minute, time.Second, time.Millisecond, from.Kind);
            if (candidate <= from) candidate = candidate.AddDays(1);
            return candidate;
        }
        /// <summary>
        /// calculates if the lamp should be turned off based on the eco mode or the default mode,
        /// ex: if the lamp is turned on at 6 and it should be on for 3 hours but the eco mode starts at 7 and it can be on for 1 hour the lamp turns off at 8
        /// </summary>
        /// <param name="TurnOnAt"></param>
        /// <returns></returns>
        private DateTime ComputeFinalOffInstant(DateTime TurnOnAt) //Calculates when the lamp should turn off based on Eco settings
        {
            DateTime offByDefault = TurnOnAt + DefaultAutoOff;

            if (EcoEnabled==false)
                return offByDefault;

            if (IsInEco(TurnOnAt))
            {
                // uses timer eco when turned on in eco mode
                return TurnOnAt + EcoAutoOff;
            }
            else
            {
                // checks if it ends before the normal timer or the timer if it enters in eco mode
                // termine = min(offPredef, nextEcoStart + ecoAutoOff)
                DateTime nextEcoStart = NextOccurrence(TurnOnAt, EcoStart);
                DateTime ecoBound = nextEcoStart + EcoAutoOff;
                if (ecoBound < offByDefault)
                    return ecoBound;
                else
                    return offByDefault;

            }
        }
    }
}