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

namespace TubeQualityControl
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();

            Part part1 = new Part("PLNA", 3);
            Part part2 = new Part("PHOLEA", 3);
            Part part3 = new Part("PLNB", 3);
            Part part4 = new Part("PHOLEB", 3);

            List<Part> allParts = new List<Part> {part1, part2, part3, part4};

            Part part5;
            bool pipeOption = false;
            if (pipeOption)
            {
                part5 = new Part("PIPE", 3);
                allParts.Add(part5);
            }
            else
            {
                int numOfHoles = 4;

                for (int i = 1; i <= numOfHoles; i++)
                {
                    Part part = new Part("CIR" + i, 3);
                    allParts.Add(part);
                }
            }

            foreach (var part in allParts)
                Debug.WriteLine(part);



            //MessageBox.Show(Octave.OctaveHandler.Invoke());
        }


    }
}
