using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace L1_conccurent_programming
{

    class Program
    {
       


        
        public static void write2Monitor(string[] Array, Dmonitor d)

        {
            if (Array.Length != 0)
            {

                foreach (string s in Array)
                {
                    string[] lines = s.Split(',');
                    string name = lines[0];
                    int number = int.Parse(lines[1]);
                    double fees = double.Parse(lines[2]);
                    field r = new field(name, number, fees);
                    d.AddItem(r);


                }
                

            }



        }



       




        public static void conditon(Dmonitor d, Rmonitor r )
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            string threadName = Thread.CurrentThread.Name;
            int n = d.GetSize();
           
           // Console.WriteLine("Size of the monitor : {0} " , n);

            for (int i = 0; i < n; i++)
            {
                try
                {
                    double grade = 3;
                    var fieldObject = d.Get(i);
                    if (fieldObject != null && fieldObject.GetGpa() > grade)
                    {
                        // Your logic here
                    
                    
                        field s = d.Removeitem(i);
                        if (s != null)
                        {
                            Console.WriteLine($"Thread ID {threadId} : "+"Student : {0} has been removed from monitor and added to reuslt ",s.ToString());
                            Console.WriteLine("");
                            r.Add(s);
                           
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Thread ID {threadId}, Name '{threadName}' Doesnt fullfill requirements: {ex.Message}");
                   
                }
            }
        }

        // ... (rest of your code)





        static string[] ReadDataFromFile(string filePath)
        {
            try
            {
                // Read all lines from the file and store them in a string array
                string[] lines = File.ReadAllLines(filePath);


                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new string[0]; // Return an empty array in case of an error
            }
        }

        static void WriteStudentTableToFile(Rmonitor students, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Name                Credits    GPA");
                    writer.WriteLine("--------------------------------------");

                    for (int i = 0; i < students.GetSize(); i++)
                    {
                        writer.WriteLine(students.Get(i).ToString());
                    }

                }

                Console.WriteLine($"Data has been written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
           // Console.Write("Enter the name of the text file (including extension, e.g., myfile.txt): ");
            string filePath = "f1.txt";

            string[] dataArray = ReadDataFromFile(filePath);
            int Dmonitorsize = (int)Math.Round(dataArray.Length * 0.5);
            int max = 300;
            Dmonitor d = new Dmonitor(Dmonitorsize);
            Rmonitor r = new Rmonitor(max);
            Console.WriteLine(Dmonitorsize + "Here is the size ");



            Thread[] threads = new Thread[5];


            // Create and start seven threads
            for (int i = 0; i < 5; i++)
            {



                threads[i] = new Thread(() => conditon(d, r));
                threads[i].Start();



                Console.WriteLine("thread number : {0}", i);

            }
            write2Monitor(dataArray, d);

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine("====================================================");
            Console.WriteLine("Size of the result monitor is :{0} ",r.GetSize());
            WriteStudentTableToFile(r, "output.txt");









            // Wait for all seven threads to finish(optional)




            // Rest of your program...







            Console.ReadLine();



        }
    }


    
}
