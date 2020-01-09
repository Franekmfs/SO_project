using System;
using System.Net.Security;
using Microsoft.Win32;

namespace SO
{


    //Code 1 error  - program failure
    //Code 2 error - user failure 
    class Program
    {
        public static void Main()
        {
            ProcessData processdata = new ProcessData();
            int[,] processData = new int[,] { };

            Console.WriteLine("Select data input type: ");
                Console.WriteLine("1. Input from file");
                Console.WriteLine("2. Input from keyboard");
                switch (Console.ReadLine())
                {
                    case "1":
                        string filePath = "/Users/franciszekprzewozny/RiderProjects/TESTcsharp/SO/process.txt";
                        Console.WriteLine(
                            "If you want to select different path, input the file localisation now (actual path: " +
                            filePath);
                        /*
                        string userinput = Console.ReadLine();
                        if(!userinput.Equals(ConsoleKey.Enter)) 
                        {
                            filePath = userinput;
                        }
                        //nie do końca łapie że kiedy jest enter to ma przejść dalej
                        */
                        processData = processdata.ReadFromFile(filePath);
                        break;
                    case "2":
                        processData = processdata.OpenPool();

                        
                        //obsługa wprowadzania ręcznego do tablicy (muszę sprawdzić, czy wchodzą też nagłówki do tabeli, chyba nie)
                        
                        break;
                    default:
                        Console.WriteLine("Wrong choose, use only 1 or 2 digits.");
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
                            Console.WriteLine("Wrong choice");
                            break;
                    }

                }

        }

        }


    }

