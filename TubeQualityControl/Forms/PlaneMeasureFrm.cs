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
using TubeQualityControl.DbHandler;

namespace TubeQualityControl.Forms
{
    public partial class PlaneMeasureFrm : UserControl
    {
        public event EventHandler OnFinishHandler;

        public Part _Part { get; set; }

        private int partNum;

        public DbService Service;

        bool serviceFlag = false;

        public List<Part> Parts = new List<Part>();

        public PlaneMeasureFrm(string description)
        {
            InitializeComponent();

            partNum = 0;
            _Part = new Part(6, "PLANE"+partNum,3);
            lbDes.Text = description;
            lbSuggest.Text ="/"+_Part.SuggestPoints ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Finished");
            if (OnFinishHandler != null)
                OnFinishHandler(this, e);

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

        protected override void OnLoad(EventArgs e)
        {
            Start_Service();
            lbActual.Text = _Part.PointCount + "";
            
            
        }


        private void Start_Service()
        {
            bool newPoint = false;
            serviceFlag = true;
            Service = new DbService();
            Service.Start();
            Service.NewDataReceived += (sender, e) =>
            {
                if (serviceFlag)
                {
                    newPoint = _Part.AddPoint(new MeasurePoint(GetRandomNumber(29.12, 120.12), GetRandomNumber(29.12, 120.12), GetRandomNumber(29.12, 120.12)));
                    lbActual.Invoke(new Action(() => lbActual.Text = _Part.PointCount.ToString()));
                }
            };
        }


        Random random = new Random(); // replace from new Random(DateTime.Now.Ticks.GetHashCode());
        // Since similar code is done in default constructor internally
        List<double> randomList = new List<double>();

        private double GetRandomNumber(double minimum, double maximum)
        {
            double num = random.NextDouble() * (maximum - minimum) + minimum;
            if (!randomList.Contains(num))
                randomList.Add(num);

            return num;
        }

    }
}
