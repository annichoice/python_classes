using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCFScan.Classes
{
    public class LogControl
    {
        private static string _Path = string.Empty;
        private static bool DEBUG = true;

        public static void Write(string msg, string filename = "log.txt")
        {
            _Path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(Path.Combine(_Path, filename)))
                {
                    Log(msg, w);
                }
                