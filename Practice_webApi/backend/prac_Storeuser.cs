using System.Data.SqlClient;

namespace Practice_webApi.backend
{//start of namespace
    public class prac_Storeuser
    {//start of class

        //method to store user details in the backend database
        //store user details in the database
        public string store_user_details(string userName,string userSurname,int userAge)
        {//start of method 

            //create a connection string to connect to the database
            string connection_string = @"Data Source=(localdb)\prac_api;database=Prac_api;";

            string message = "";

            //try and catch block to handle any exceptions that may occur while connecting to the database
            try
            {//start of try block

                //use the using statemet
                using (SqlConnection connect =new SqlConnection(connection_string)) 
                {//start of using statement

                    //open connection
                    connect.Open();

                    //create a query to insert user details into the database
                    string query = @"Insert Into User_info Values('"+userName+"','"+userSurname+"','"+userAge+"');";

                    //create a sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query,connect);

                    //message
                    message = "User details stored successfully.";

                    //run the query
                    run_query.ExecuteNonQuery();

                    

                    //close connection
                    connect.Close();


                }//end of using statement

            } catch (Exception error) 
            {//start of catch block

                //error message
                message = error.Message;
            
            }//end of catch block

            return message;
        }//end of method

        
    }//end of class
}//end of namespace
