using RaceDay_poe_part2.models;
using System.Data.SqlClient;

namespace RaceDay_poe_part2.backend
{
    public class Result
    {

        string connections = @"Data source=(localdb)\raceday_api;database=racedaydb;";

        //POST
        public string CreateResult(string finishTime, int position, string ResultStatus, int BibNumber, string EventName, string Email)
        {//start of method

            string message = "Result could not be created...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    connect.Open();

                    string checkOrganizer = "SELECT TOP 1 o.OrganizerId FROM Organizers o INNER JOIN users u ON u.UserId = o.User_id WHERE u.Email='" + Email + "'";

                    SqlCommand run_check = new SqlCommand(checkOrganizer, connect);

                    if (run_check.ExecuteScalar() == null)
                    {
                        return "Only organizers can post results";
                    }


                    //find the enrollment this result belongs to
                    string findEnrollment = "SELECT TOP 1 e.EnrollmentId FROM Enrollment e " +
                                            "INNER JOIN Events ev ON ev.EventId = e.event_id " +
                                            "WHERE e.BibNumber = " + BibNumber + " AND ev.EventName = '" + EventName + "';";

                    SqlCommand run_find = new SqlCommand(findEnrollment, connect);

                    object found = run_find.ExecuteScalar();

                    if (found == null)
                    {
                        message = "No enrollment found for that bib number and event";
                    }
                    else
                    {
                        //recorded time is set automatically
                        string recordedAt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                        string query = "INSERT INTO Results (finishTime, position, ResultStatus, RecordedAt, Enrollment_id) VALUES('" +
                                       finishTime + "'," + position + ",'" + ResultStatus + "','" + recordedAt + "'," + (int)found + ")";

                        SqlCommand run_query = new SqlCommand(query, connect);

                        int result = run_query.ExecuteNonQuery();

                        if (result > 0)
                        {
                            message = "Result created successfully";
                        }
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





        //GET ALL
        public string GetAllResults()
        {//start of method

            string message = "No results found...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    int countResult = 0;

                    string query = "SELECT r.ResultId, r.finishTime, r.position, r.ResultStatus, r.RecordedAt, r.Enrollment_id, e.BibNumber, ev.EventName " +
                                   "FROM Results r " +
                                   "LEFT JOIN Enrollment e ON e.EnrollmentId = r.Enrollment_id " +
                                   "LEFT JOIN Events ev ON ev.EventId = e.event_id;";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    string allResults = "";

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start of while loop

                            countResult = 1;
                            allResults += "ResultId: " + reader["ResultId"].ToString() +
                                          ", Finish Time: " + reader["finishTime"].ToString().Trim() +
                                          ", Position: " + reader["position"].ToString() +
                                          ", Status: " + reader["ResultStatus"].ToString() +
                                          ", Recorded At: " + reader["RecordedAt"].ToString().Trim() +
                                          ", Enrollment Id: " + reader["Enrollment_id"].ToString() +
                                          ", Bib Number: " + reader["BibNumber"].ToString() +
                                          ", Event Name: " + reader["EventName"].ToString() + "\n";

                        }//end of while loop

                    }//end of if

                    if (countResult == 1)
                    {
                        message = allResults;
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






        //GET BY ID
        public string ResultbyID(int ResultId)
        {//start of method

            string message = "Result is found...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    int countResult = 0;

                    string query = "SELECT r.ResultId, r.finishTime, r.position, r.ResultStatus, r.RecordedAt, r.Enrollment_id, e.BibNumber, ev.EventName " +
                                   "FROM Results r " +
                                   "LEFT JOIN Enrollment e ON e.EnrollmentId = r.Enrollment_id " +
                                   "LEFT JOIN Events ev ON ev.EventId = e.event_id " +
                                   "WHERE r.ResultId = " + ResultId + ";";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);

                    SqlDataReader reader = run_query.ExecuteReader();

                    if (reader != null)
                    {//start of if
                        while (reader.Read())
                        {//start of while loop

                            countResult = 1;
                            message = "ResultId: " + reader["ResultId"].ToString();
                            message += ", Finish Time: " + reader["finishTime"].ToString().Trim();
                            message += ", Position: " + reader["position"].ToString();
                            message += ", Status: " + reader["ResultStatus"].ToString();
                            message += ", Recorded At: " + reader["RecordedAt"].ToString().Trim();
                            message += ", Enrollment Id: " + reader["Enrollment_id"].ToString();
                            message += ", Bib Number: " + reader["BibNumber"].ToString();
                            message += ", Event Name: " + reader["EventName"].ToString();

                        }//end of while loop

                    }//end of if

                    if (countResult == 0)
                    {
                        message = "Result not found";
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







        //PUT
        public string UpdateResult_byId(int ResultId, string finishTime, int position, string ResultStatus)
        {//start of method

            string message = "Result could not be updated...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    string query = "UPDATE Results SET finishTime = '" + finishTime + "', position = " + position +
                                   ", ResultStatus = '" + ResultStatus + "' WHERE ResultId = " + ResultId + ";";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "Result updated successfully";
                    }
                    else
                    {
                        message = "Result not found";
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







        //DELETE
        public string DeleteResult_byId(int ResultId)
        {//start of method

            string message = "Result could not be deleted...";

            try
            {//start of try

                using (SqlConnection connect = new SqlConnection(connections))
                {//start of using

                    string query = "DELETE FROM Results WHERE ResultId = " + ResultId + ";";

                    connect.Open();

                    SqlCommand run_query = new SqlCommand(query, connect);

                    int result = run_query.ExecuteNonQuery();

                    if (result > 0)
                    {
                        message = "Result deleted successfully";
                    }
                    else
                    {
                        message = "Result not found";
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


        public List<ResultsModel> GetAllResultsJson()
        {
            List<ResultsModel> results = new List<ResultsModel>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connections))
                {
                    string query = "SELECT ResultId, finishTime, position, ResultStatus, RecordedAt, Enrollment_id FROM Results;";
                    connect.Open();
                    SqlCommand run_query = new SqlCommand(query, connect);
                    SqlDataReader reader = run_query.ExecuteReader();
                    while (reader.Read())
                    {
                        ResultsModel r = new ResultsModel();
                        r.ResultId = (int)reader["ResultId"];
                        r.finishTime = reader["finishTime"].ToString() ?? "";
                        r.position = reader["position"] == DBNull.Value ? 0 : (int)reader["position"];
                        r.ResultStatus = reader["ResultStatus"].ToString() ?? "";
                        r.RecordedAt = reader["RecordedAt"].ToString() ?? "";
                        r.Enrollment_id = reader["Enrollment_id"] == DBNull.Value ? 0 : (int)reader["Enrollment_id"];
                        results.Add(r);
                    }
                    connect.Close();
                }
            }
            catch (Exception error) { Console.WriteLine("GetAllResultsJson error: " + error.Message); }
            return results;
        }




        public List<ResultsModel> GetMyResultsJson(string email)
        {
            List<ResultsModel> results = new List<ResultsModel>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connections))
                {
                    string query = @"SELECT r.ResultId, r.finishTime, r.position, r.ResultStatus, r.RecordedAt, r.Enrollment_id
                            FROM Results r
                            LEFT JOIN Enrollment e ON r.Enrollment_id = e.EnrollmentId
                            LEFT JOIN Participants p ON e.Participant_id = p.ParticipantId
                            LEFT JOIN Users u ON p.User_id = u.UserId
                            WHERE u.Email = @email";
                    connect.Open();
                    SqlCommand run_query = new SqlCommand(query, connect);
                    run_query.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = run_query.ExecuteReader();
                    while (reader.Read())
                    {
                        ResultsModel r = new ResultsModel();
                        r.ResultId = (int)reader["ResultId"];
                        r.finishTime = reader["finishTime"].ToString() ?? "";
                        r.position = reader["position"] == DBNull.Value ? 0 : (int)reader["position"];
                        r.ResultStatus = reader["ResultStatus"].ToString() ?? "";
                        r.RecordedAt = reader["RecordedAt"].ToString() ?? "";
                        r.Enrollment_id = reader["Enrollment_id"] == DBNull.Value ? 0 : (int)reader["Enrollment_id"];
                        results.Add(r);
                    }
                    connect.Close();
                }
            }
            catch (Exception error) { Console.WriteLine("GetMyResultsJson error: " + error.Message); }
            return results;
        }
    }
}
