using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_questions
{//start of namespace
    public class Program
    {//start of class
        static void Main(string[] args)
        {//start of method
            //creating an instance of the search class
            prompt_search Search = new prompt_search();

            string collected_name = "Lesego";
            Search.aibot(collected_name);

        }//end of method
    }//end of class
}//end of namespace
