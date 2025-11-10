using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.AirFryer
{
    internal class AirFryer
    {
        //todo: implementare metodo che aumenta la temperatura gradualmente in base al tempo
        //todo: aggiungere anche una funzione timer per spegnimento automatico
        //todo: calcolare consumo energetico in base al tempo di utilizzo e alla modalitá
        //todo: volendo aggiungere anche una funzione che permetta la programmazione dell'accensione/spegnimento
        //todo: implementare main per testare la classe


        private int Temp;
        private int MaxTemp;
        private int MaxConsumption;
        private int minConsumption;
        private int CurrentConsumption;
        private bool IsOn;
        private DateTime TurnedOnAt;
        private DateTime TurnedOffAt;
        private float CostPerKWh;
        private DateTime AutoTurnOffAt;
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
        public int MinConsumptionProperty
        {
            get { return minConsumption; }
            set { minConsumption = value; }
        }
        public int TempProperty{ get; set; }
        public int MaxTempProperty { get; set; }
        public int MaxConsumptionProperty { get;set; }
        public float CostPerKWhProperty { get; set; }
        private DateTime TurnedOnAtProperty {  get; set; }
        private DateTime TurnedOffAtProperty {  get; set; }
        private DateTime AutoTurnOffAtProperty {get; set; }
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
        public void AirFry(int temp, int maxTemp,int maxConsumption, bool isOn, float costPerKWh)
        {
            CostPerKWh = costPerKWh;
            Temp = temp;
            MaxTemp = maxTemp;
            MaxConsumption = maxConsumption;
            bool IsOn = isOn;
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
            if (IsOn && Temp==MaxTemp)
            {
                return minConsumption;
            }
            else if (IsOn && Temp<MaxTemp)
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
                TimeSpan timeOn = DateTime.Now- TurnedOnAt;
                return timeOn;
            }
            else
            {
                TimeSpan timeOn = TurnedOffAt-TurnedOnAt;
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

    }
}
