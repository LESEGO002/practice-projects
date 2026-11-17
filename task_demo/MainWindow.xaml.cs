using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task_demo
{//start of namespace

    public partial class MainWindow : Window
    {//start of class


        //crate an instance of the class tasks
        //with and object name of manage_tasks
        tasks manage_tasks = new tasks();

        //global variables to hold task details
        string task_name, task_description,task_status,task_due_date, task_priority = string.Empty;

        public MainWindow()
        {//start of constructor

            InitializeComponent();

            //call the testing method for the connection
            manage_tasks.test_connection();

        }//end of constructor


        //event handler for submiting the tasks to the database
        private void Submit_task(Object sender, RoutedEventArgs e)
        { //start of event hander submit

            //collect the user input
            //what you collect from the textboc will always be collected as a string
            //string datatype amnd if it is a number then use int/parse()

            //temp vairable
            string user_input = question_box.Text.ToString();

            //this past from here, add it before it sesarch for the answer
            //make it be the first line in that method
            //check ifthe user is adding the task or just asking aquestion

            if (user_input.ToLower().StartsWith("add task"))
            {//start of if add task

                //add the task to the listview as part of the chats
                tasks.Items.Add("Great your task is added,would you like a reminder?");

                //then filter to get task name
                task_name = user_input.Replace("add task", " ");

                task_description = "assign to what the ai retun when sesarching for the answer";

            }//end of if add task


            if (user_input.ToLower().StartsWith("yes, remind me in"))
            {


                //replace the yes in reminde me in
                string reminder =user_input.Replace("yes, remind me in","");

                string days_number = Regex.Replace(reminder, @"[^0-9]","");
                

                //cast the day nubmer to an int 
                int days = int.Parse(days_number);

                //add the days  the user chose to do the task current daste
                DateTime user_reminder = DateTime.Now.AddDays(days);

                //format the date how it should be
                //like this 2024-06-30
                string format_date = user_reminder.ToString("MMM dd yyyy");
                //assigign
                task_due_date = format_date;
                task_status = "pending";
                task_priority = "update";

                //call the instert method 
               tasks.Items.Add($"Great, I will remind you in " + days + " days to do the task on " + format_date );


                //then insert the task to the database
                manage_tasks.insert_task( task_name, task_description, task_status,  task_due_date,  task_priority);

            }//end reminder if


        }//end of event handler submit





        //manage task button event handler
        private void Manage_task(object sender, RoutedEventArgs e) 
        { //start of event handler manage task

            //is task done or not
            /*
             * 1:check if task is done
             * 2:if done then update the status to done
             *  3 if the task contain marked done before , then delete it
             *  4:reload the task listview to show the updated tasks
             *  
             */
            
            //get the selected value
            string get_selected_value = view_taskgrid.SelectedValue.ToString();
            //get ID using sub string from 0-1 
            string get_Id = get_selected_value.Substring(0,1);
            //then cast the string getId to an int
            int id = int.Parse(get_Id);

            //chech if the selected task end woth done
            if (get_selected_value.ToLower().EndsWith("done"))
            {//start of if
                manage_tasks.delete_task(id);



            }//end of if
            else 
            {//start of if
            
                //mark it done since it ends with pending not done
                manage_tasks.update_taskStatus(id);



            }//end of if

            //recall the auto loat method
            autoload_task();

            }//end of event handler manage task

       
        private void Back_to_chats(object sender, RoutedEventArgs e)
            { //start of event handler back to chats

            task_grid.Visibility = Visibility.Visible;
             viewtasks_grid.Visibility = Visibility.Hidden;


            }//end of event handler back to chats



        private void view_alltasks(object sender, RoutedEventArgs e) 
        {
         
            viewtasks_grid.Visibility = Visibility.Visible;
            task_grid.Visibility = Visibility.Hidden;

            autoload_task();
        }



        private void autoload_task() 
             {
            //clear the list view first
            view_taskgrid.Items.Clear();

            //use the object name to manage task
            manage_tasks.Load_task(view_taskgrid);
        
            }

    }//end of class
}//end of namespace
