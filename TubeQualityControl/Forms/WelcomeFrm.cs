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
    public partial class WelcomeFrm : UserControl
    {
        public event EventHandler StartBtnHandler;

        public WelcomeFrm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.StartBtnHandler != null)
                this.StartBtnHandler(this, e);
        }
    }
}
