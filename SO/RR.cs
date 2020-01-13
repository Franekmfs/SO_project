using System;

namespace SO
{
    public class Rr
    {
        private int processesCount;
        public Rr(int processesCount)
        {
            this.processesCount = processesCount;
        }
        public void Calculate(int[,] processData)
        {
            ProcessData processdata = new ProcessData();
            
            int quantum = processdata.AskQuantum();
            int time = 0;
            string gantt;
            int counter = 0;
            int totalTime = 0;
            int runs = 0;
            int[,] sortedProcessData = processdata.SortBy(processData,"pid");
            int[] durationTimeTable = new int[processesCount];
            int[] waitTimeTable = new int[processesCount];
            int[] serviceTimeTable = new int[processesCount];
            int[] finishTimeTable = new int[processesCount];
            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;

            Console.Write("|");
            gantt = "|";

            for (int i = 0; i < processesCount; i++)
            {
                totalTime = totalTime + sortedProcessData[i, 2];
                durationTimeTable[i] = sortedProcessData[i, 2];
                serviceTimeTable[i] = 0;
            }
            int[] firtsDigit = new int[totalTime];
            for (int n = 0; n < processesCount; n++)
            {
                for (int i = 0; i < processesCount; i++)
                {
                    if (sortedProcessData[i, 1] < time+1 && durationTimeTable[i]>0)
                    {
                        counter = 0;
                        if (serviceTimeTable[i] == 0 && runs < processesCount)
                        {
                            serviceTimeTable[i] = time;
                        }
                        do
                        {
                            Console.Write(sortedProcessData[i, 0] + "|");
                            gantt += sortedProcessData[i, 0] + "|";
                            durationTimeTable[i]--;
                            time++;
                            counter++;
                            
                            
                            if (durationTimeTable[i] <= 0)
                            {
                                break;
                            }
                            
                        } while (quantum > counter);
                        
                        finishTimeTable[i] = time;
                    }
                    runs++;
                }
                if (sortedProcessData[n, 1] > time && durationTimeTable[n] >= 0)
                {
                    time++;
                    Console.Write(" |");
                    gantt += " |";
                    n--;
                }
            }
            processdata.DisplayData(waitTimeTable,serviceTimeTable,sortedProcessData,firtsDigit,finishTimeTable,avgWaitTime,avgTurnAroundTime,time,processesCount,"Round Robin",gantt,quantum);
        }
    }
}