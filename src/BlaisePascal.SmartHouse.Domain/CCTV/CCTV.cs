using BlaisePascal.SmartHouse.Domain.Abstractions;
using System;
using BlaisePascal.SmartHouse.Domain.Interfaces;
using BlaisePascal.SmartHouse.Domain.CCTV.CCTVInterfaces;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;


namespace BlaisePascal.SmartHouse.Domain.CCTV
{
    public sealed class CCTV : AbstractDevice, ICamera, ILicensePlateRecognition
    {
        private string name;
        private string location;

        public bool IsOn { get; private set; }
        public bool IsRecording { get; private set; }
        public Location Location { get; private set; }

        public LicensePlate[] LicensePlateEnebled { get; private set; }

        public bool LicensePlateRecognitionEnabled { get; private set; }

        public CCTV(bool isRecording, Location location, bool licensePlateRecognitionEnabled, LicensePlate[] licensePlateEnebled, NameDevice name) : base(name)
        {
            IsRecording = isRecording;
            Location = location;
            LicensePlateRecognitionEnabled = licensePlateRecognitionEnabled;
            LicensePlateEnebled = licensePlateEnebled;
        }

        public CCTV(NameDevice name, string location) : base(name)
        {
            this.location = location;
        }

        public void TurnOn()
        {
            IsOn = true;
        }
        public void TurnOff()
        {
            IsOn = false;
        }
        public void StartRecording()
        {
            IsRecording = true;
        }

        public void StopRecording()
        {
            IsRecording = false;
        }

        public void EnableLicensePlateRecognition(LicensePlate licensePlate)
        {
            LicensePlateRecognitionEnabled = false;
            for (int i = 0; i < LicensePlateEnebled.Length; i++)
            {
                if (LicensePlateEnebled[i].Number == licensePlate.Number)
                {
                    LicensePlateRecognitionEnabled = true;
                    break;
                }
            }
        }

        public void Update(bool isOn, string resolution, int frameRate)
        {
            throw new NotImplementedException();
        }
    }
}