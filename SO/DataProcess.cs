using System;
using System.Collections.Generic;

namespace SO
{
    public class DataProcess
    {

        public DataProcess(int PID, int INCOM, int DURAT)
        {
            this.incomingTime = INCOM;
            this.durationTime = DURAT;
        }

        private int incomingTime;
        private int durationTime;
        

        public int inc
        {
            get { return incomingTime; }
            set { incomingTime = value;  }
        }
        
        public int PID { get; set;}
        
        
        
    }
}