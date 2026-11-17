using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace password_hashing.database
{//start of namespace
    public class store_info
    {//start of class

        //temp variable to hold message
        string message = "Failed to register a user...";
        //connection string to the database
        string connections = @"Data source=(localdb)\RbiProgrammers;database=RbiProgrammer;";
        string Role = "User";

        //method to store user's info
        public string store_userinfo(string email,string password) 
        {//start of method
            try
            {//start of try

                //creating instance for the 
                //classs sqlconnection
                //object name connect
                SqlConnection connect = new SqlConnection( connections );
               
                //open connection
                connect.Open();

                 //create query to insert into the password table
                 string query = @"INSERT INTO hash_password VALUES('"+Role+"','"+email+"','"+password+"');";

                //run the query and execute
                SqlCommand run_query = new SqlCommand( query,connect );

                //execute non query
                int result = run_query.ExecuteNonQuery();

                //check if the rows were affected
                if (result > 0)
                {//start of if

                    message = "User sucessfully registered...";

                }//end of if
                else 
                {//start of else
                    message = "Failed to register user...";
                
                }//end of else

                //close connection
                connect.Close();

            }//end of try
            catch (Exception error) 
            {//start of catch

                //assign to error message
                message = error.Message;
            
            }//end of catch
            return message;
        }//end of method

     
    }//end of class

}//end of namespace
