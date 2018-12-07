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
        

        private Dictionary<int, UserControl> formDictionary = new Dictionary<int, UserControl>();
        public MainFrm()
        {
            InitializeComponent();

            Part part1 = new Part(1,"PLNA", 3);
            Part part2 = new Part(2,"PHOLEA", 3);
            Part part3 = new Part(3,"PLNB", 3);
            Part part4 = new Part(4,"PHOLEB", 3);

            allParts = new List<Part> {part1, part2, part3, part4};

            progressBar1.Maximum = 6;
            progressBar1.Minimum = 0;
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
            

            welcome = new WelcomeFrm();
            formDictionary.Add(0,welcome);
            panel1.Controls.Add(welcome);
            welcome.StartBtnHandler += Welcome_StartBtnHandler;

            //4 only meant for first 4 steps - use step count as dictionary key
            foreach (var part in allParts)
            {
                measureFrm1 = new MeasureFrm(part.Name, part);
                formDictionary.Add(part.Step,measureFrm1);
                measureFrm1.NextBtnHandler += MeasureFrm1_NextBtnHandler;
            }

            // init pipeOrPlane selection frm - form5
            PlaneOrPipeFrm planeOrPipeFrm = new PlaneOrPipeFrm();
            formDictionary.Add(5, planeOrPipeFrm);
            planeOrPipeFrm.NextBtnHandler += PlaneOrPipeFrm_NextBtnHandler;

           
        }

        private void PlaneOrPipeFrm_NextBtnHandler(object sender, EventArgs e)
        {
            string selection = (sender as PlaneOrPipeFrm).Selection;
            Debug.WriteLine("Selection: "+ selection);

            if (selection.Equals("Pipe"))
            {
                Part part5 = new Part(6, "PIPE", 3);
                allParts.Add(part5);
                measureFrm1 = new MeasureFrm(part5.Name, part5);
                measureFrm1.NextBtnHandler += MeasureFrm1_NextBtnHandler;
                formDictionary.Add(6, measureFrm1);
                panel1.Controls.Remove(sender as UserControl);
                panel1.Controls.Add(measureFrm1);
                UpdateStep(6);
            }
            else if (selection.Equals("Plane"))
            {
                var planeAb = new PlaneAorB();
                planeAb.NextBtnHandler += PlaneAB_NextBtnHandler;
                panel1.Controls.Remove(sender as UserControl);
                panel1.Controls.Add(planeAb);
                UpdateStep(6);
            }
        }

        private void PlaneAB_NextBtnHandler(object sender, EventArgs e)
        {
            var planeAb = sender as PlaneAorB;

            var selection = planeAb.Selection;
            Debug.WriteLine("Selection: " + selection);

            var planMeasureFrm = new PlaneMeasureFrm("MEASURE PARTS", new Part(6, "PLANE", 5));

            panel1.Controls.Remove(sender as UserControl);
            panel1.Controls.Add(planMeasureFrm);


        }

        private void MeasureFrm1_NextBtnHandler(object sender, EventArgs e)
        {
            var oldFrm = sender as MeasureFrm;

            //get index of the clicked form
            int index = formDictionary.FirstOrDefault(x => x.Value == oldFrm ).Key;
            Debug.WriteLine("\n -------- \nClicked Part: " + oldFrm._Part + "index:" + index);


            // check if next index available
            if (formDictionary.ContainsKey(index + 1))
            {
                UserControl newFormControl;
                formDictionary.TryGetValue(index + 1, out newFormControl);
                if (newFormControl is MeasureFrm)
                {
                    MeasureFrm newFrm = newFormControl as MeasureFrm;
                    int step = newFrm._Part.Step;

                    Debug.WriteLine("Next Part: " + newFrm._Part + "step:" + step);
                    UpdateStep(step);
                }

                if (newFormControl is PlaneOrPipeFrm)
                {
                    UpdateStep(5);
                }

                panel1.Controls.Remove(oldFrm);
                panel1.Controls.Add(newFormControl);

                
            }
            else
            {
                Debug.WriteLine("No next part");
            }
            

            

        }

        //start button - first form
        private void Welcome_StartBtnHandler(object sender, EventArgs e)
        {
            panel1.Controls.Remove(welcome);
            panel1.Controls.Add(formDictionary[1]);
            UpdateStep(1);
        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void UpdateStep(int step)
        {
            progressBar1.Value = step;
            lbStepCount.Text = string.Format("Step {0}/6", step);
        }
    }
}
