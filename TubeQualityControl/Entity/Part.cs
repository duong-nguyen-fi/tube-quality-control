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

        public string Description { get; set; }

        public int PointCount
        {
            get { return MeasurePoints.Count; }
        }

        public List<MeasurePoint> MeasurePoints { get; set; }

        public Part(int step, string name, int suggestPoints, string description)
        {
            Step = step;
            Name = name;
            SuggestPoints = suggestPoints;
            Description = description;
            MeasurePoints = new List<MeasurePoint>();
        }

        public Part(int step, string name, int suggestPoints)
        {
            Step = step;
            Name = name;
            SuggestPoints = suggestPoints;
            MeasurePoints = new List<MeasurePoint>();
        }

        public Part(int step, string name)
        {
            Step = step;
            Name = name;
            MeasurePoints = new List<MeasurePoint>();
        }

        public Part(string name, List<MeasurePoint> measurePoints)
        {
            Name = name;
            MeasurePoints = measurePoints;
        }

        public Part()
        {
            MeasurePoints = new List<MeasurePoint>();
        }

        public override string ToString()
        {
            return String.Format("Name: {0} - Step: {2} - Suggest Point: {3} - Actual Point: {1}", Name, MeasurePoints.Count, Step, SuggestPoints);

        }

        public void Reset()
        {
            MeasurePoints = new List<MeasurePoint>();

        }

        public bool AddPoint(MeasurePoint point)
        {
            if (!MeasurePoints.Contains(point))
            {
                MeasurePoints.Add(point);
                return true;
            }

            return false;
        }

        public string ponitsToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (MeasurePoint mp in MeasurePoints)
            {
                builder.Append(mp.ToString()).Append("|");
            }
            string result = builder.ToString();
            return Name + result;
        }

    }
}
