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


        public static void getIPRangeInfo(string ip, int network, out uint start,  out uint end,  out uint total)
        {
            IPAddress netAddr = SubnetMask.CreateByNetBitLength(network);
            byte[] mask = netAddr.GetAddressBytes();
            byte[] iprev = IPAddress.Parse(ip).GetAddressBytes();
            // Network id - network address
            byte[] netid = BitConverter.GetBytes(BitConverter.ToUInt32(iprev, 0) & BitConverter.ToUInt32(mask, 0));
            // Binary inverted netmask
            byte[] inv_mask = mask.Select(r => (byte)~r).ToArray();
            // Broadcast address
            byte[] brCast = BitConverter.GetBytes(BitConverter.ToUInt32(netid, 0) ^ BitConverter.ToUInt32(inv_mask, 0));            

            start = BitConverter.ToUInt32(netid.Reverse().ToArray(), 0) + 1;
            end = BitConverter.ToUInt32(brCast.Reverse().ToArray(), 0);
      