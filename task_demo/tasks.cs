using System;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace task_demo
{//start  of namespace
    public class tasks
    {//start of class

        //global connection string,with variable declaration
        string connection = @"Data source=(localdb)\task_demo;Database=prog_tasks";


        //creating method to test the connection to the database
        public void test_connection()
        {//start of method


            /*
             * sqlconnection -used to make connection with database
             * SqlCommand -used to execute sql statements
             * SqlDataReader -used to read data collected by,
             *                 the sqlcommand,and show the user
             * 
             */

            //connect to the database
            SqlConnection connect = new SqlConnection(connection);

            //try and catch any error that it will throw
            try
            {
                //open the connection and close the connection
                connect.Open();
                //put the database query and run it
                MessageBox.Show("Connection to database is successful");
                //then close if after you are done
                connect.Close();
            }
            catch (Exception error) 
            {
                MessageBox.Show(error.Message);
            }

        }//end of connection method




        //method to insert or store the tasks

        public void insert_task(string name, string description, string status, string due_date,string priority)
        {
            //create the connection instance
            //SqlConnection connect = new SqlConnection(connection);

            //you must use tyr and catch
           try {//start of try


                //make sure the using is coverd by the try and catch
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    //temp variable to hold the query
                    string query = $"insert into Tasks values('{name}', '{description}', '{status}', '{due_date}', '{priority}');";

                    //create the sqlcommand instance to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);
                    run_query.ExecuteNonQuery();

                    connect.Close();
                }

           }//end of try
           catch (Exception error ) 
           {//start of catch

              MessageBox.Show (error.Message);


           }//end of catch



        }//end of insert task method




        //method to load the tasks from the database
        public void Load_task(ListView view_taskgrid) 
        {//start of load tasks method

      

         //create the connection instance
            SqlConnection connects = new SqlConnection(connection);

            //open the connection
            connects.Open();

            //temp variable, to hold query
            string query = $"select * from Tasks;";

            //create the sqlcommand instance to run the query
            SqlCommand run_query = new SqlCommand( query ,  connects );

            //reading th comand file and executing it
           SqlDataReader data_collect = run_query.ExecuteReader();

            //temp variable for boolean to get the statis of the data found , not found means false but if found means true
            bool data_Found = false;

            //use while to loop and get all the columns
            while ( data_collect.Read() ) 
            {//stat of while loop


                //get the data from the database and store it in a variable
                data_Found = true;


                //get all columns from the database and store it in a variable
                string task_id = data_collect["task_id"].ToString();
                string task_name = data_collect["task_name"].ToString();
                string task_description = data_collect["task_description"].ToString();
                string task_status = data_collect["task_status"].ToString();
                string task_due_date = data_collect["task_due_date"].ToString();
                string task_priority = data_collect["task_Priority"].ToString();
               
                //add the items to the list view
                
               view_taskgrid.Items.Add(task_id +" " + task_name +" with " + task_description + " is due on " + task_status + " and is " + task_due_date +" And the task priority is to" + task_priority);
             

            }

            //close the connection after you are done
            connects.Close();


            //display error message
            if (!data_Found)
            {
                view_taskgrid.Items.Add("Task is not found!!");

            }

        }//end of load tasks method




        public void update_taskStatus(int id)
        {

            SqlConnection connects = new SqlConnection(connection);

            connects.Open();

            //then use sqlcommand to run the query
            //temp variable to hold query
            string query = $"update Tasks set task_status='done' where task_id={id}";

            connects.Close ();
        }





        public void delete_task(int id)
        {

            SqlConnection connects = new SqlConnection(connection);

            connects.Open();

            //then use sqlcommand to run the query
            //temp variable to hold query
            string query = $"delete Tasks set task_status='done' where task_id={id}";

            SqlCommand run_query =new SqlCommand(query, connects);

            run_query.ExecuteNonQuery();

            connects.Close();
        }




    }//end of class
    }//end of namespace}