using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCFScan.Classes
{
    internal class ExceptionMonitor
    {
        private string monitorName = "";
        private float greenErrRate = 5;
        private float warningErrRate;
        private int successCount = 0;
        private int errCount = 0;

        private Dictionary<string, int> errorsList = new Dictionary<string, int>();

        public ExceptionMonitor(string monitorName, float greenErrRate = 1, float warningErrRate = 5)
        {
            this.monitorName = monitorName;
            this.greenErrRate = greenErrRate;
            this.warningErrRate = warningErrRate;
        }

        public void a