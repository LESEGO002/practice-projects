using RaceDay_poe_part2.models;
using System.Data.SqlClient;

namespace RaceDay_poe_part2.backend
{//start of namespace
    public class Enrollments
    {//start of class


        //temp global varibles
        //string message = "Event could not be created found...";
        string connections = @"Data source=(localdb)\raceday_api;database=racedaydb;";

        public string CreateEnrollment(int BibNumber, string Enrollmentstatus, string EventName, string Email)
        {//start of method

            string message = "Enrollment could not be created...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    //insert the enrollment, finding the event and participant ids with lookups
                    string query = "INSERT INTO Enrollment (BibNumber, Enrollmentstatus, event_id, Participant_id) VALUES(" +
                        BibNumber + ",'" + Enrollmentstatus + "'," +
                        "(SELECT TOP 1 EventId FROM Events WHERE EventName='" + EventName + "')," +
                        "(SELECT TOP 1 p.ParticipantId FROM Participants p INNER JOIN users u ON u.UserId = p.User_id WHERE u.Email='" + Email + "' ORDER BY p.ParticipantId))";

                    //open connection
                    connect.Open();

                    //the event must exist
                    SqlCommand run_checkEvent = new SqlCommand("SELECT TOP 1 EventId FROM Events WHERE EventName='" + EventName + "'", connect);

                    if (run_checkEvent.ExecuteScalar() == null)
                    {
                        return "Event not found, check the event name";
                    }

                    //the participant must exist
                    SqlCommand run_checkParticipant = new SqlCommand("SELECT TOP 1 p.ParticipantId FROM Participants p INNER JOIN users u ON u.UserId = p.User_id WHERE u.Email='" + Email + "'", connect);

                    if (run_checkParticipant.ExecuteScalar() == null)
                    {
                        return "Participant not found, check the email";
                    }

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "Enrollment created successfully";
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


        public string EnrollmentbyID(int EnrollmentId)
        {//start of method

            string message = "Enrollment is found...";

            //try and catch block to catch errors
            try
            {//start of try

                //use using
                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    int countEnrollment = 0;

                    connect.Open();

                    //create the query to get the enrollment by Id
                    string query = "SELECT e.EnrollmentId, e.BibNumber, e.EnrollmentDate, e.Enrollmentstatus, e.event_id, ev.EventName, e.Participant_id, u.Email " +
                                   "FROM Enrollment e " +
                                   "LEFT JOIN Events ev ON ev.EventId = e.event_id " +
                                   "LEFT JOIN Participants p ON p.ParticipantId = e.Participant_id " +
                                   "LEFT JOIN users u ON u.UserId = p.User_id " +
                                   "WHERE e.EnrollmentId = " + EnrollmentId + ";";

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start of while loop

                            //get the data per column
                            countEnrollment = 1;
                            message = "EnrollmentId: " + reader["EnrollmentId"].ToString();
                            message += ", Bib Number: " + reader["BibNumber"].ToString();
                            message += ", Date: " + reader["EnrollmentDate"].ToString();
                            message += ", Status: " + reader["Enrollmentstatus"].ToString().Trim();
                            message += ", Event Id: " + reader["event_id"].ToString();
                            message += ", Event Name: " + reader["EventName"].ToString();
                            message += ", Participant Id: " + reader["Participant_id"].ToString();
                            message += ", Participant Email: " + reader["Email"].ToString();

                        }//end of while loop

                    }//end of if

                    if (countEnrollment == 0)
                    {//start of if

                        //Assign error message if enrollment not found
                        message = "Enrollment not found";

                    }//end of if

                    connect.Close();

                }//end of using

            }//end of try
            catch (Exception error)
            {//start of catch

                message = error.Message;

            }//end of catch

            return message;

        }//end of method




        //method to get all enrollment details
        public string GetAllEnrollments()
        {//start of method

            string message = "No enrollments found...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    int countEnrollment = 0;

                    //create the query to get all the enrollments
                    string query = "SELECT e.EnrollmentId, e.BibNumber, e.EnrollmentDate, e.Enrollmentstatus, e.event_id, ev.EventName, e.Participant_id, u.Email " +
                                   "FROM Enrollment e " +
                                   "LEFT JOIN Events ev ON ev.EventId = e.event_id " +
                                   "LEFT JOIN Participants p ON p.ParticipantId = e.Participant_id " +
                                   "LEFT JOIN users u ON u.UserId = p.User_id;";

                    //open connection
                    connect.Open();

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    string allEnrollments = "";

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start of while loop

                            //get the data per column and add each enrollment on its own line
                            countEnrollment = 1;
                            allEnrollments += "EnrollmentId: " + reader["EnrollmentId"].ToString();
                            allEnrollments += ", Bib Number: " + reader["BibNumber"].ToString();
                            allEnrollments += ", Date: " + reader["EnrollmentDate"].ToString();
                            allEnrollments += ", Status: " + reader["Enrollmentstatus"].ToString();
                            allEnrollments += ", Event Id: " + reader["event_id"].ToString();
                            allEnrollments += ", Event Name: " + reader["EventName"].ToString();
                            allEnrollments += ", Participant Id: " + reader["Participant_id"].ToString();
                            allEnrollments += ", Participant Email: " + reader["Email"].ToString();

                        }//end of while loop

                    }//end of if

                    if (countEnrollment == 1)
                    {
                        message = allEnrollments;
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



        public string UpdateEnrollment_byId(int EnrollmentId, int BibNumber, string Enrollmentstatus)
        {//start of method

            string message = "Enrollment could not be updated...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    //create the query to update the enrollment
                    string query = "UPDATE Enrollment SET BibNumber = " + BibNumber +
                                   ", Enrollmentstatus = '" + Enrollmentstatus +
                                   "' WHERE EnrollmentId = " + EnrollmentId + ";";

                    //open connection
                    connect.Open();

                    //sqlcommand to run the query
                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "Enrollment updated successfully";
                    }
                    else
                    {
                        message = "Enrollment not found";
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


        public string DeleteEnrollment_byId(int EnrollmentId)
        {//start of method

            string message = "Enrollment could not be deleted...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    string query = "DELETE FROM Enrollment WHERE EnrollmentId = " + EnrollmentId + ";";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "Enrollment deleted successfully";
                    }
                    else
                    {
                        message = "Enrollment not found";
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



        public List<EnrollmentsModel> GetAllEnrollmentsJson()
        {
            List<EnrollmentsModel> enrollments = new List<EnrollmentsModel>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connections))
                {
                    string query = @"SELECT e.EnrollmentId, e.BibNumber, e.EnrollmentDate, e.Enrollmentstatus, 
                e.event_id, e.Participant_id,
                ev.EventName, ev.EventDate,
                u.Email AS ParticipantEmail
                FROM Enrollment e
                LEFT JOIN Events ev ON e.event_id = ev.EventId
                LEFT JOIN Participants p ON e.Participant_id = p.ParticipantId
                LEFT JOIN Users u ON p.User_id = u.UserId";
                    connect.Open();
                    SqlCommand run_query = new SqlCommand(query, connect);
                    SqlDataReader reader = run_query.ExecuteReader();
                    while (reader.Read())
                    {
                        EnrollmentsModel en = new EnrollmentsModel();
                        en.EnrollmentId = (int)reader["EnrollmentId"];
                        en.BibNumber = reader["BibNumber"] == DBNull.Value ? 0 : (int)reader["BibNumber"];
                        en.EnrollmentDate = reader["EnrollmentDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["EnrollmentDate"];
                        en.Enrollmentstatus = reader["Enrollmentstatus"].ToString() ?? "";
                        en.event_id = reader["event_id"] == DBNull.Value ? 0 : (int)reader["event_id"];
                        en.Participant_id = reader["Participant_id"] == DBNull.Value ? 0 : (int)reader["Participant_id"];
                        en.EventName = reader["EventName"].ToString() ?? "";
                        en.EventDate = reader["EventDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["EventDate"];
                        en.ParticipantEmail = reader["ParticipantEmail"].ToString() ?? "";
                        enrollments.Add(en);
                    }
                    connect.Close();
                }
            }
            catch (Exception error) { Console.WriteLine("GetAllEnrollmentsJson error: " + error.Message); }
            return enrollments;
        }


        public List<EnrollmentsModel> GetMyEnrollmentsJson(string email)
        {
            List<EnrollmentsModel> enrollments = new List<EnrollmentsModel>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connections))
                {
                    string query = @"SELECT e.EnrollmentId, e.BibNumber, e.EnrollmentDate, e.Enrollmentstatus,
                            e.event_id, e.Participant_id,
                            ev.EventName, ev.EventDate,
                            u.Email AS ParticipantEmail
                            FROM Enrollment e
                            LEFT JOIN Events ev ON e.event_id = ev.EventId
                            LEFT JOIN Participants p ON e.Participant_id = p.ParticipantId
                            LEFT JOIN Users u ON p.User_id = u.UserId
                            WHERE u.Email = @email";
                    connect.Open();
                    SqlCommand run_query = new SqlCommand(query, connect);
                    run_query.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = run_query.ExecuteReader();
                    while (reader.Read())
                    {
                        EnrollmentsModel en = new EnrollmentsModel();
                        en.EnrollmentId = (int)reader["EnrollmentId"];
                        en.BibNumber = reader["BibNumber"] == DBNull.Value ? 0 : (int)reader["BibNumber"];
                        en.EnrollmentDate = reader["EnrollmentDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["EnrollmentDate"];
                        en.Enrollmentstatus = reader["Enrollmentstatus"].ToString() ?? "";
                        en.event_id = reader["event_id"] == DBNull.Value ? 0 : (int)reader["event_id"];
                        en.Participant_id = reader["Participant_id"] == DBNull.Value ? 0 : (int)reader["Participant_id"];
                        en.EventName = reader["EventName"].ToString() ?? "";
                        en.EventDate = reader["EventDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["EventDate"];
                        en.ParticipantEmail = reader["ParticipantEmail"].ToString() ?? "";
                        enrollments.Add(en);
                    }
                    connect.Close();
                }
            }
            catch (Exception error) { Console.WriteLine("GetMyEnrollmentsJson error: " + error.Message); }
            return enrollments;
        }


    }//end of classs
}//end of 
