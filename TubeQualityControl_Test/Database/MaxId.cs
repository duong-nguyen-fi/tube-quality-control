using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeQualityControl.DbHandler;

namespace TubeQualityControl_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetMaxId_NotEqualsZero()
        {
            TubeQualityControl.DbHandler.DbHandler db = new DbHandler();

            int id = db.Find_Max_Id();

            Assert.AreNotEqual(0, id);
        }
    }
}
