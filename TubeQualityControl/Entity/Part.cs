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

        public int SuggestPoints { get; set; }

        public List<MeasurePoint> MeasurePoints { get; set; }

        public Part(string name, int suggestPoints)
        {
            Name = name;
            SuggestPoints = suggestPoints;
            MeasurePoints = new List<MeasurePoint>();
        }

        public override string ToString()
        {
            return string.Format("Name: {0} - Actual Point: {1}",Name,MeasurePoints.Count);

        }

        public Part() { }

     

    }
}
