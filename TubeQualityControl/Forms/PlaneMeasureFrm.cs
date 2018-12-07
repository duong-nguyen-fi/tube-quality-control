using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TubeQualityControl.Entity;
using System.Diagnostics;

namespace TubeQualityControl.Forms
{
    public partial class PlaneMeasureFrm : UserControl
    {
        public Part _Part { get; set; }

        public PlaneMeasureFrm(string description, Part part)
        {
            InitializeComponent();
            _Part = part;
            lbDes.Text = description;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Finished");
            var part1 = new Entity.Part(6, "PLNA",3);
            var point1 = new Entity.MeasurePoint(12, 52, 22);
            var point2 = new Entity.MeasurePoint(32, 59, 222);
            var point3 = new Entity.MeasurePoint(120, 124, 233);

            List<Entity.MeasurePoint> points = new List<Entity.MeasurePoint>() { point1, point2, point3 };
            part1.MeasurePoints = points;


            var part2 = new Entity.Part(6, "PLNB", 3);

            List<Entity.MeasurePoint> points1 = new List<Entity.MeasurePoint>() { point1, point2};
            part2.MeasurePoints = points1;


            List<Entity.Part> parts = new List<Entity.Part>() { part1, part2 };

            

            foreach (var point in part1.MeasurePoints)

                Debug.WriteLine(point);

            XmlHandler.XmlWriter.WriteXml(parts);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to reset", "ARE YOU SURE?", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                _Part.Reset();
            }
        }


    }
}
