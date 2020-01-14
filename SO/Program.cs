using System;
using System.IO;
using System.Reflection;
using static System.Environment;

namespace SO
{
    //Code 0 - Closing signal
    //Code 1 error - program failure
    //Code 2 error - user failure 
    class Program
    {
        public static void Main()
        {
            ProcessData processdata = new ProcessData();
            int[,] processData = new int[,] { };

                PlatformID OS = OSVersion.Platform;
                Console.WriteLine("Select data input type: ");
                Console.WriteLine("1. Input from file");
                Console.WriteLine("2. Input from keyboard");
                switch (Console.ReadLine())
                {
                    case "1":
                        string slash;
                        if (OS == PlatformID.Unix)
                        {
                            slash = @"/";
                        }
                        else
                        {
                            slash = @"\";
                        }
                        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + slash + "process.txt";
                        Console.WriteLine(
                            "If you want to select different path, input the file localisation now (actual path: " +
                            filePath + " )");
                        
                        string userInput = Console.ReadLine();
                        if(!(userInput == "")) 
                        {
                            filePath = userInput;
                        }
                        
                        processData = processdata.ReadFromFile(filePath);
                        break;
                    case "2":
                        processData = processdata.OpenPool();
                        break;
                    default:
                        Console.WriteLine("Wrong choose, use only digits 1 or 2 .");
                        Exit(2);
                        break;
                }
                
            int processesCount = processData.GetLength(0) - 1;

            string[] columnNames = {"PID", "Arrival", "Duration", "Priority"};
                Console.WriteLine("Loaded data:");
                Console.WriteLine("");
                for (int i = 0; i < columnNames.Length; i++)
                {
                    Console.Write(columnNames[i] + " ");
                }

                Console.WriteLine("");

                for (int i = 0; i < processesCount; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write(processData[i, j] + "\t");
                    }

                    Console.WriteLine(" ");
                }


                while (true)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Please choose algorithm:");
                    Console.WriteLine("1. FCFS");
                    Console.WriteLine("2. SJF");
                    Console.WriteLine("3. Round Robin");
                    Console.WriteLine("4. Priority Scheduling with aging");
                    Console.WriteLine("0. Exit");
                    switch (Console.ReadLine())
                    {
                        case "0":
                            Exit(0);
                            break;

                        case "1":
                            Console.WriteLine("Chosen FCFS");
                            Console.WriteLine(" ");
                            Fcfs fcfs = new Fcfs(processesCount);
                            fcfs.Calculate(processData);
                            break;

                        case "2":
                            Console.WriteLine("Chosen SJF");
                            Console.WriteLine(" ");
                            Sjf sjf = new Sjf(processesCount);
                            sjf.Calculate(processData);
                            break;

                        case "3":
                            Console.WriteLine("Chosen RR");
                            Console.WriteLine(" ");
                            Rr rr = new Rr(processesCount);
                            rr.Calculate(processData);
                            break;
                        
                        case "4":
                            Console.WriteLine("Chosen PS");
                            Console.WriteLine(" ");
                            Ps ps = new Ps(processesCount);
                            ps.Calculate(processData);
                            break;

                        default:
                            Console.WriteLine("Wrong choice");
                            break;
                    }
                }
        }
    }
}

