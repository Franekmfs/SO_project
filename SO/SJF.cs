using System;

namespace SO
{
    public class Sjf
    {
        private int processesCount;

        public Sjf(int processesCount)
        {
             this.processesCount = processesCount;
        }

        public void Calculate(int[,] processData)
        {
            ProcessData processdata = new ProcessData();
            string type = "duration";
            int tempDuration;
            int[] durationTimeTable = new int[processesCount];
            int[] waitTimeTable = new int[processesCount];
            int[] serviceTimeTable = new int[processesCount];
            int[,] sortedProcessData = processdata.SortBy(processData, type);
            int time = 0;
            int status=0;
            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;
            
            Console.Write("|");
            
            for (int i = 0; i < processesCount; i++)
            {
                waitTimeTable[i] = 0;
                durationTimeTable[i] = sortedProcessData[i, 2];
            }
            for (int n = 0; n < processesCount; n++)
            {
                for (int i = 0; i < processesCount; i++)
                {
                    if (durationTimeTable[i] != 0 && sortedProcessData[i, 1] <= time) 
                    {
                        tempDuration = durationTimeTable[i];
                        serviceTimeTable[i] = time;

                        for (int j = 0; j < tempDuration; j++)
                        {
                            time++;
                            durationTimeTable[i] = durationTimeTable[i] - 1;
                            Console.Write(sortedProcessData[i, 0] + "|");
                            status = 1;
                        }
                        break;
                    }
                }
                if (status == 1)
                {
                    status = 0;
                }
                else
                {
                    status = 0;
                    time++;
                    Console.Write(" |");
                    n--;
                }
            }
            processdata.DisplayData(waitTimeTable,serviceTimeTable,sortedProcessData,avgWaitTime,avgTurnAroundTime,time,processesCount);
        }
    }
}
