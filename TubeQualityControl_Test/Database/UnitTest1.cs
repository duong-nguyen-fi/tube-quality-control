using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeQualityControl;
using TubeQualityControl.DbHandler;

namespace TubeQualityControl_Test.Database
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetMaxId()
        {
            DbHandler db = new DbHandler();

            int maxId = db.Find_Max_Id();

            Assert.AreEqual(8,maxId);
        }
    }
}
