using BlaisePascal.SmartHouse.Domain.Abstractions;
using System;
using BlaisePascal.SmartHouse.Domain.Interfaces;


namespace BlaisePascal.SmartHouse.Domain.CCTV
{
    public sealed class CCTV : AbstractDevice, Iswitch, IRecording, ILicensePlateRecognition
    {
        public bool IsOn { get; private set; }
        public bool IsRecording { get; private set; }
        public string Location { get; private set; }

        public string[] LicensePlateEnebled { get; private set; }

        public bool LicensePlateRecognitionEnabled { get; private set; }

        public CCTV(bool isRecording, string location, bool licensePlateRecognitionEnabled, string[] licensePlateEnebled, string name) : base(name)
        {
            IsRecording = isRecording;
            Location = location;
            LicensePlateRecognitionEnabled = licensePlateRecognitionEnabled;
            LicensePlateEnebled = licensePlateEnebled;
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

        public void EnableLicensePlateRecognition(string licensePlate)
        {
            LicensePlateRecognitionEnabled = false;
            for (int i = 0; i < LicensePlateEnebled.Length; i++)
            {
                if (LicensePlateEnebled[i] == licensePlate)
                {
                    LicensePlateRecognitionEnabled = true;
                    break;
                }
            }
        }
    }
}