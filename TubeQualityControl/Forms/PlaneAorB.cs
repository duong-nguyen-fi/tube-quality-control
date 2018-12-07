using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TubeQualityControl.Forms
{
    public partial class PlaneAorB : UserControl
    {

        public event EventHandler NextBtnHandler;

        public string Selection;

        public PlaneAorB()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.NextBtnHandler != null && Selection != null)
                this.NextBtnHandler(this, e);
            else
                MessageBox.Show("Choose A or B", "Please choose", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Selection = "A";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Selection = "B";
        }
    }
}
