using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeQualityControl.Octave;
namespace TubeQualityControl_Test.Octave
{
    [TestClass]
    public class InvokeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(OctaveHandler.Invoke().Contains(@"D:\Temp"));
        }
    }
}
