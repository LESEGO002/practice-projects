using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RaceDay_poe_part2.models;
using System.ComponentModel;
using System.Data.SqlClient;

namespace RaceDay_poe_part2.backend
{//start of namespace
    public class Events
    {//start of class

        //temp global varibles
        string message = "Event could not be created found...";
        string connections = @"Data source=(localdb)\raceday_api;database=racedaydb;";

        public string CreateEvents(string EventName, string EventDescription,DateTime EventDate, string EventLocation, int MaxParticipants,string Eventstatus,string organizerEmail, string categoryName) 
        {//start of method

            //try amd catch block
            try
            {

                using(SqlConnection connect = new SqlConnection(connections))
                {//start of using 

                    //creating a query to insert user info into the database
                    //string query = "INSERT INTO Events VALUES('" + EventName + "','" + EventDescription + "','" + EventDate + "','" + EventLocation + "','" + MaxParticipants + "','" + Eventstatus + "','" + createdAt + "');";

                   
                    //open connection
                    connect.Open();

                    string query2 = "SELECT o.OrganizerId, o.User_id, u.Email FROM Organizers o JOIN Users u ON u.UserId = o.User_id WHERE u.Email='" + organizerEmail + "';";

                    //sqlcommand to run the query
                    SqlCommand getOrganizer = new SqlCommand(query2, connect);

                    object organizerResult = getOrganizer.ExecuteScalar();

                    if (organizerResult == null)
                    {
                        message = "Organizer not found...";
                        return message;
                    }

                    string query3 = "SELECT CatergoryId FROM Catergories WHERE CatergoryName='" + categoryName + "';";

                    //sqlcommand to run the query
                    SqlCommand getCategory = new SqlCommand(query3, connect);

                    object categoryResult = getCategory.ExecuteScalar();

                    if (categoryResult == null)
                    {
                        message = "Category not found...";
                        return message;
                    }

                    string createdAt = DateTime.Now.ToString();
                    //string query = "INSERT INTO Events VALUES('" + EventName + "','" + EventDescription + "','" + EventDate + "','" + EventLocation + "','" + MaxParticipants + "','" + Eventstatus + "','" + createdAt + "','" + organizerResult + "','" + categoryResult + "');";
                    string query = "INSERT INTO Events (EventName, Eventdescription, EventDate, EventLocation, MaxParticipants, Eventstatus,createdAt, Organiser_id, Category_id) VALUES('" + EventName + "','" + EventDescription + "','" + EventDate + "','" + EventLocation + "','" + MaxParticipants + "','" + Eventstatus + "','"+createdAt+"','" + organizerResult + "','" + categoryResult + "');";

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                   

                    if (result > 0)
                    {
                        //messager to the user
                        message = " Thank you, your" + EventName + "  has been created successfully";
                    }
                    else
                    {
                        if (EventName == null)
                        {
                            return message;
                        }
                    }

                    //if (EventName != null)
                    //{
                      //  string query1 = "INSERT INTO Participants (event_id) VALUES((SELECT EventId FROM Events WHERE EventName='" + EventName + "'));";

                        //SqlCommand storeId = new SqlCommand(query1, connect);

                        //storeId.ExecuteNonQuery();


                   // }
                }//end of using

            }
            catch (Exception error) 
            {
                message = error.Message;
            
            }

            return message;
        }//emnd of method





        public string EventbyID(int EventId)
        {//start of class


            string message = "Event is found...";


            //try and catch block to catch errors
            try
            {//start of try

              

                //use using 
                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    int countUser = 0;

                    connect.Open();

                    //create the query to get the users by Id
                    string query = @"SELECT EventId,EventName, Eventdescription, EventDate, EventLocation, MaxParticipants, Eventstatus, createdAt, Organiser_id, Category_id FROM Events WHERE EventId='" + EventId + "';";

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start ofwhile loop


                            //get the data per column
                            countUser = 1;
                            message = "EventId: "+reader["EventId"].ToString();
                            message +=",EventName: "+ reader["EventName"].ToString();
                            message += ",EvenDes: "+reader["Eventdescription"].ToString();
                            message += ",EventDate: "+reader["EventDate"].ToString();
                            message += ",EventLocation: "+reader["EventLocation"].ToString();
                            message += ",Max participants: "+reader["MaxParticipants"].ToString();
                            message += ",Event Status: "+reader["EventStatus"].ToString().Trim();
                            message += ",Created At: "+reader["createdAt"].ToString();
                            message += ",Org Id: "+reader["Organiser_id"].ToString();
                            message += ",Cate Id: "+reader["Category_id"].ToString();

                        }//end of while loop

                    }//end of if

                    if (countUser == 0)
                    {//start of else

                        //Assign error message if user not found
                        message = "Event not found";

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







        //update method
        public string UpdateEvent(int EventId, string Email, string EventName, string EventDescription, DateTime EventDate, string EventLocation, int MaxParticipants, string Eventstatus)
        {//start of method

            string message = "Event could not be updated...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    connect.Open();

                    string query = "UPDATE Events SET EventName='" + EventName + "', Eventdescription='" + EventDescription +
                                   "', EventDate='" + EventDate + "', EventLocation='" + EventLocation +
                                   "', MaxParticipants=" + MaxParticipants + ", Eventstatus='" + Eventstatus +
                                   "' WHERE EventId=" + EventId +
                                   " AND Organiser_id = (SELECT o.OrganizerId FROM Organizers o INNER JOIN users u ON u.UserId = o.User_id WHERE u.Email='" + Email + "')";

                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "Event updated successfully";
                    }
                    else
                    {
                        message = "Event not found, or you are not the organizer of this event";
                    }

                    connect.Close();

                }//end of using

            }//end of try
            catch (Exception error)
            {//start of catch

                message = error.Message;

            }//end of catch

            return message;

        }//end of update method





        //delete method
        public string DeleteEvent(int EventId, string Email)
        {//start of method

            string message = "Event could not be deleted...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    connect.Open();

                    string deleteParticipants = $"DELETE FROM Participants WHERE event_id = " + EventId +
                    " AND event_id IN (SELECT EventId FROM Events WHERE EventId=" + EventId +
                    " AND Organiser_id = (SELECT o.OrganizerId FROM Organizers o INNER JOIN users u ON u.UserId = o.User_id WHERE u.Email='" + Email + "'));";

                    SqlCommand run_participants = new SqlCommand(deleteParticipants, connect);
                    run_participants.ExecuteNonQuery();


                    //only delete if the event belongs to the organizer with this email
                    string query = $"DELETE FROM Events WHERE EventId=" + EventId +" AND Organiser_id = (SELECT o.OrganizerId FROM Organizers o INNER JOIN users u ON u.UserId = o.User_id WHERE u.Email='" + Email + "');";

                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "Event deleted successfully";
                    }
                    else
                    {
                        message = "Event not found, or you are not the organizer of this event";
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


        public List<EventsModel> GetAllEventsJson()
        {//start of method

            List<EventsModel> events = new List<EventsModel>();

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    string query = "SELECT EventId, EventName, Eventdescription, EventDate, EventLocation, MaxParticipants, Eventstatus, createdAt, Organiser_id, Category_id FROM Events;";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);
                    SqlDataReader reader = run_query.ExecuteReader();

                    while (reader.Read())
                    {//start of while
                        EventsModel ev = new EventsModel();
                        ev.EventId = (int)reader["EventId"];
                        ev.EventName = reader["EventName"].ToString() ?? "";
                        ev.Eventdescription = reader["Eventdescription"].ToString() ?? "";
                        ev.EventDate = Convert.ToDateTime(reader["EventDate"]);
                        ev.EventLocation = reader["EventLocation"].ToString() ?? "";
                        ev.MaxParticipants = (int)reader["MaxParticipants"];
                        ev.Eventstatus = reader["Eventstatus"].ToString() ?? "";
                        ev.createdAt = reader["createdAt"].ToString() ?? "";
                        ev.Organiser_id = reader["Organiser_id"] == DBNull.Value ? 0 : (int)reader["Organiser_id"];
                        ev.Category_id = reader["Category_id"] == DBNull.Value ? 0 : (int)reader["Category_id"];
                        events.Add(ev);
                    }//end of while

                    connect.Close();

                }//end of using

            }//end of try
            catch (Exception error) 
            {
                Console.WriteLine("GetAllEventsJason: "+error.Message); 
            
            }//end of catch

            return events;

        }//end of method


        public EventsModel GetEventByIdJson(int EventId)
        {//start of method

            EventsModel ev = new EventsModel();

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    string query = "SELECT EventId, EventName, Eventdescription, EventDate, EventLocation, MaxParticipants, Eventstatus, createdAt, Organiser_id, Category_id FROM Events WHERE EventId=" + EventId + ";";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);
                    SqlDataReader reader = run_query.ExecuteReader();

                    if (reader.Read())
                    {//start of if
                        ev.EventId = (int)reader["EventId"];
                        ev.EventName = reader["EventName"].ToString() ?? "";
                        ev.Eventdescription = reader["Eventdescription"].ToString() ?? "";
                        ev.EventDate = Convert.ToDateTime(reader["EventDate"]);
                        ev.EventLocation = reader["EventLocation"].ToString() ?? "";
                        ev.MaxParticipants = (int)reader["MaxParticipants"];
                        ev.Eventstatus = reader["Eventstatus"].ToString() ?? "";
                        ev.createdAt = reader["createdAt"].ToString() ?? "";
                        ev.Organiser_id = reader["Organiser_id"] == DBNull.Value ? 0 : (int)reader["Organiser_id"];
                        ev.Category_id = reader["Category_id"] == DBNull.Value ? 0 : (int)reader["Category_id"];
                    }//end of if

                    connect.Close();

                }//end of using

            }//end of try
            catch (Exception error) { Console.WriteLine("GetEventByIdJson error: " + error.Message); }//end of catch

            return ev;

        }//end of method

    }//end of class
}//end of namespace
