using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeQualityControl.DbHandler;
using TubeQualityControl.Entity;
using System.Diagnostics;

namespace TubeQualityControl_Test.PollData
{
    [TestClass]
    public class Poll_New_Data
    {
        [TestMethod]
        public void StartDbService_Get_NewData()
        {
            Part _Part = new Part();
            bool newPoint = false;

            DbService service = new DbService();
            service.Start();
            service.NewDataReceived += (sender, e) =>
            {
                
                newPoint = _Part.AddPoint(new MeasurePoint(GetRandomNumber(29.12,120.12), GetRandomNumber(29.12, 120.12), GetRandomNumber(29.12, 120.12)));

                
            };

            while (!newPoint) { }

            Debug.WriteLine(_Part);

            service.Stop();

            Assert.AreNotEqual(0, _Part.PointCount);

            
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        

    }
}
