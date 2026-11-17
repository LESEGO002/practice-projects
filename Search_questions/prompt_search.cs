using Confluent.Kafka;
using System;
using System.Collections;
using System.Configuration;

namespace Search_questions
{//start of namespace
    public class prompt_search
    {//start of class

        //arrays to store answers and what to ignore
        ArrayList answers = new ArrayList();
        ArrayList ignore = new ArrayList();
       // private object ignore;

        //method to search
        public void aibot(string username) 
        {
            //add answers
            answers.Add("Password is 8 charaters long");
            answers.Add("Cyber i don't know");
            answers.Add("heyy ,are you good");

            //what to ignore
            ignore.Add("Hi");
            ignore.Add("you");
            ignore.Add("hey");
            ignore.Add("me");
            ignore.Add("about");
            ignore.Add("tell");

            //start the searching
            string ask = string.Empty;

            //do while loop to keep the app running until the user exit
            do
            {
                //then
                Console.Write(username + " :");
                ask = Console.ReadLine();
                

            } while (exit(ask));
        }//end of method

        //boolean method to check if the user exit or not

        private Boolean exit (string ask)
        {//start of boolean method
            if (ask.ToLower() == "exit")
            {//start of if statement
                //exit the app
                System.Environment.Exit(0);
                return false;

            }
            else
            {
                string[] words = ask.Split(' ');
                return true;
            }

        }//end of boolean method

    }//end of class
}//end of namespace