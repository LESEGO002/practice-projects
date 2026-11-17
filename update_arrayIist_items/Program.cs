using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace update_arrayIist_items
{//start of namespace
    public class Program
    {//start of class
        static void Main(string[] args)
        {//start of main method

            //instance of an arraylist
            ArrayList st_numbers = new ArrayList();

            //adding items to arraylist
            st_numbers.Add("ST_150050");
            st_numbers.Add("ST_235500");

            //display the items
            Console.WriteLine(st_numbers[0]);
            Console.WriteLine(st_numbers[1]);

            //updateing the items
            Console.WriteLine("Updated Student Information");

            //Update index 1 
            st_numbers[1] = "Lesego Mtshixa";

            //display the updated items
            Console.WriteLine(st_numbers[0]);
            Console.WriteLine(st_numbers[1]);


        }//end of main method
    }//end of class
}//end of namespace
