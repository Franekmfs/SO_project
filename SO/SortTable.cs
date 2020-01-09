using System;

namespace SO
{
    public class SortTable
    {
        public int[,] SortBy(int[,] processData, string by)
        {
            int type = 0;
            if (by == "arrival")
            {
                type = 1;
            }
            if (by == "duration")
            {
                type = 2;
            }
            if (by == "priority")
            {
                type = 3;
            }
            
            int[] temp = new int[4];
            for (int i = 0; i < processData.GetLength(0) - 2; i++)
            {
                for (int j = 0; j < processData.GetLength(0) - 2; j++){
				
                    if (processData[j, type] > processData[j + 1, type]){
                        for (int k = 0; k < 4; k++)
                        {
                            temp[k] = processData[j, k];
                            processData[j, k] = processData[j + 1, k];
                            processData[j + 1, k] = temp[k];
                        }
                    }
                }
            }
            return processData;
        }

        public void DisplayTable(int[,] processData)
        {
            
            for (int i = 0; i < processData.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(processData[i,j] + "\t");
                }
                Console.WriteLine(" ");
            }
            
        }
        
        public void DisplayData(int[] waitTimeTable, int[] serviceTimeTable, int[,] sortedProcessData, int[] firtsDigit, int[] finishTimeTable, double avgWaitTime, double avgTurnAroundTime, int time, int processesCount)
        {
            string[] columnNames = {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish"};

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
