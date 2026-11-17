using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace password_hashing.database
{//start of naamespace
    public class get_userInfo
    {//start of class

        //temp global variable
        string message = "password already exitss...";
        string connectionstring = @"Data source=(localdb)\RbiProgrammers;database=RbiProgrammer;";

        public string get_info(string email,string password) 
        {//start of getinfo

            //sqlconnection to connect to the database
            SqlConnection connector = new SqlConnection(connectionstring);

            //open connection
            connector.Open();

            //Sql query to select details from the database
            string query = "Select password From hash_password where email={"+email+"}";

            SqlCommand run_query = new SqlCommand(query,connector);

            SqlDataReader read = run_query.ExecuteReader();

            if (read != null) 
            {
                message = "User found";
            }

            //close connection
            connector.Close();



            return message;
        }//end of get info

    }//end of class
}//end of namespace
