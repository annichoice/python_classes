using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinCFScan.Classes.IP
{
    public static class IPAddressExtensions
    {
        public static List<string> getIPRange(string ip, int network)
        {
            uint start, end, total;
            getIPRangeInfo(ip, network, out start, out end, out total);

            var ipRange = new List<string>();

            for (uint n = start; n < end; n++)
            {
                ipRange.Add(new IPAddress(BitConverter.GetBytes(n).Reverse().ToArray()).ToString());
            }

            return ipRange;

        }

        public static uint getIPRangeTotalIPs (string ipAndNet)
        {
            string[] splitted = ipAndNet.Split('/');
            getIPRangeInfo(splitted[0], Int32.Parse(splitted[1]), out uint start, out uint end, out uint total);
            return total;
         }


        public static void getIPRangeInfo(string ip, int network, 