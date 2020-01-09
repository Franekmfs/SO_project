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
            SortTable sort = new SortTable();
            string type = "duration";
            int tempDuration;
            int[] durationTimeTable = new int[processesCount];
            int[] waitTimeTable = new int[processesCount];
            int[] serviceTimeTable = new int[processesCount];
            string[] columnNames = {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish"};
            int[,] sortedProcessData = sort.SortBy(processData, type);
            int time = 0;
            int status=0;
            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;

            Console.WriteLine("");
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
