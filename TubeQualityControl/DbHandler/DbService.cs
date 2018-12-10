using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TubeQualityControl.Entity;

namespace TubeQualityControl.DbHandler
{
    public class DbService
    {
        private int id = 0;
        DbHandler db;
        public event EventHandler NewDataReceived;

        public void Start()
        {
            db = new DbHandler();
            // GET biggest ID
            id = db.Find_Max_Id();

            //start timer
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {

            // polling for biggest id to changed while start
            int currentMax = db.Find_Max_Id();

            //raise an event on id changed
            if (currentMax > id)
            {
                if (NewDataReceived != null)
                    this.NewDataReceived(this, new NewDataEventArgs(currentMax));

                // update local biggest id
                id = currentMax;
            }

            

        }


    }
}
