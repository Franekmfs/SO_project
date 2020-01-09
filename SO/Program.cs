using System;

namespace SO
{
    class Program
    {
        public static void Main()
        {
            
            string[] inputFile = System.IO.File.ReadAllLines("/Users/franciszekprzewozny/RiderProjects/TESTcsharp/SO/process.txt");
            string[] columnNames = {"PID", "Arrival", "Duration", "Priority"};
            int[,] processData = new int[inputFile.Length, 4];
            int processesCount = inputFile.Length - 1;
           
           for (int i = 1; i < inputFile.Length; i++)
           {
               string[] splittedInputFile = inputFile[i].Split(':');
               
               for (int j = 0; j < 4; j++)
               {
                   processData[i-1,j] = Convert.ToInt32(splittedInputFile[j]);
               }
           }



           Console.WriteLine("Loaded data:");
           Console.WriteLine("");
           for (int i = 0; i < columnNames.Length; i++)
           {
               Console.Write(columnNames[i] + " ");
           }
           Console.WriteLine("");



           for (int i = 0; i < inputFile.Length-1; i++)
           {
               for (int j = 0; j < 4; j++)
               {
                   Console.Write(processData[i,j] + "\t");
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
               Console.WriteLine("0. Exit");
               switch (Console.ReadLine())
               {
                   case "0":
                       Environment.Exit(1);
                       break;
                   
                   case "1":
                       Console.WriteLine("Choosed FCFS");
                       Console.WriteLine(" ");
                       Fcfs fcfs = new Fcfs(processesCount);
                       fcfs.Calculate(processData);
                       break;
                   
                   case "2":
                       Console.WriteLine("Choosed SJF");
                       Console.WriteLine(" ");
                        Sjf sjf = new Sjf(processesCount);
                       sjf.Calculate(processData);
                       break;
                   
                   case "3":
                       Console.WriteLine("Choosed RR");
                       Console.WriteLine(" ");
                       Rr rr = new Rr(processesCount);
                       rr.Calculate(processData);
                       break;
                   
                   default:
                       Console.WriteLine("Błędny wybor");
                       break;
               }

           }
           
        }
        
    }
    
  
}
