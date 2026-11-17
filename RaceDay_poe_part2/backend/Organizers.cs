using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace RaceDay_poe_part2.backend
{//strat of namespace 
    public class Organizers
    {//start of class

        //temp global varibles
        string message = "No Organizers found...";
        string connections = @"Data source=(localdb)\raceday_api;database=racedaydb;";

        //method to store the organiser Id & user Id
        public string OrganisersInfo() 
        {
            //try and catch
            try
            {//stat of try
                //use using to avoid errors
                using (SqlConnection connect = new SqlConnection(connections)) 
                {//start of using
                                       
                    int CountUser = 0;
                    //create the query
                   
                    connect.Open();

                    string Query = @"SELECT OrganizerId,User_id FROM Organizers;";

                    SqlCommand runquery = new SqlCommand(Query,connect);
              
                    SqlDataReader READ = runquery.ExecuteReader();

                    if (READ != null)
                    {
                        while (READ.Read()) 
                        {
                            CountUser = 1;
                            message= "Organizer ID: "+READ["OrganizerId"].ToString();
                            message+= ",User ID: "+READ["User_id"].ToString();
                        }
                       

                    }

                    if (CountUser ==0) 
                    {
                        message = "Organizers not found";
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



         public string OrganisersInfo_byId(int OrganizerId) 
        {
            //try and catch
            try
            {//stat of try
                //use using to avoid errors
                using (SqlConnection connect = new SqlConnection(connections)) 
                {//start of using
                    int Count_User = 0;
                     

                    //create the query
                    string Query = @"SELECT OrganizerId FROM Organizers WHERE OrganizerId= '"+OrganizerId+"';";

                    connect.Open();

                    SqlCommand runquery = new SqlCommand(Query,connect);
              
                    SqlDataReader Reader = runquery.ExecuteReader();

                    if (Reader != null)
                    {
                        while (Reader.Read()) 
                        {

                            //get the data per column
                            Count_User = 1;
                            message = "Organizer ID: " + Reader["OrganizerId"].ToString();
                        }
                       

                    }

                    if (Count_User ==0) 
                    {
                        message = "Organizer not found";
                    }
                   
                    connect.Close();
                
                }//end of using

            }//end of try
            catch (Exception error) 
            {//start of catch

                message = error.Message;
            
            }//end of catch


            return message;
        
        }

    }//end of class
}//end of namespace
