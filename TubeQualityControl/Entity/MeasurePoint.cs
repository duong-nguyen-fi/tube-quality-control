using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeQualityControl.Entity
{
    class MeasurePoint
    {
        public MeasurePoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Time_Stamp = GetTimestamp(DateTime.Now);
            
        }

        public int X { get; set; }

        public int Y { get; set; }
        public int Z { get; set; }

        
        public string Time_Stamp { set; get; }

        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
