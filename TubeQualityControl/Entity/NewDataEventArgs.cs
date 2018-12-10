using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeQualityControl.Entity
{
    
        public class NewDataEventArgs : EventArgs
        {
            private int id;
            public NewDataEventArgs(int _id)
            {
                id = _id;
            }
            public int GetId()
            {
                return id;
            }
        }
    
}
