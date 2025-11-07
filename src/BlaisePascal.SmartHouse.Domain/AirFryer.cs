using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    internal class AirFryer
    {
        private int Temp;
        private int MaxTemp;
        private int MaxConsumption;
        private float CurrentConsumption;
        public enum Mode
        {
            frying,
            baking,
            grill,
            reheat,
            dehydrate,
            toast

        }
        private static readonly Dictionary<Mode, (int maxConsumption, int minConsumption)> ModeProperties = new()
        {
            { Mode.frying, (1500, 800) },
            { Mode.baking, (1500, 700) },
            { Mode.grill, (1500, 900) },
            { Mode.reheat, (1500, 600) },
            { Mode.dehydrate, (1500, 250) },
            { Mode.toast, (1500, 900) }
        };

        public int TempProperty{ get; set; }
        public int MaxTempProperty { get; set; }
        public int MaxConsumptionProperty { get;set; }
        public int CurrentConsumptionProperty
        {
            set { CurrentConsumption = value; }
        }
        public void AirFry(int temp, int maxTemp,int maxConsumption)
        {
            Temp = temp;
            MaxTemp = maxTemp;
            MaxConsumption = maxConsumption;
        }
  
        
         
    }
}
