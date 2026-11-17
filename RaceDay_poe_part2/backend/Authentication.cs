using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace RaceDay_poe_part2.backend
{//start of namespace
    public class Authentication
    {//dstart of class

        //temp variables and connection string
        //string UserRole = string.Empty;
        string message = "Failed to register Participant or organizer";
        string connectionString = @"Data source=(localdb)\raceday_api;Database=racedaydb;";

        public string RegisterUserInfo(string FirstName, string LastName, int age, string Email, string passwordHashed, string UserRole)
        {//start of RegisterInfo

            try
            {//start of try

                //using the using method
                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using 

                    string createdAt = DateTime.Now.ToString();

                    //creating a query to insert user info into the database
                    string query = "INSERT INTO users VALUES('" + FirstName + "','" + LastName + "','" + age + "','" + Email + "','" + passwordHashed + "','" + createdAt + "','" + UserRole + "')";

                    //open connection
                    connect.Open();

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);


                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        //messager to the user
                        message = " Thank you " + FirstName + " you, have been registered successfully";
                    }
                    else
                    {
                        if (UserRole == null)
                        {
                            return message;
                        }
                    }

                    if (UserRole == "Organizer")
                    {
                        string query2 = "INSERT INTO Organizers (User_id) VALUES((SELECT UserId FROM users WHERE Email='" + Email + "'))";

                        SqlCommand storeId = new SqlCommand(query2, connect);

                        storeId.ExecuteNonQuery();


                    } else if (UserRole == "Participant") 
                    {

                        string query3 = "INSERT INTO Participants (User_id) VALUES((SELECT UserId FROM users WHERE Email='" + Email + "'))";

                        SqlCommand store_Id = new SqlCommand(query3, connect);

                        store_Id.ExecuteNonQuery();


                    }

                    //close connection
                    connect.Close();

                }//end of using

            }//end if try
            catch (Exception error)
            { //start of catch

                //assign error message
                message = error.Message;

            }//end of catch


            return message;

        }//end of ResgisterInfo



        public string PasswordCheck(string password)
        {//start of method

            if (password == null || password.Length < 8)
            {
                return "Password must be at least 8 characters long";
            }

            if (password.Contains(" "))
            {
                return "Password must not contain spaces";
            }

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {//start of loop

                if (char.IsUpper(c))
                {
                    hasUpper = true;
                }
                else if (char.IsLower(c))
                {
                    hasLower = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else
                {
                    hasSpecial = true;
                }

            }//end of loop

            if (!hasUpper)
            {
                return "Password must contain an uppercase letter";
            }

            if (!hasLower)
            {
                return "Password must contain a lowercase letter";
            }

            if (!hasDigit)
            {
                return "Password must contain a number";
            }

            if (!hasSpecial)
            {
                return "Password must contain a special character, for example @ # $ !";
            }

            //empty message means the password is fine
            return "";

        }//end of method




        public string LogInInfo(string Email, string passwordHashed)
        {

            string message = string.Empty;

            try
            {
                //sqlconnection to connect to the database
                using (SqlConnection connector = new SqlConnection(connectionString))
                {
                    int countUser = 0;

                    //open connection
                    connector.Open();

                    //Sql query to select details from the database
                    string query = "SELECT  Email,passwordHashed FROM users WHERE Email='" + Email + "';";

                    SqlCommand run_query = new SqlCommand(query, connector);
                    

                    SqlDataReader read = run_query.ExecuteReader();


                    
                     if (read != null)
                     {

                        while (read.Read())
                        {
                        
                            countUser = 1;
                            string storedHash = read["passwordHashed"].ToString();

                            if (BCrypt.Net.BCrypt.Verify(passwordHashed, storedHash))
                            {
                                message = "Logged in successfully...";
                                
                            }
                            else 
                            {
                                message = "Login Unsucessful...";
                            }

                        }
                        //verify the typed password against the stored hash
                        
                    }                                         
                    //close connection
                    connector.Close();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;

        }//end of login method


        public string GetUserRole(string Email)
        {//start of method

            string role = "Participant";
            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using

                    connect.Open();
                    string query = "SELECT UserRole FROM users WHERE Email='" + Email + "'";
                    SqlCommand run_query = new SqlCommand(query, connect);
                    SqlDataReader read = run_query.ExecuteReader();

                    if (read.Read())
                    {
                        role = read["UserRole"].ToString();
                    }

                    connect.Close();

                }//end of using

            }//end of try
            catch (Exception error)
            {//start of catch

                role = error.Message;

            }//end of catch

            return role;

        }//end of method

    }//end of class
}//end of method
