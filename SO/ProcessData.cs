using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace SO
{
    public class ProcessData
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
            if (type != 3)
            {
                for (int i = 0; i < processData.GetLength(0) - 2; i++)
                {
                    for (int j = 0; j < processData.GetLength(0) - 2; j++)
                    {

                        if (processData[j, type] > processData[j + 1, type])
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                temp[k] = processData[j, k];
                                processData[j, k] = processData[j + 1, k];
                                processData[j + 1, k] = temp[k];
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < processData.GetLength(0) - 2; i++)
                {
                    for (int j = 0; j < processData.GetLength(0) - 2; j++)
                    {

                        if (processData[j, type] < processData[j + 1, type])
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                temp[k] = processData[j, k];
                                processData[j, k] = processData[j + 1, k];
                                processData[j + 1, k] = temp[k];
                            }
                        }
                    }
                }
            }

            return processData;
        }

        private void Report(int[] waitTimeTable, int[] serviceTimeTable, int[,] sortedProcessData, int time, int processesCount, string name, string gantt)
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "/" + name + "_report.txt";
            Console.WriteLine("If you want to select different path, input the file localisation now");
            Console.WriteLine("actual path: " + filePath + " )");

            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;
            string userInput = Console.ReadLine();
            if(!(userInput == "")) 
            {
                filePath = userInput;
            }
            
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    Console.WriteLine("Cannot access file in the provided path. Exiting.");
                    Environment.Exit(2);
                }
            }
            
            using (FileStream fsream = File.Create(filePath))
            {
                using (StreamWriter stwr = new StreamWriter(fsream))
                {
                    try
                    {
                        stwr.WriteLine(name + " report");
                        stwr.WriteLine("");
                        stwr.WriteLine(gantt);

                        string[] columnNames =
                            {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish"};
                        stwr.WriteLine("");
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            stwr.Write(columnNames[i] + " ");
                        }

                        stwr.WriteLine("");

                        for (int i = 0; i < processesCount; i++)
                        {
                            waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];
                            stwr.Write(sortedProcessData[i, 0] + "\t"); //PID
                            stwr.Write(sortedProcessData[i, 1] + " ms\t"); //Arr
                            stwr.Write(sortedProcessData[i, 2] + " ms\t"); //Dur
                            stwr.Write(waitTimeTable[i] + " ms\t"); //Wait
                            stwr.Write(sortedProcessData[i, 2] + waitTimeTable[i] + " ms\t"); //Turn around
                            stwr.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                            stwr.Write(serviceTimeTable[i] + sortedProcessData[i, 2] + " ms"); //Finish Time
                            stwr.WriteLine("");
                            avgWaitTime = avgWaitTime + waitTimeTable[i];
                            avgTurnAroundTime = avgTurnAroundTime + sortedProcessData[i, 2] + waitTimeTable[i];
                        }

                        stwr.WriteLine("Average waiting time: " + avgWaitTime / processesCount + " ms");
                        stwr.WriteLine("Average turnaround time: " + avgTurnAroundTime / processesCount + " ms");
                        stwr.WriteLine("Total completion time: " + time + " ms");
                    }catch
                    {
                        Console.WriteLine("Cannot access file in the provided path. Exiting.");
                        Environment.Exit(2);
                    }
                }
                
            }
            
        }

        private void Report(int[] waitTimeTable, int[] serviceTimeTable, int[,] sortedProcessData,int time, int processesCount, string type, string name, string gantt)
        {
            
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "/" + name + "_report.txt";
            Console.WriteLine("If you want to select different path, input the file localisation now");
            Console.WriteLine("actual path: " + filePath + " )");

            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;
            
            string userInput = Console.ReadLine();
            if(!(userInput == "")) 
            {
                filePath = userInput;
            }
            
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    Console.WriteLine("Cannot access file in the provided path. Exiting.");
                    Environment.Exit(2);
                }
            }
            
            using (FileStream fsream = File.Create(filePath))
            {
                using (StreamWriter stwr = new StreamWriter(fsream))
                {
                    try
                    {

                        stwr.WriteLine(name + " report");
                        stwr.WriteLine("");
                        stwr.WriteLine(gantt);

                        string[] columnNames =
                            {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish ", "Priority  "};
                        stwr.WriteLine("");
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            stwr.Write(columnNames[i] + " ");
                        }

                        stwr.WriteLine("");

                        for (int i = 0; i < processesCount; i++)
                        {
                            waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];
                            stwr.Write(sortedProcessData[i, 0] + "\t"); //PID
                            stwr.Write(sortedProcessData[i, 1] + " ms\t"); //Arr
                            stwr.Write(sortedProcessData[i, 2] + " ms\t"); //Dur
                            stwr.Write(waitTimeTable[i] + " ms\t"); //Wait
                            stwr.Write(sortedProcessData[i, 2] + waitTimeTable[i] + " ms\t"); //Turn around
                            stwr.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                            stwr.Write(serviceTimeTable[i] + sortedProcessData[i, 2] + " ms\t"); //Finish Time
                            stwr.Write(sortedProcessData[i, 3] + " ms"); //Priority
                            stwr.WriteLine("");
                            avgWaitTime = avgWaitTime + waitTimeTable[i];
                            avgTurnAroundTime = avgTurnAroundTime + sortedProcessData[i, 2] + waitTimeTable[i];
                        }

                        stwr.WriteLine("Average waiting time: " + avgWaitTime / processesCount + " ms");
                        stwr.WriteLine("Average turnaround time: " + avgTurnAroundTime / processesCount + " ms");
                        stwr.WriteLine("Total completion time: " + time + " ms");
                    }catch
                    {
                        Console.WriteLine("Cannot access file in the provided path. Exiting.");
                        Environment.Exit(2);
                    }
                }
            }
            
        }

        private void ReportRR(int[] waitTimeTable, int[] serviceTimeTable, int[,] sortedProcessData, int time, int processesCount, string name, string gantt, int[] firstDigit, int[] finishTimeTable, int quantum)
        {
            
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "/" + name + "_report.txt";
            Console.WriteLine("If you want to select different path, input the file localisation now");
            Console.WriteLine("actual path: " + filePath + " )");
            double avgWaitTime = 0;
            double avgTurnAroundTime = 0;
            string userInput = Console.ReadLine();
            if(!(userInput == "")) 
            {
                filePath = userInput;
            }
            
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    Console.WriteLine("Cannot access file in the provided path. Exiting.");
                    Environment.Exit(2);
                }
            }
            
            using (FileStream fsream = File.Create(filePath))
            {
                using (StreamWriter stwr = new StreamWriter(fsream))
                {
                    try
                    {
                        stwr.WriteLine(name + " report");
                        stwr.WriteLine("");
                        stwr.WriteLine("Quantum: " + quantum);
                        stwr.WriteLine(gantt);

                        string[] columnNames =
                            {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish "};
                        stwr.WriteLine("");
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            stwr.Write(columnNames[i] + " ");
                        }

                        stwr.WriteLine("");

                        for (int i = 0; i < processesCount; i++)
                        {
                            waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];

                            if (sortedProcessData[i, 0] == firstDigit[0])
                            {
                                waitTimeTable[i] = 0;
                            }

                            stwr.Write(sortedProcessData[i, 0] + "\t"); //PID
                            stwr.Write(sortedProcessData[i, 1] + " ms\t"); //Arrival
                            stwr.Write(sortedProcessData[i, 2] + " ms\t"); //Duration
                            stwr.Write(waitTimeTable[i] + " ms\t"); //Wait
                            stwr.Write(finishTimeTable[i] - sortedProcessData[i, 1] + " ms\t"); //Turn around
                            stwr.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                            stwr.Write(finishTimeTable[i] + " ms"); //Finish Time
                            stwr.WriteLine("");
                            avgWaitTime = avgWaitTime + waitTimeTable[i];
                            avgTurnAroundTime = avgTurnAroundTime + sortedProcessData[i, 2] + waitTimeTable[i];
                        }

                        stwr.WriteLine("Average waiting time: " + avgWaitTime / processesCount + " ms");
                        stwr.WriteLine("Average turnaround time: " + avgTurnAroundTime / processesCount + " ms");
                        stwr.WriteLine("Total completion time: " + time + " ms");
                    }catch
                    {
                        Console.WriteLine("Cannot access file in the provided path. Exiting.");
                        Environment.Exit(2);
                    }
                    
                }
            }
            
        }


        public void DisplayData(int[] waitTimeTable, int[] serviceTimeTable, int[,] sortedProcessData, int[] firstDigit, int[] finishTimeTable, double avgWaitTime, double avgTurnAroundTime, int time, int processesCount, string name, string gantt, int quantum)
        {
            string[] columnNames = {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish"};

            Console.WriteLine("");

            for (int i = 0; i < columnNames.Length; i++)
            {
                Console.Write(columnNames[i] + " ");
            }

            Console.WriteLine("");


            for (int i = 0; i < processesCount; i++)
            {
                //waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];
                //wait = turn - dur
                waitTimeTable[i] = finishTimeTable[i] - sortedProcessData[i, 1] - sortedProcessData[i, 2];

                if (sortedProcessData[i, 0] == firstDigit[0])
                {
                    waitTimeTable[i] = 0;
                }

                Console.Write(sortedProcessData[i, 0] + "\t"); //PID
                Console.Write(sortedProcessData[i, 1] + " ms\t"); //Arrival
                Console.Write(sortedProcessData[i, 2] + " ms\t"); //Duration
                Console.Write(waitTimeTable[i] + " ms\t"); //Wait
                Console.Write(finishTimeTable[i] - sortedProcessData[i, 1] + " ms\t"); //Turn around
                Console.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                Console.Write(finishTimeTable[i] + " ms"); //Finish Time
                Console.WriteLine("");
                avgWaitTime = avgWaitTime + waitTimeTable[i];
                avgTurnAroundTime = avgTurnAroundTime + sortedProcessData[i, 2] + waitTimeTable[i];
            }

            Console.WriteLine("Average waiting time: " + avgWaitTime / processesCount + " ms");
            Console.WriteLine("Average turnaround time: " + avgTurnAroundTime / processesCount + " ms");
            Console.WriteLine("Total completion time: " + time + " ms");
            
            Console.WriteLine("");
            Console.Write("Do You want to create report (Y / N) ? ");
            string report = Console.ReadLine();
            if (report == "y" || report == "Y" || report == "yes" || report == "Yes" || report == "YES")
            {
                ReportRR(waitTimeTable, serviceTimeTable, sortedProcessData,time, processesCount,name, gantt,firstDigit,finishTimeTable,quantum);
            }
        }

        public void DisplayData(int[] waitTimeTable, int[] serviceTimeTable, int[,] sortedProcessData,
            double avgWaitTime, double avgTurnAroundTime, int time, int processesCount, string name, string gantt)
        {
            string[] columnNames = {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish"};

            Console.WriteLine("");

            for (int i = 0; i < columnNames.Length; i++)
            {
                Console.Write(columnNames[i] + " ");
            }

            Console.WriteLine("");
            
            for (int i = 0; i < processesCount; i++)
            {
                waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];
                Console.Write(sortedProcessData[i, 0] + "\t"); //PID
                Console.Write(sortedProcessData[i, 1] + " ms\t"); //Arr
                Console.Write(sortedProcessData[i, 2] + " ms\t"); //Dur
                Console.Write(waitTimeTable[i] + " ms\t"); //Wait
                Console.Write(sortedProcessData[i, 2] + waitTimeTable[i] + " ms\t"); //Turn around
                Console.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                Console.Write(serviceTimeTable[i] + sortedProcessData[i, 2] + " ms"); //Finish Time
                Console.WriteLine("");
                avgWaitTime = avgWaitTime + waitTimeTable[i];
                avgTurnAroundTime = avgTurnAroundTime + sortedProcessData[i, 2] + waitTimeTable[i];
            }

            Console.WriteLine("Average waiting time: " + avgWaitTime / processesCount + " ms");
            Console.WriteLine("Average turnaround time: " + avgTurnAroundTime / processesCount + " ms");
            Console.WriteLine("Total completion time: " + time + " ms");

            Console.WriteLine("");
            Console.Write("Do You want to create report (Y / N) ? ");
            string report = Console.ReadLine();
            if (report == "y" || report == "Y" || report == "yes" || report == "Yes" || report == "YES")
            {
                Report(waitTimeTable, serviceTimeTable, sortedProcessData, time, processesCount, name, gantt);
            }
        }

        public void DisplayData(int[] waitTimeTable, int[] serviceTimeTable, int[,] sortedProcessData,
            double avgWaitTime, double avgTurnAroundTime, int time, int processesCount, string type, string name, string gantt)
        {
            string[] columnNames = {"PID  ", "Arrival", "Duration", "Waiting", "Turn   ", "Start   ", "Finish ", "Priority  "};

            Console.WriteLine("");

            for (int i = 0; i < columnNames.Length; i++)
            {
                Console.Write(columnNames[i] + " ");
            }

            Console.WriteLine("");
            
            for (int i = 0; i < processesCount; i++)
            {
                waitTimeTable[i] = serviceTimeTable[i] - sortedProcessData[i, 1];
                Console.Write(sortedProcessData[i, 0] + "\t"); //PID
                Console.Write(sortedProcessData[i, 1] + " ms\t"); //Arr
                Console.Write(sortedProcessData[i, 2] + " ms\t"); //Dur
                Console.Write(waitTimeTable[i] + " ms\t"); //Wait
                Console.Write(sortedProcessData[i, 2] + waitTimeTable[i] + " ms\t"); //Turn around
                Console.Write(serviceTimeTable[i] + " ms\t"); //Start Time
                Console.Write(serviceTimeTable[i] + sortedProcessData[i, 2] + " ms\t"); //Finish Time
                Console.Write(sortedProcessData[i,3]); //Priority
                Console.WriteLine("");
                avgWaitTime = avgWaitTime + waitTimeTable[i];
                avgTurnAroundTime = avgTurnAroundTime + sortedProcessData[i, 2] + waitTimeTable[i];
            }

            Console.WriteLine("Average waiting time: " + avgWaitTime / processesCount + " ms");
            Console.WriteLine("Average turnaround time: " + avgTurnAroundTime / processesCount + " ms");
            Console.WriteLine("Total completion time: " + time + " ms");

            
            Console.WriteLine("");
            Console.Write("Do You want to create report (Y / N) ? ");
            string report = Console.ReadLine();
            if (report == "y" || report == "Y" || report == "yes" || report == "Yes" || report == "YES")
            {
                Report(waitTimeTable, serviceTimeTable, sortedProcessData, time, processesCount, type, name, gantt);
            }
        }
        
        public int AskQuantum()
        {
            string readedValue;
            Console.Write("Set quantum value (only int values): ");
            readedValue = Console.ReadLine();
            if (int.TryParse(readedValue, out int quantum))
            {
                Console.Write(quantum);
                Console.WriteLine("");
                return quantum;
            }
            else
            {
                Console.WriteLine("Value wasn't integer, closing program.");
                Environment.Exit(2);
            }

            return quantum;
        }

        public int[,] ReadFromFile(string filePath)
        {


            if (!File.Exists(filePath))
            {
                Console.WriteLine("File in provided path doesn't exist.");
                Environment.Exit(2);
            }

            string[] inputFile = File.ReadAllLines(filePath);

            int[,] processData = new int[inputFile.Length, 4];

            for (int i = 1; i < inputFile.Length; i++)
            {
                string[] splittedInputFile = inputFile[i].Split(':');

                for (int j = 0; j < 4; j++)
                {
                    processData[i - 1, j] = Convert.ToInt32(splittedInputFile[j]);
                }
            }


            return (processData);
        }

        public int[,] OpenPool()
        {
            Console.WriteLine("Open pool has been selected, you will now enter data manually");
            Console.WriteLine("How many processes do You want to enter?");
            string[] columnNames = {"PID", "Arrival", "Duration", "Priority"};

            string readedProcessesCount = Console.ReadLine();
            string readedValue;
            if (int.TryParse(readedProcessesCount, out int processesCount))
            {
                int[,] processData = new int[processesCount+1, 4];
                for (int i = 1; i < processesCount+1; i++)
                {
                    processData[i-1, 0] = i;
                }
                

                for (int i = 1; i < 4; i++)
                {
                    Console.WriteLine("Insert " + columnNames[i] + " values");

                    for (int j = 0; j < processesCount; j++)
                    {
                        Console.Write("For PID " + processData[j,0] + " :");

                        readedValue = Console.ReadLine();
                        if (int.TryParse(readedValue, out int actualValue))
                        {
                            processData[j, i] = actualValue;
                        }
                        else
                        {
                            Console.WriteLine("Value wasn't integer, closing program.");
                            Environment.Exit(2);
                        }
                    }
                }

                return processData;
            }
            else
            {
                Console.WriteLine("Value wasn't integer, closing program.");
                Environment.Exit(2);
            }

            return new int[,] { };
        }
        
        public void DisplayTable(int[,] processData)
        {

            for (int i = 0; i < processData.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(processData[i, j] + "\t");
                }

                Console.WriteLine(" ");
            }

        }

    }
}
