using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using BlaisePascal.SmartHouse.Domain.Abstractions;
namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class EcoLamp : AbstractLamp
    {

        private TimeSpan DefaultAutoOff; // Default time to auto turn off
        private bool EcoEnabled; // Eco mode enabled or not
        private TimeOnly EcoStart;  //Eco mode start time (hour of the day).
        private TimeOnly EcoEnd; // End of eco mode time (può oltrepassare mezzanotte).
        private int EcoMaxBrightness; // max brightness in eco mode.
        private TimeSpan EcoAutoOff;  // auto turn off light when in eco mode
        public DateTime? ScheduledOffAt { get; set; }  // calcolated time when the lamp is going to get turned off automatically

        public int EcoMaxBrightnessProperty { get; private set; }
        private bool hasValue(DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return true;
            return false;
        }

        /// <summary>
        /// Constructor for EcoLamp class
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        public EcoLamp(bool isOn, string name, ColorType color, int brightness, LampType lampType) : base(isOn, name, color, brightness, lampType)
        {
            Id = Guid.NewGuid();
            IsOn = isOn;
            Name = name;
            Color = color;
            Brightness = brightness;
            this.lampType = lampType;

            //MaxConsumption is automatically set by the enum
            MaxConsumption = GetMaxConsumption(lampType);

            if (IsOn == true)
            {
                TurnedOnAt = DateTime.Now; // Set turn-on time to now


                if (EcoEnabled)
                    Brightness = Math.Min(Brightness, EcoMaxBrightness); // Apply brightness cap if in Eco
                ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt);

            }
        }

        // Turn on the lamp
        public void TurnOnEco()
        {
            if (IsOn == false)
                IsOn = true;
            TurnedOnAt = DateTime.Now;                                                 // registers the moment when the lamp is turned on
                                                                                       // checks if it gets turned on while in eco mode
            if (EcoEnabled)                                                // if it is i enable eco mode
                Brightness = Math.Min(Brightness, EcoMaxBrightness);
            ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt);
        }



        public void ChangeTimers(TimeSpan defaultAutoOff, TimeSpan ecoAutoOff)
        {

            DefaultAutoOff = defaultAutoOff;
            EcoAutoOff = ecoAutoOff;
            if (IsOn && hasValue(TurnedOnAt)) ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt);
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

            EcoEnabled = enebled;
            EcoStart = start;
            EcoEnd = end;
            EcoMaxBrightness = maxBrightness;

            if (IsOn && EcoEnabled && IsInEco(DateTime.Now))
                Brightness = Math.Min(Brightness, EcoMaxBrightness);

            if (IsOn && hasValue(TurnedOnAt)) ScheduledOffAt = ComputeFinalOffInstant(TurnedOnAt);
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
        private DateTime? ComputeFinalOffInstant(DateTime TurnOnAt) //Calculates when the lamp should turn off based on Eco settings
        {
            DateTime? offByDefault = TurnOnAt + DefaultAutoOff;

            if (EcoEnabled == false)
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
                DateTime? nextEcoStart = NextOccurrence(TurnOnAt, EcoStart);
                DateTime? ecoBound = nextEcoStart + EcoAutoOff;
                if (ecoBound < offByDefault)
                    return ecoBound;
                else
                    return offByDefault;

            }
        }
    }
}