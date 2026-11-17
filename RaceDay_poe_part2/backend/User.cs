using BCrypt.Net;
using System.Data.SqlClient;

namespace RaceDay_poe_part2.backend
{//Start of namespace
    public class User
    {//start of class

        //temp variables and connection string
       
        string message = string.Empty;
        string connectionString = @"Data source=(localdb)\raceday_api;Database=racedaydb;";

        public string users(int UserId) 
        {//start of class

            //try and catch block to catch errors
            try
            {//start of try

                

                //use using 
                using (SqlConnection connect = new SqlConnection(connectionString)) 
                {//start of using

                    int countUser = 0;

                    connect.Open();

                    //create the query to get the users by Id
                    string query = @"SELECT * FROM users WHERE UserId='"+ UserId +"';";

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query,connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start ofwhile loop


                            //get the data per column
                            countUser = 1;
                            message = reader["UserId"].ToString();
                            message += ",FirstName: " + reader["FirstName"].ToString();
                            message += ",LastName: " + reader["LastName"].ToString();
                            message += ",age: " + reader["age"].ToString();
                            message += ",Email: " + reader["Email"].ToString();
                            message += ",password: " + reader["passwordHashed"].ToString();
                            message += ",Created At: " + reader["createdAt"].ToString();
                            message += ",Role role: " + reader["UserRole"].ToString();
                          



                        }//end of while loop

                    }//end of if

                    if (countUser == 0)
                    {//start of else

                        //Assign error message if user not found
                        message = "User not found";

                    }//end of else

                    connect.Close();

                }//end of using

            }//end of try
            catch 
            (Exception error)
            {//start of catch

                message = error.Message;
            
            }//end of catch

            return message;
        }//end of method


        public string update_byId(int UserId,string FirstName, string LastName, int age, string Email, string passwordHashed, string UserRole) 
        {
            string result_message = "failed to update...";
            string createdAt = DateTime.Now.ToString();

            try
            {
                SqlConnection connect = new SqlConnection(connectionString);


                string Query = "Update users set FirstName = '" + FirstName + "', LastName = '" + LastName + "', age = '" + age + "'," +
                    " Email= '"+Email+"',passwordHashed= '"+passwordHashed+"',createdAt= '"+createdAt+"',UserRole='"+UserRole+"' WHERE UserId = " + UserId + ";";

                connect.Open();


                SqlCommand Run_query = new SqlCommand(Query, connect);


                int result = Run_query.ExecuteNonQuery();

                if (result > 0)
                {
                    result_message = "Participant or Organizer updated successfully.";
                }

                connect.Close();
            }
            catch (Exception error)
            {
                result_message = error.Message;
            }

            return result_message;

        }//end of  get method




        public string DeleteEvent(int UserId)
        {//start of method

            string message = "Event could not be deleted...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using

                    connect.Open();

                    //only delete if the event belongs to the organizer with this email
                    string query = $"DELETE FROM users WHERE UserId=" + UserId + ";";

                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "User deleted successfully";
                    }
                    else
                    {
                        message = "User not found, or user does not exixt...";
                    }

                    connect.Close();

                }//end of using

            }//end of try
            catch (Exception error)
            {//start of catch

                message = error.Message;

            }//end of catch

            return message;

        }//end of method


    }//end of class
}//end of namespace
