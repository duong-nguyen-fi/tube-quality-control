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

namespace TubeQualityControl.Forms
{
    public partial class MeasureFrm : UserControl
    {
        public event EventHandler NextBtnHandler;

        public Part _Part { get; set; }

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.NextBtnHandler != null)
                this.NextBtnHandler(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("RESET?", "WARNING", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                _Part.Reset();
            }
        }
    }
}
