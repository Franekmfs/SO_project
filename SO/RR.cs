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
           
            int time = 0;
            int quantum = 2;
            int totalTime = 0;
            SortTable sort = new SortTable();
            int[,] sortedProcessData = sort.SortBy(processData,"pid");
            int[] durationTimeTable = new int[processesCount];
            int[] waitTimeTable = new int[processesCount];
            int[] serviceTimeTable = new int[processesCount];
            int[] finishTimeTable = new int[processesCount];
            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;
            string[] columnNames = {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish"};

            
            Console.WriteLine("");
            Console.Write("|");

         for (int i = 0; i < processesCount; i++)
         {
             totalTime = totalTime + sortedProcessData[i, 2];
             durationTimeTable[i] = sortedProcessData[i, 2];
         }
         int[] available = new int[totalTime];
            
            for (int i = 0; i < totalTime; i++)
            {
                //waitTimeTable[i] = 0;
                //
                available[i] = 0;

                for (int j = 0; j < processesCount; j++)
                {
                    if (sortedProcessData[j, 1] <= time)
                    {
                        available[i]++;
                    }
                }
                time++;
                //Console.WriteLine(available[i]);
            }

            int counter = 0;
            time = 0;
            int startTimeOfFirstProcess = 0;
            int[] firtsDigit = new int[totalTime];
            for (int g = 0; g < processesCount; g++)
            {
                for (int i = 0; i < processesCount; i++)
                {
                    if (sortedProcessData[i, 1] <= time && durationTimeTable[i]>0)
                    {
                        counter = 0;
                        if (serviceTimeTable[i] == 0)
                        {
                            serviceTimeTable[i] = time;
                        }
                        do
                        {
                            Console.Write(sortedProcessData[i, 0] + "|");
                            firtsDigit[time] = sortedProcessData[i, 0];
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
                }
                if (counter == 0)
                {
                    time++;
                }
            }
            
            Console.WriteLine("");
            
            for(int i = 0; i < columnNames.Length; i++)
            {
                Console.Write(columnNames[i] + " ");
            }
            Console.WriteLine("");
            
            
            for (int i = 0; i < processesCount; i++)
            {
                waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];

                if (sortedProcessData[i, 0] == firtsDigit[0])
                {
                    waitTimeTable[i] = 0;
                }    
                
                Console.Write(sortedProcessData[i,0] + "\t"); //PID
                Console.Write(sortedProcessData[i,1] + " ms\t"); //Arr
                Console.Write(sortedProcessData[i,2] + " ms\t"); //Dur
                Console.Write(waitTimeTable[i] + " ms\t"); //Wait
                Console.Write(finishTimeTable[i]-sortedProcessData[i,1] + " ms\t"); //Turn around
                Console.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                //Console.Write(serviceTimeTable[i]+sortedProcessData[i,2] + " ms"); //Finish Time
                Console.Write(finishTimeTable[i] + " ms"); //Finish Time
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