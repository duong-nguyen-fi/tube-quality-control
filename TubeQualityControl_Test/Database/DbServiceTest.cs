using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TubeQualityControl;
using TubeQualityControl.DbHandler;
using System.Timers;
using System.Diagnostics;
using TubeQualityControl.Entity;

namespace TubeQualityControl_Test.Database
{
    [TestClass]
    public class DbServiceTest
    {
        int id;
        int second = 0;
        DbHandler db;
        public event EventHandler NewDataReceived;

        [TestMethod]
        public void GetMaxId()
        {
            db    = new DbHandler();

            int maxId = db.Find_Max_Id();

            Assert.AreEqual(8,maxId);
        }

        int newMaxId;
        [TestMethod]
        public void DbService()
        {
            db = new DbHandler();
            id = db.Find_Max_Id();
            //start timer
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;

            this.NewDataReceived += (sender, e) =>
            {
                newMaxId = (e as NewDataEventArgs).GetId();
                
            };
            while(newMaxId == 0)
            {

            }

            Debug.WriteLine("old ID = {0} - new ID = {1}", id, newMaxId);
            Assert.AreEqual((id+1), newMaxId);
        }

        

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine(++second + " second passes");
            // polling for biggest id to changed while start
            int currentMax = db.Find_Max_Id();

            //raise an event on id changed
            if (currentMax > id)
            {
                if (NewDataReceived != null)
                    this.NewDataReceived(this, new NewDataEventArgs(currentMax));

                // update local biggest id
            }
        }
    }

}
