using RaceDay_poe_part2.models;
using System.Data.SqlClient;
using static RaceDay_poe_part2.Controllers.CategoriesController;

namespace RaceDay_poe_part2.backend
{
    public class Categories
    {
        string message = "Faield to add category....";

        string connectionString = @"Data source=(localdb)\raceday_api;database=racedaydb;";

        public string CategoriesInfo(string CatergoryName, string CategoryDescrition, int MinAge, int MaxAge,string OrganizerEmail)
        {//start of RegisterInfo

            try
            {//start of try

                //using the using method
                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using 

                   
                    //creating a query to insert user info into the database
                    string query = "INSERT INTO Catergories VALUES('" + CatergoryName + "','" + CategoryDescrition + "','" + MinAge + "','" + MaxAge + "',(SELECT o.OrganizerId FROM Organizers o JOIN Users u ON u.UserId = o.User_id WHERE u.Email='" + OrganizerEmail + "'))";

                    //open connection
                    connect.Open();

                    SqlCommand check_organizer = new SqlCommand("SELECT o.OrganizerId FROM Organizers o JOIN users u ON u.UserId = o.User_id WHERE u.Email = @email", connect);
                    check_organizer.Parameters.AddWithValue("@email", OrganizerEmail);
                    object organizer_id = check_organizer.ExecuteScalar();

                    //a participant has no organizer row so stop here
                    if (organizer_id == null)
                    {
                        return "Only organizers can add categories.";
                    }

                    //then save the category using (int)organizer_id for the organizerID column

                    //only organizers can create events
                    string checkOrganizer = "SELECT TOP 1 o.OrganizerId FROM Organizers o INNER JOIN users u ON u.UserId = o.User_id WHERE u.Email='" + OrganizerEmail + "'";

                    SqlCommand run_check = new SqlCommand(checkOrganizer, connect);

                    if (run_check.ExecuteScalar() == null)
                    {
                        return "Only organizers can create events";
                    }

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);


                    int result = run_query.ExecuteNonQuery();



                    if (result > 0)
                    {
                        //messager to the user
                        message = " Thank you " + " your, "+ CatergoryName +" is added...";
                    }
                    else
                    {
                        if (CatergoryName == null)
                        {
                            return message;
                        }
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
        }//end of CategoryMethod




        public string CateGoryInfo()
        {//start of class


            string message = "Category is found...";


            //try and catch block to catch errors
            try
            {//start of try



                //use using 
                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using

                    int countUser = 0;

                    connect.Open();

                    //create the query to get the users by Id
                    string query = @"SELECT CatergoryId,CatergoryName, CategoryDescrition, MinAge, MaxAge, OrganizerID FROM Catergories;";

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start ofwhile loop


                            //get the data per column
                            countUser = 1;
                            message ="CategoryId: " + reader["CatergoryId"].ToString();
                            message += ",CategoryName: " + reader["CatergoryName"].ToString();
                            message += ",CateDes: " + reader["CategoryDescrition"].ToString();
                            message += ",MinAge: " + reader["MinAge"].ToString();
                            message += ",MaxAge: " + reader["MaxAge"].ToString();
                            message += ",OrganizerID: " + reader["OrganizerID"].ToString();
                            

                        }//end of while loop

                    }//end of if

                    if (countUser == 0)
                    {//start of else

                        //Assign error message if user not found
                        message = "Category not found";

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




        public string CateGoryInfo_Id( int CatergoryId)
        {//start of class


            string message = "Category is found...";


            //try and catch block to catch errors
            try
            {//start of try



                //use using 
                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using

                    int countUser = 0;

                    connect.Open();

                    //create the query to get the users by Id
                    string query = @"SELECT CatergoryId,CatergoryName, CategoryDescrition, MinAge, MaxAge, OrganizerID FROM Catergories WHERE CatergoryId= '"+CatergoryId+"';";

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start ofwhile loop


                            //get the data per column
                            countUser = 1;
                            message = "CategoryId: " + reader["CatergoryId"].ToString();
                            message += ",CategoryName: " + reader["CatergoryName"].ToString();
                            message += ",CateDes: " + reader["CategoryDescrition"].ToString();
                            message += ",MinAge: " + reader["MinAge"].ToString();
                            message += ",MaxAge: " + reader["MaxAge"].ToString();
                            message += ",OrganizerID: " + reader["OrganizerID"].ToString();


                        }//end of while loop

                    }//end of if

                    if (countUser == 0)
                    {//start of else

                        //Assign error message if user not found
                        message = "Category not found";

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



        public string Catergoryupdate_byId(int CatergoryId,string CatergoryName, string CategoryDescrition, int MinAge, int MaxAge)
        {
            string result_message = "failed to update Category...";
            string createdAt = DateTime.Now.ToString();

            try
            {
                SqlConnection connect = new SqlConnection(connectionString);


                string Query = "Update Catergories set CatergoryName = '" + CatergoryName + "',  CategoryDescrition= '" + CategoryDescrition + "', MinAge = '" + MinAge + "'," +
                    " MaxAge= '" + MaxAge + "' WHERE CatergoryId = " + CatergoryId + ";";

                connect.Open();


                SqlCommand Run_query = new SqlCommand(Query, connect);


                int result = Run_query.ExecuteNonQuery();

                if (result > 0)
                {
                    result_message = "Category updated successfully.";
                }

                connect.Close();
            }
            catch (Exception error)
            {
                result_message = error.Message;
            }

            return result_message;

        }//end of  get method



        //delete method
        public string DeleteCategory_byId(int CatergoryId )
        {//start of method

            string message = "Catergory could not be deleted...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using

                    connect.Open();

                    //first remove this category from any events that use it
                    string clearEvents = "UPDATE Events SET Category_id = NULL WHERE Category_id = " + CatergoryId + ";";

                    SqlCommand run_clear = new SqlCommand(clearEvents, connect);
                    run_clear.ExecuteNonQuery();


                    string deleteCatergory = $"DELETE FROM Catergories WHERE CatergoryId = " + CatergoryId + ";"; ;

                    SqlCommand run_Catergories = new SqlCommand(deleteCatergory, connect);
                    int result = run_Catergories.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "category deleted successfully";
                    }
                    else
                    {
                        message = "Catergory not found, or you are not the organizer of this Catergory";
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


        public List<CatergoriesModel> GetAllCategoriesJson()
        {//start of method

            List<CatergoriesModel> categories = new List<CatergoriesModel>();

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connectionString))
                {//start of using

                    string query = "SELECT CatergoryId, CatergoryName, CategoryDescrition, MinAge, MaxAge, OrganizerID FROM Catergories;";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);
                    SqlDataReader reader = run_query.ExecuteReader();

                    while (reader.Read())
                    {//start of while
                        CatergoriesModel cat = new CatergoriesModel();
                        cat.CatergoryId = (int)reader["CatergoryId"];
                        cat.CategoryName = reader["CatergoryName"].ToString() ?? "";
                        cat.CategoryDescrition = reader["CategoryDescrition"].ToString() ?? "";
                        cat.MinAge = (int)reader["MinAge"];
                        cat.MaxAge = (int)reader["MaxAge"];
                        cat.organizerID = reader["OrganizerID"] == DBNull.Value ? 0 :(int)reader["OrganizerID"];
                        categories.Add(cat);
                    }//end of while

                    connect.Close();

                }//end of using

            }//end of try
            catch (Exception error) 
            {
                Console.WriteLine("GetAllCategoriesJson error" + error.Message);
            
            }//end of catch

            return categories;

        }//end of method
    }
}