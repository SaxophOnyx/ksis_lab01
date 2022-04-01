using System.Collections.Generic;

namespace TracertApp
{
    public class TracertFrame
    {
        public string RemoteIP { get; set; }
        public int HopNumber { get; set; }
        public bool Success { get; set; }
        public List<int> Attempts { get; set; }


        public TracertFrame(string ip, int hopNumber, bool success, params int[] attempts)
        {
            RemoteIP = ip;
            HopNumber = hopNumber;
            Success = success;

            Attempts = new List<int>(attempts.Length);
            foreach (int a in attempts)
                Attempts.Add(a);
        }

        public TracertFrame()
        {
            Success = false;
            Attempts = new List<int>();
            RemoteIP = "EMPTY_STR";
        }
    }
}
