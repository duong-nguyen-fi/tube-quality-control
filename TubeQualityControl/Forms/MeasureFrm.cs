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
using TubeQualityControl.DbHandler;
using System.Diagnostics;

namespace TubeQualityControl.Forms
{
    public partial class MeasureFrm : UserControl
    {
        public event EventHandler NextBtnHandler;

        public event EventHandler OnFinishedHandler;

        public Part _Part { get; set; }

        public DbService Service;

        bool serviceFlag = false;

        public MeasureFrm(string description, Part part)
        {
            _Part = part;
            InitializeComponent();
            lbDes.Text = description;
            lbSuggest.Text = "/"+part.SuggestPoints;
            lbActual.Text = "0";
            if (part.Name.Equals("PLNA") || part.Name.Equals("PLNB"))
            {
                pictureBox1.Image = Properties.Resources.planeAB;
            }
            else if (part.Name.Equals("PIPE"))
            {
                pictureBox1.Image = Properties.Resources.pipe;

                var btnFinish = new Button()
                {
                    Text = "Finish",
                    Name = "btnFinish",
                    Location = button2.Location,
                    ForeColor = button1.ForeColor,
                    BackColor = button1.BackColor,
                    Size = button2.Size,
                    Font = button1.Font,
                    UseVisualStyleBackColor = true

                };

                button2.Visible = false;
                this.Controls.Add(btnFinish);
                btnFinish.Click += BtnFinish_Click;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.pipe_hole;
            }

        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Finished");
            serviceFlag = false;

            if (OnFinishedHandler != null)
                this.OnFinishedHandler(this, e);
        }
        
        //next button
        private void button2_Click(object sender, EventArgs e)
        {
            serviceFlag = false;

            Debug.WriteLine(_Part);
            if (this.NextBtnHandler != null)
                this.NextBtnHandler(this, e);
        }

        //reset button
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("RESET?", "WARNING", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                _Part.Reset();
                lbActual.Invoke(new Action(() => lbActual.Text = _Part.PointCount.ToString()));
            }
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        protected override void OnLoad(EventArgs e)
        {
            lbActual.Text = "0";
            Start_Service();
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
    }
}
