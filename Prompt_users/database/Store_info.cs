using System.Data.SqlClient;

namespace Prompt_users.database
{//start of namespace
    public class Store_info
    {//start of class

        //connection string to the database
        string message = "";
        string connection = @"data source=(localdb)\RbiProgrammers;database=apiweb;";

        public string Store_Userinfo(string Name,String Surname,int age) 
        {

            try
            {
                using (SqlConnection connect = new SqlConnection(connection)) 
                {
                    connect.Open();

                    string query = "Insert into UserInfo Values ('"+Name+"','"+Surname+"','"+age+"')";

                    SqlCommand run_query = new SqlCommand(query, connect);

                    run_query.ExecuteNonQuery();

                    message="user registered successfully";

                    connect.Close();
                }

            }
            catch(Exception error)
            {

                message = error.Message;
            }
            return message;
        }

    }//end of class
}//end of namespace
