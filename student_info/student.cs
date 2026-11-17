using System;

namespace student_info
{//start of namespace
    public class student
    {//start of class
        private string Student_name = "";
        private string Course_name = "";
        private string Student_number = "";

        //method
        public void student_info(){

            //prompting the user
            Console.Write("Enter student name:");
            string student_name = Console.ReadLine();

            Console.Write("Enter course name:");
            string Course_name = Console.ReadLine();

            Console.Write("Enter Student number:");
            string Student_number = Console.ReadLine();

            Console.WriteLine(" ");

            Console.WriteLine("--- Student Details ---");

                //string manipulation 

            
            
        }
        public string return_student_info() {
            string uppercase = student.studentinfo.Toupper();
            string lowercase = student.studentinfo.Tolower();

            return;
        }


    }//end of class
}//end of class