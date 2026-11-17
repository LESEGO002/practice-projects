using System.Data.SqlClient;

namespace Practice_webApi.backend
{//start of namespace
    public class prac_Getuser
    {//start of class

        public string Get_userDetails(string userName, string userSurname) 
        {//start of method

            //temp varibale to hold
            //the message
            string message = string.Empty;

            //create a connection string to connect to the database
            string connection_string = @"Data Source=(localdb)\prac_api;database=Prac_api;";

            //try and catch block to handle any exceptions that may occur while connecting to the database
            try
            {//start of try block

                //use the using statemet
                using (SqlConnection connect = new SqlConnection(connection_string))
                {//start of using statement

                    int count_User = 0;

                    //open connection
                    connect.Open();

                    //create a query to insert user details into the database
                    string query = @"Select * From User_info Where userName='"+userName+"' And userSurname='"+userSurname+"';";

                    //create a sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    //message
                    message = "User details found.";

                    //run the query
                    run_query.ExecuteNonQuery();



                    //close connection
                    connect.Close();


                }//end of using statement

            }
            catch (Exception error)
            {//start of catch block

                //error message
                message = error.Message;

            }//end of catch block

            return message;
        }

    }//end of class
}//end of namespace
