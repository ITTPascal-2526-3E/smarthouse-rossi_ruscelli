using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.Lamps;
using BlaisePascal.SmartHouse.Domain.Lamps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lightning.Lamps.Commands
{
    public class AddampCommand
    {
        private readonly ILampRepository lampRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lampRepository">Accetta tutte le classi che implementi questa interfaccia</param>

        public AddampCommand(ILampRepository lampRepository)
        {
            this.lampRepository = lampRepository;
        }

        public void Execute(NameDevice name, Brightness brightness)
        {
            var lamp = new Lamp(false, name, ColorType.White, brightness, LampType.LED);
            lampRepository.Add(lamp);
        }
    }
}
