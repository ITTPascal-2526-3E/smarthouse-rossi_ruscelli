using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.AirFryer
{
    // Make class public so it is accessible from other projects (console)
    public class AirFryer
    {

        private int Temp;
        private int MaxTemp;
        private int MaxConsumption;
        private int MinConsumption;
        private int CurrentConsumption;
        private bool IsOn;
        private DateTime TurnedOnAt;
        private DateTime TurnedOffAt;
        private DateTime AutoTurnOffAt;
        private float CostPerKWh;
        /// <summary>
        /// dictionary that contains the max and min consumption for each mode (min consumption is when the airfryer is maintaining the temperature, max when heating)
        /// </summary>
        private static readonly Dictionary<Mode, (int maxConsumption, int minConsumption)> ModeProperties = new()
        {
            { Mode.frying, (1500, 800) },
            { Mode.baking, (1500, 700) },
            { Mode.grill, (1500, 900) },
            { Mode.reheat, (1500, 600) },
            { Mode.dehydrate, (1500, 250) },
            { Mode.toast, (1500, 900) }
        };
        public static int GetMaxConsumption(Mode mode)
        {
            return ModeProperties[mode].maxConsumption;
        }
        public static float GetMinConspumption(Mode mode)
        {
            return ModeProperties[mode].minConsumption;
        }
        public int MinConsumptionProperty
        {
            get { return MinConsumption; }
            set { MinConsumption = value; }
        }
        public int TempProperty { get; set; }
        public int MaxTempProperty { get; set; }
        public int MaxConsumptionProperty { get; set; }
        public float CostPerKWhProperty { get; set; }
        public DateTime TurnedOnAtProperty { get; set; }
        public DateTime TurnedOffAtProperty { get; set; }
        public DateTime AutoTurnOffAtProperty { get; set; }
        public bool IsOnProperty
        {
            set { IsOn = value; }
            get { return IsOn; }

        }
        public int CurrentConsumptionProperty
        {
            set { CurrentConsumption = value; }
        }



        /// <summary>
        /// Constructor, if the airfryer is on it sets the turnedOnAt property to the current time
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="maxTemp"></param>
        /// <param name="maxConsumption"></param>
        /// <param name="isOn"></param>
        public void AirFry(int temp, int maxTemp, bool isOn, float costPerKWh)
        {

            Temp = temp;
            MaxTemp = maxTemp;
            bool IsOn = isOn;
            CostPerKWh = costPerKWh;
            if (IsOn)
            {
                TurnedOnAt = DateTime.Now;
            }
        }



        /// <summary>
        /// returns the current consumption based on the state of the airfryer (if it is heating or maintaining temperature) if off returns 0
        /// </summary>
        /// <returns></returns>
        public int GetCurrentConsumption()
        {
            if (IsOn && Temp == MaxTemp)
            {
                return MinConsumption;
            }
            else if (IsOn && Temp < MaxTemp)
            {
                return MaxConsumption;
            }
            return 0;
        }



        /// <summary>
        /// turns on the airfryer, also saving the time it was turned on
        /// </summary>
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                TurnedOnAt = DateTime.Now;
                Temp = MaxTemp;
            }
        }
        /// <summary>
        /// turns off the airfryer, also saving the time it was turned off
        /// </summary>
        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                TurnedOffAt = DateTime.Now;
                Temp = 0;
            }

        }




        /// <summary>
        /// sets the mode of the airfryer, changing the max and min consumption :)
        /// </summary>
        /// <param name="mode"></param>
        public void SetMode(Mode mode)
        {
            if (IsOn)
            {
                (int maxConsumption, int minConsumption) = ModeProperties[mode];
                // assign to instance fields so subsequent calculations use them
                MaxConsumption = maxConsumption;
                MinConsumption = minConsumption;
            }
            else
            {
                // cannot set mode if airfryer is off
            }
        }





        /// <summary>
        /// returns the time the airfryer has been on
        /// </summary>
        /// <returns></returns>
        public TimeSpan TimeOn()
        {
            if (IsOn)
            {
                TimeSpan timeOn = DateTime.Now - TurnedOnAt;
                return timeOn;
            }
            else
            {
                TimeSpan timeOn = TurnedOffAt - TurnedOnAt;
                return timeOn;
            }
        }
        //todo: aggiungere anche una funzione timer per spegnimento automatico
        public void AutoTurnOff(TimeSpan time)
        {
            if (IsOn)
            {

            }
            else
            {
                //errore: non puoi settare un time se è spento
            }
        }

        /// <summary>
        /// Calcola il consumo totale in watt-ora usando sempre il valore di minConsumption per la modalità fornita.
        /// Converte il TimeSpan restituito da TimeOn() in ore tramite TotalHours e moltiplica per i watt (minConsumption).
        /// Restituisce un valore double che rappresenta i watt-ora totali.
        /// </summary>
        public double ConsumptionWattHours(Mode mode)
        {
            // get the min consumption based on the mode
            int min = ModeProperties[mode].minConsumption;

            TimeSpan timeOn = TimeOn();
            double hours = timeOn.TotalHours;
            double wattHours = hours * min;
            return wattHours;
        }

        /// <summary>
        /// calculates the consumption in kilo watt hours
        /// </summary>
        public double ConsumptionKiloWattHours(Mode mode)
        {
            return ConsumptionWattHours(mode) / 1000.0;
        }

        public double CostOfConsumption(Mode mode)
        {
            double kWh = ConsumptionKiloWattHours(mode);
            return kWh * CostPerKWh;
        }


    }
}
