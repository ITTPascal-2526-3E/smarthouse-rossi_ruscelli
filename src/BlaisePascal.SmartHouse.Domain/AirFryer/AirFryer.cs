using BlaisePascal.SmartHouse.Domain.Abstractions;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.AirFryerDevice
{
   
    public sealed class AirFryer : AbstractDevice, IAirFryer, IModeSelectable
    {
       
        private TemperatureDevice Temp;
        private TemperatureDevice MaxTemp;
        private ConsumptionDevice MaxConsumption;
        private ConsumptionDevice MinConsumption;
        private ConsumptionDevice CurrentConsumption;
        private bool IsOn;
        private DateTime TurnedOnAt;
        private DateTime TurnedOffAt;
        private DateTime AutoTurnOffAt;
        private CostPerKWh CostPerKWh;
        private Mode mode;
        /// <summary>
        /// dictionary that contains the max and min consumption for each mode (min consumption is when the airfryer is maintaining the temperature, max when heating)
        /// </summary>
        private static readonly Dictionary<Mode, (ConsumptionDevice maxConsumption, ConsumptionDevice minConsumption)> ModeProperties = new()
        {
            { Mode.frying, (new ConsumptionDevice(1500), new ConsumptionDevice(800)) },
            { Mode.baking, (new ConsumptionDevice(1500), new ConsumptionDevice(700)) },
            { Mode.grill, (new ConsumptionDevice(1500), new ConsumptionDevice(900)) },
            { Mode.reheat, (new ConsumptionDevice(1500), new ConsumptionDevice(600)) },
            { Mode.dehydrate, (new ConsumptionDevice(1500), new ConsumptionDevice(250)) },
            { Mode.toast, (new ConsumptionDevice(1500), new ConsumptionDevice(900)) }
        };


        /// <summary>
        /// properties
        /// </summary>
        public ConsumptionDevice MinConsumptionProperty { get; private set; } // ok
        public TemperatureDevice TempProperty { get; private set; }    //ok
        public TemperatureDevice MaxTempProperty { get; private set; } // ok
        public ConsumptionDevice MaxConsumptionProperty { get; private set; }    // ok
        public CostPerKWh CostPerKWhProperty { get; private set; }    // ok
        public DateTime TurnedOnAtProperty { get; private set; }  //ok
        public DateTime TurnedOffAtProperty { get; private set; }  //ok
        public DateTime AutoTurnOffAtProperty { get; private set; }  //ok
        public bool IsOnProperty { get; private set; }   //ok
        public ConsumptionDevice CurrentConsumptionProperty { get; private set; }   //ok
        public Mode ModeProperty { get; private set; }   //ok





        /// <summary>
        /// Constructor, if the airfryer is on it sets the turnedOnAt property to the current time
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="maxTemp"></param>
        /// <param name="maxConsumption"></param>
        /// <param name="isOn"></param>
        public AirFryer(TemperatureDevice temp, TemperatureDevice maxTemp, bool isOn, CostPerKWh costPerKWh,NameDevice name, Mode Mode) : base(name)
        {
            
            Temp = temp;
            mode = Mode;
            Temp = maxTemp;
            bool IsOn = isOn;
            CostPerKWh = costPerKWh;
            if (IsOn)
            {
                TurnedOnAt = DateTime.Now;
            }
        }

        /// <summary>
        /// methods to get max and min consumption based on mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int GetMaxConsumption()
        {
            return ModeProperties[mode].maxConsumption.Consumption;
        }
        public float GetMinConspumption()
        {
            return ModeProperties[mode].minConsumption.Consumption;
        }

        public void SetTemp(TemperatureDevice temp)
        {
            if (IsOn)
            {
                MaxTemp= temp;
            }
            else
            {
                // cannot set temp if airfryer is off
            }
        }

        public void ChangeCost(CostPerKWh cost)
        {
            CostPerKWh = cost;
        }

        /// <summary>
        /// returns the current consumption based on the state of the airfryer (if it is heating or maintaining temperature) if off returns 0
        /// </summary>
        /// <returns></returns>
        public int GetConsumption()
        {
            if (IsOn && Temp == MaxTemp)
            {
                return MinConsumption.Consumption;
            }
            else if (IsOn && Temp.Value < MaxTemp.Value)
            {
                return MaxConsumption.Consumption;
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
                TemperatureDevice NewTemp = new TemperatureDevice(0);
                SetTemp(NewTemp);
            }

        }




        /// <summary>
        /// sets the mode of the airfryer, changing the max and min consumption :)
        /// </summary>
        /// <param name="mode"></param>
        public void SelectMode(Mode mode)
        {
            if (IsOn)
            {
                (ConsumptionDevice maxConsumption, ConsumptionDevice minConsumption) = ModeProperties[mode];
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
      
        public void SetTimer(TimeSpan time)   // già implementata con interfaccia 
        {
            if (IsOn)
            {
                TurnedOffAt = DateTime.Now + time;
            }
            else
            {
                //errore: non puoi settare un timer se è spento
            }
        }

        /// <summary>
        /// Calcola il consumo totale in watt-ora usando sempre il valore di minConsumption per la modalità fornita.
        /// Converte il TimeSpan restituito da TimeOn() in ore tramite TotalHours e moltiplica per i watt (minConsumption).
        /// Restituisce un valore double che rappresenta i watt-ora totali.
        /// </summary>
        public double ConsumptionWattHours()
        {
            // get the min consumption based on the mode
            int min = ModeProperties[mode].minConsumption.Consumption;

            TimeSpan timeOn = TimeOn();
            double hours = timeOn.TotalHours;
            double wattHours = hours * min;
            return wattHours;
        }

        /// <summary>
        /// calculates the consumption in kilo watt hours
        /// </summary>
        public double ConsumptionKiloWattHours()
        {
            return ConsumptionWattHours() / 1000.0;
        }

        public double CostOfConsumption()
        {
            double kWh = ConsumptionKiloWattHours();
            return kWh * CostPerKWh.Value;
        }
    }
}
