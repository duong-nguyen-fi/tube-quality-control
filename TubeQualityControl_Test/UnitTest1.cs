using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeQualityControl.DbHandler;

namespace TubeQualityControl_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TubeQualityControl.DbHandler.DbHandler db = new DbHandler();

            int id = db.Find_Max_Id();

            Assert.Equals(id, 8);
        }
    }
}
