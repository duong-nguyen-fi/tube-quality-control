using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeQualityControl.Octave
{
    public class OctaveHandler
    {
        public static string Invoke()
        {
            //var myFile = TubeQualityControl.Properties.Resources.joj_C4566ETP12BAREN_20180914;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            startInfo.WorkingDirectory = @"D:\Temp";
            startInfo.FileName = Properties.Settings.Default.Octave_Path;
            //startInfo.Arguments = "joj_C4566ETP12BAREN_20180914(2)";
            //startInfo.FileName = @"cmd";
            //startInfo.Arguments = "/C octave-cli --eval joj_C4566ETP12BAREN_20180914(2)";
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();

            System.IO.StreamWriter myStreamWriter = process.StandardInput;
            //myStreamWriter.WriteLine("octave");
            //myStreamWriter.WriteLine("cd D:\\Temp");
           // myStreamWriter.WriteLine("pwd");
            myStreamWriter.WriteLine("main(2)"); 
            myStreamWriter.Close();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return output;
        }
    }
}
