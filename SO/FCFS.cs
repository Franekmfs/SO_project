using System;

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
            SortTable sort = new SortTable();
            string type = "arrival";
            int[,] sortedProcessData = sort.SortBy(processData,type);
            int[] durationTimeTable = new int[processesCount];
            int[] waitTimeTable = new int[processesCount];
            int[] serviceTimeTable = new int[processesCount];

            string[] columnNames = {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish"};

            int time = 0;
            Console.Write("|");
           
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
                        durationTimeTable[i]--;
                        time++;
                    } while (durationTimeTable[i] > 0);
                }
                else
                {
                    Console.Write(" |");
                    time++;
                    i--;
                }
            }
            
            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;
            
            Console.WriteLine(" ");
            
            for(int i = 0; i < columnNames.Length; i++)
            {
                Console.Write(columnNames[i] + " ");
            }
            Console.WriteLine("");

            for (int i = 0; i < processesCount; i++)
            {
                waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];
                Console.Write(sortedProcessData[i,0] + "\t"); //PID
                Console.Write(sortedProcessData[i,1] + " ms\t"); //Arr
                Console.Write(sortedProcessData[i,2] + " ms\t"); //Dur
                Console.Write(waitTimeTable[i] + " ms\t"); //Wait
                Console.Write(sortedProcessData[i,2]+waitTimeTable[i] + " ms\t"); //Turn around
                Console.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                Console.Write(serviceTimeTable[i]+sortedProcessData[i,2] + " ms"); //Finish Time
                Console.WriteLine("");
                avgWaitTime = avgWaitTime + waitTimeTable[i];
                avgTurnAroundTime = avgTurnAroundTime + sortedProcessData[i, 2] + waitTimeTable[i];
            }
            Console.WriteLine("Average waiting time: "+ avgWaitTime/processesCount + " ms");
            Console.WriteLine("Average turnaround time: "+ avgTurnAroundTime/processesCount + " ms");
            Console.WriteLine("Total completion time: "+ time +" ms");
        }
    }
}