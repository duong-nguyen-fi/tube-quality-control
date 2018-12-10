using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TubeQualityControl.DbHandler
{
    public class DbService
    {
        private int id = 0;
        public void Start()
        {
            //start timer
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {

            // GET biggest ID
            

            // polling for biggest id to changed while start



            //raise an event on id changed

            // update local biggest id
        }
    }
}
