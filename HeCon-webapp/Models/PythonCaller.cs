using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HeCon_webapp.Models
{
    public class PythonCaller
    {
        private static PythonCaller python;

        public static PythonCaller getPythonCaller()
        {
            if (python == null)
                python = new PythonCaller();
            return python;

        }

        public string[] callPython(string imagePath)
        {

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:/Users/Alin/Anaconda3/envs/tensorflow/python.exe";
            start.Arguments = string.Format("\"{0}\" \"{1}\"", "F:\\IP\\HeCon-ml\\script.py", imagePath);
            start.UseShellExecute = false;
            start.CreateNoWindow = true;
            start.WorkingDirectory = "F:\\IP\\HeCon-ml\\";
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            char[] trimChars = { '[', ']','\r','\n' };
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); 
                    string result = reader.ReadToEnd();
                    result = result.TrimStart(trimChars);
                    result = result.TrimEnd(trimChars);
                    return result.Split(' ');
                }
            }

        }
    }
}