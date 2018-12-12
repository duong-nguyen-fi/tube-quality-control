using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeQualityControl.DbHandler;
using TubeQualityControl.Entity;

namespace TubeQualityControl_Test.PollData
{
    [TestClass]
    public class NewData
    {
        [TestMethod]
        public void GetLatestData_NotNull()
        {
            MeasurePoint point = null;

            DbService service = new DbService();
            service.Start();

            point = service.GetNewPoint();

            service.Stop();

            Debug.WriteLine(point);

            Assert.AreNotEqual(null, point);
        }
    }
}
