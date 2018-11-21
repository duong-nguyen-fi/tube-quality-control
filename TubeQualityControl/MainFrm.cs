using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TubeQualityControl.Entity;
using TubeQualityControl.Forms;

namespace TubeQualityControl
{
    public partial class MainFrm : Form
    {
        private WelcomeFrm welcome;
        private MeasureFrm measureFrm1;
        private List<Part> allParts;
        private int step = 1;

        private Dictionary<int, UserControl> formDictionary = new Dictionary<int, UserControl>();
        public MainFrm()
        {
            InitializeComponent();

            Part part1 = new Part(1,"PLNA", 3);
            Part part2 = new Part(2,"PHOLEA", 3);
            Part part3 = new Part(3,"PLNB", 3);
            Part part4 = new Part(4,"PHOLEB", 3);

            allParts = new List<Part> {part1, part2, part3, part4};
/*
            Part part5;
            bool pipeOption = false;
            if (pipeOption)
            {
                part5 = new Part(5,"PIPE", 3);
                allParts.Add(part5);
            }
            else
            {
                int numOfHoles = 4;

                for (int i = 1; i <= numOfHoles; i++)
                {
                    Part part = new Part(5,"CIR" + i, 3);
                    allParts.Add(part);
                }
            }
*/
            progressBar1.Maximum = 120;
            progressBar1.Minimum = 0;

            welcome = new WelcomeFrm();
            formDictionary.Add(0,welcome);
            panel1.Controls.Add(welcome);
            welcome.StartBtnHandler += Welcome_StartBtnHandler;
            foreach (var part in allParts)
            {
                measureFrm1 = new MeasureFrm(part.Name, part);
                formDictionary.Add(part.Step,measureFrm1);
                measureFrm1.NextBtnHandler +=(sender,e) => MeasureFrm1_NextBtnHandler(sender, e, part);
            }

            foreach (var pair in formDictionary)
            {
                var value = pair.Value;
                if (value is MeasureFrm)
                    Debug.WriteLine((value as MeasureFrm)._Part);
            }

            //MessageBox.Show(Octave.OctaveHandler.Invoke());
        }

        private void MeasureFrm1_NextBtnHandler(object sender, EventArgs e, Part part)
        {
            
            
            if (step < formDictionary.Count-1)
            {
                step++;
                lbStepCount.Text = string.Format("Step {0}/6", step);
                int index = allParts.IndexOf(part);
                Debug.WriteLine("Part: " + part + "index:" + index);
                panel1.Controls.Remove(formDictionary[index + 1]);
                panel1.Controls.Add(formDictionary[index + 2]);
                progressBar1.Value = 120 / 6 * step;
            }
            else
            {
                
            }
        }

        private void Welcome_StartBtnHandler(object sender, EventArgs e)
        {
            panel1.Controls.Remove(welcome);
            panel1.Controls.Add(formDictionary[1]);
            lbStepCount.Text = string.Format("Step {0}/6", step);

        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
