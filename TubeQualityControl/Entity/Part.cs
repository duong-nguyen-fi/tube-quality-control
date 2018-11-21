using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeQualityControl.Entity
{
    public class Part
    {
        public string Name { get; set; }

        public int SuggestPoints { get; set; }

        public int Step { get; set; }

        public List<MeasurePoint> MeasurePoints { get; set; }

        public Part(int step, string name, int suggestPoints)
        {
            Step = step;
            Name = name;
            SuggestPoints = suggestPoints;
            MeasurePoints = new List<MeasurePoint>();
        }

        public override string ToString()
        {
            return String.Format("Name: {0} - Step: {2} - Suggest Point: {3} - Actual Point: {1}",Name,MeasurePoints.Count, Step, SuggestPoints);

        }

        public Part() { }

     

    }
}
