using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.CCTV;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class CCTVTest
    {
        private Location GetLocation()
        {
            return new Location("Italy", "Milan", "Via Roma", "20100");
        }

        private LicensePlate[] GetSamplePlates()
        {
            return new LicensePlate[]
            {
                    new LicensePlate("ABC123"),
                    new LicensePlate("XYZ789")
            };
        }

        [Fact]
        public void Constructor_ShouldInitializeCorrectly()
        {
            var location = GetLocation();
            var plates = GetSamplePlates();

            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(false, location, false, plates, new NameDevice("cam1"));

            Assert.False(cctv.IsRecording);
            Assert.Equal(location, cctv.Location);
            Assert.False(cctv.LicensePlateRecognitionEnabled);
            Assert.Equal(plates, cctv.LicensePlateEnebled);
        }

        [Fact]
        public void TurnOn_ShouldSetIsOnTrue()
        {
            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(false, GetLocation(), false, GetSamplePlates(), new NameDevice("cam"));

            cctv.TurnOn();

            Assert.True(cctv.IsOn);
        }

        [Fact]
        public void TurnOff_ShouldSetIsOnFalse()
        {
            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(false, GetLocation(), false, GetSamplePlates(), new NameDevice("cam"));

            cctv.TurnOn();
            cctv.TurnOff();

            Assert.False(cctv.IsOn);
        }

        [Fact]
        public void StartRecording_ShouldSetIsRecordingTrue()
        {
            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(false, GetLocation(), false, GetSamplePlates(), new NameDevice("cam"));

            cctv.StartRecording();

            Assert.True(cctv.IsRecording);
        }

        [Fact]
        public void StopRecording_ShouldSetIsRecordingFalse()
        {
            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(true, GetLocation(), false, GetSamplePlates(), new NameDevice("cam"));

            cctv.StopRecording();

            Assert.False(cctv.IsRecording);
        }

        [Fact]
        public void EnableLicensePlateRecognition_ShouldEnable_WhenPlateExists()
        {
            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(false, GetLocation(), false, GetSamplePlates(), new NameDevice("cam"));

            cctv.EnableLicensePlateRecognition(new LicensePlate("ABC123"));

            Assert.True(cctv.LicensePlateRecognitionEnabled);
        }

        [Fact]
        public void EnableLicensePlateRecognition_ShouldDisable_WhenPlateNotExists()
        {
            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(false, GetLocation(), true, GetSamplePlates(), new NameDevice("cam"));

            cctv.EnableLicensePlateRecognition(new LicensePlate("NOTFOUND"));

            Assert.False(cctv.LicensePlateRecognitionEnabled);
        }

        [Fact]
        public void EnableLicensePlateRecognition_ShouldWorkWithEmptyArray()
        {
            var cctv = new BlaisePascal.SmartHouse.Domain.CCTV.CCTV(false, GetLocation(), false, new LicensePlate[0], new NameDevice("cam"));

            cctv.EnableLicensePlateRecognition(new LicensePlate("ABC123"));

            Assert.False(cctv.LicensePlateRecognitionEnabled);
        }
    }
}