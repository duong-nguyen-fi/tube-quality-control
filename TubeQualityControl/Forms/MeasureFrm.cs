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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.NextBtnHandler != null)
                this.NextBtnHandler(this, e);
        }
    }
}
