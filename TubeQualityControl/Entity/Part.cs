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
    }
}
