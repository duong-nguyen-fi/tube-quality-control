using System;
using System.Diagnostics;
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
            string output = OctaveHandler.Invoke();
            
            Debug.WriteLine(output);

            Assert.AreNotEqual("", output);
        }
    }
}
