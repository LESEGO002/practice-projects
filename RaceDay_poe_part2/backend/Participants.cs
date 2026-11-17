using System.Data.SqlClient;

namespace RaceDay_poe_part2.backend
{//start of namespace
    public class Participants
    {//start of class

        //temp global varibles
        string message = "No Organizers found...";
        string connections = @"Data source=(localdb)\raceday_api;database=racedaydb;";

        //method to store the organiser Id & user Id
        public string ParticipantsInfo()
        {
            //try and catch
            try
            {//stat of try
                //use using to avoid errors
                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using
                    int CountUser = 0;


                    //create the query
                    string Query = @"SELECT ParticipantId,event_id,User_id FROM Participants;";

                    connect.Open();

                    SqlCommand runquery = new SqlCommand(Query, connect);

                    SqlDataReader READ = runquery.ExecuteReader();

                    if (READ != null)
                    {
                        while (READ.Read())
                        {
                            CountUser = 1;
                            message = "Participant ID: " + READ["ParticipantId"].ToString();
                            message += ",Event ID: " + READ["event_id"].ToString();
                            message += ",User ID: " + READ["User_id"].ToString();

                            //message = "Particpants found...";
                        }


                    }

                    if (CountUser == 0)
                    {
                        message = "Participants not found";
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



        public string ParticipantInfo_byId(int ParticipantId)
        {//start of method
            //try and catch
            try
            {//stat of try
                //use using to avoid errors
                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using
                    int Count_User = 0;


                    //create the query
                    string Query = @"SELECT ParticipantId,event_id,User_id FROM Participants WHERE ParticipantId= '" + ParticipantId + "';";

                    connect.Open();

                    SqlCommand runquery = new SqlCommand(Query, connect);

                    SqlDataReader Reader = runquery.ExecuteReader();

                    if (Reader != null)
                    {
                        while (Reader.Read())
                        {

                            //get the data per column
                            Count_User = 1;
                            message = "Particiopant ID: " + Reader["ParticipantId"].ToString();
                            message += ",Event ID: " + Reader["event_id"].ToString();
                            message += ",User ID: " + Reader["User_id"].ToString();
                        }


                    }

                    if (Count_User == 0)
                    {
                        message = "Participant not found";
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

}//end od namespace
