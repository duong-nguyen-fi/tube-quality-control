using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeQualityControl.Entity
{
    public class MeasurePoint
    {
        public MeasurePoint(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Time_Stamp = GetTimestamp(DateTime.Now);

        }

        public double X { get; set; }

        public double Y { get; set; }
        public double Z { get; set; }

        public override string ToString()
        {
            return X.ToString() + Y.ToString() + Z.ToString();
        }

        public string Time_Stamp { set; get; }

        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}