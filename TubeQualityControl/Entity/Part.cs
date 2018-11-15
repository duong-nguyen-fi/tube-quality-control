using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeQualityControl.Entity
{
    class Part
    {
        public string Name { get; set; }

        public int Suggest_points { get; set; }

        public List<MeasurePoint> MeasurePoints { get; set; }

        public Part(string name, int suggest_points, List<MeasurePoint> MeasurePoints)
        {
            Name = name;
            Suggest_points = suggest_points;
            MeasurePoints = new List<MeasurePoint>();
        }

        public Part() { }

     
      /*  public override String ToString() {
            return Name + Suggest_points+ String.Join(",",MeasurePoints);
           
        }*/
    }
}
