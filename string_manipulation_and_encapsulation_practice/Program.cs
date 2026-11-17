using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;

namespace string_manipulation_and_encapsulation_practice
{//start of namespace
    public class Program
    {//start of class
        static void Main(string[] args)
        {//start of main method

            call calling = new call();

            //diplaying the caller information
            Console.WriteLine(calling.return_caller_info());

        }//end of main method
    }//end of class
}//end of namespace
