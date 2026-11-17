using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_marks
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create an ArrayList to store 5 student marks
            ArrayList studentMarks = new ArrayList();

            Console.WriteLine("Enter 5 student marks:");

            // Loop to get 5 marks from the user
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Mark {i + 1}: ");
                string input = Console.ReadLine();

                // Validate input to ensure it's a valid number
                if (double.TryParse(input, out double mark))
                {
                    studentMarks.Add(mark);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    i--; // Decrement counter to retry this mark
                }
            }

            // Calculate total marks
            double total = 0;
            foreach (double mark in studentMarks)
            {
                total += mark;
            }

            // Calculate average marks
            double average = total / studentMarks.Count;

            // Display results
            Console.WriteLine($"\nTotal Marks: {total}");
            Console.WriteLine($"Average Marks: {average:F1}");

            // Keep console window open
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
    

