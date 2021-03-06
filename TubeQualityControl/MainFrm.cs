﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private List<Part> iniParts;
        public static string CurrentDir;

        private Dictionary<int, UserControl> formDictionary = new Dictionary<int, UserControl>();
        public MainFrm()
        {
            InitializeComponent();
            CurrentDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Part part1 = new Part(1,"PLNA", 6, "Measure the first plane (A), take at 6 measurements");
            Part part2 = new Part(2,"PHOLEA", 3, "Measure the pipe hole, take at 3 measurements");
            Part part3 = new Part(3,"PLNB", 6, "Measure the first plane (B), take at 6 measurements");
            Part part4 = new Part(4,"PHOLEB", 3, "Measure the pipe hole, take at 3 measurements");

            iniParts = new List<Part> {part1, part2, part3, part4};

            progressBar1.Maximum = 6;
            progressBar1.Minimum = 0;

            Debug.WriteLine(CurrentDir);
            

            welcome = new WelcomeFrm();
            formDictionary.Add(0,welcome);
            panel1.Controls.Add(welcome);
            welcome.StartBtnHandler += Welcome_StartBtnHandler;

            //4 only meant for first 4 steps - use step count as dictionary key
            foreach (var part in iniParts)
            {
                measureFrm1 = new MeasureFrm(part.Description, part);
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
                Part part5 = new Part(6, "PIPE", 9, "Measure the pipe, take at 9 measurements");
                iniParts.Add(part5);
                measureFrm1 = new MeasureFrm(part5.Description, part5);
                measureFrm1.NextBtnHandler += MeasureFrm1_NextBtnHandler;
                measureFrm1.OnFinishedHandler += (sende, evt) =>
                {
                    measureFrm1.Service.Stop();
                    Finish();
                };
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


        private bool finished = false;
        List<Part> AllParts = new List<Part>();
        private void Finish()
        {
            if (!finished)
            {
                // add all parts to list
                foreach (var pair in formDictionary)
                {
                    if (pair.Value is MeasureFrm)
                    {
                        var part = (pair.Value as MeasureFrm)._Part;
                        AllParts.Add(part);
                    }

                    if (pair.Value is PlaneMeasureFrm)
                    {
                        var form = (pair.Value as PlaneMeasureFrm);
                        foreach (var part in form.Parts)
                        {
                            AllParts.Add(part);
                            Debug.WriteLine("Adding {0} in PlaneForm", part.Name);
                        }
                    }
                }

                finished = true;
            }
            // after upper block finish = true
            if (finished){
                //write xml
                XmlHandler.XmlWriter.WriteXml(AllParts);
                //run Octave
                Octave.OctaveHandler.Invoke();

                DialogResult dr = MessageBox.Show("Do you want to run again?", "Measurement finished",  MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    Application.Restart();
                    Environment.Exit(0);
                }
                if (dr == DialogResult.No)
                {
                    
                    this.Close();

                }
            }
            
        }

        // create planemeasurefrm here
        private void PlaneAB_NextBtnHandler(object sender, EventArgs e)
        {
            var planeAb = sender as PlaneAorB;

            var selection = planeAb.Selection;
            Debug.WriteLine("Selection: " + selection);

            var planMeasureFrm = new PlaneMeasureFrm("MEASURE PARTS");
            formDictionary.Add(6,planMeasureFrm);
            planMeasureFrm.OnFinishHandler += (sende, evt) =>
            {
                planMeasureFrm.Service.Stop();
                Finish();
            };


            panel1.Controls.Remove(sender as UserControl);
            panel1.Controls.Add(planMeasureFrm);


        }

        // measureFrm next button
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
