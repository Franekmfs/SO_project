using System;
using System.Threading;

namespace SO
{
    public class Fcfs
    {
        private int processesCount;
        public Fcfs(int processesCount)
        {
            this.processesCount = processesCount;
        }
        public void Calculate(int[,] processData)
        {
            ProcessData processdata = new ProcessData();
            string type = "arrival";
            string gantt;
            int[,] sortedProcessData = processdata.SortBy(processData,type);
            int[] durationTimeTable = new int[processesCount];
            int[] waitTimeTable = new int[processesCount];
            int[] serviceTimeTable = new int[processesCount];
            int time = 0;
            Console.Write("|");
            gantt = "|";
           
            for (int i = 0; i < processesCount; i++)
            {
                waitTimeTable[i] = 0;
                durationTimeTable[i] = sortedProcessData[i, 2];
            }
            for (int i = 0; i < processesCount; i++)
            {
                if (sortedProcessData[i, 1] <= time)
                {
                    serviceTimeTable[i] = time;
                    do
                    {
                        Console.Write(sortedProcessData[i, 0] + "|");
                        gantt += sortedProcessData[i, 0] + "|";
                        durationTimeTable[i]--;
                        time++;
                        Thread.Sleep(50);

                    } while (durationTimeTable[i] > 0);
                }
                else
                {
                    Console.Write(" |");
                    gantt += " |";
                    time++;
                    i--;
                    Thread.Sleep(50);

                }
            }
            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;
            processdata.DisplayData(waitTimeTable,serviceTimeTable,sortedProcessData,avgWaitTime,avgTurnAroundTime,time,processesCount,"First Come First Serve",gantt);
            
        }
    }
}