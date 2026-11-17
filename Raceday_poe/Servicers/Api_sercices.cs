using Raceday_poe.Models;
using System.Text.Json;

namespace Raceday_poe.Servicers
{
    public class Api_sercices
    {

        //start of field
        private readonly HttpClient _httpClient;
        //end of field

        //start of constructor
        public Api_sercices(HttpClient _http)
        {
            _httpClient = _http;
        }
        //end of constructor

        //start of method
        public async Task<string> UserRegistration(RegisterModel register)
        {
            string message = "";
            string reg_url = "api/Auth/Register" +
                $"?FirstName={register.FirstName}" +
                $"&LastName={register.LastName}" +
                $"&age={register.age}" +
                $"&Email={register.Email}" +
                $"&passwordHashed={register.passwordHashed}" +
                $"&UserRole={register.UserRole}";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsync(reg_url, null);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Registration failed...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<string> UserLogin(LoginModel login)
        {
            string message = "";
            string login_url = "api/Auth/Login" +
                $"?Email={login.Email}" +
                $"&passwordHashed={login.passwordHashed}";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync(login_url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to login...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<string> GetUserRole(string email)
        {
            string message = "";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/Auth/GetUserRole?Email={email}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                    message = message.Trim('"');
                    
                }
                else
                {
                    message = "Participant";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<UserProfileModel> GetUserProfile(string email)
        {
            UserProfileModel profile = new UserProfileModel();
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/Auth/GetUser?Email={email}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    profile = JsonSerializer.Deserialize<UserProfileModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new UserProfileModel();
                }
            }
            catch (Exception error)
            {
                profile.FirstName = error.Message;
            }
            return profile;
        }
        //end of method

        //start of method
        public async Task<string> UpdateUserProfile(UserProfileModel profile)
        {
            string message = "";
            string url = "api/Auth/UpdateUser" +
                $"?Email={profile.Email}" +
                $"&FirstName={profile.FirstName}" +
                $"&LastName={profile.LastName}" +
                $"&age={profile.age}";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PutAsync(url, null);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to update profile...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<List<EventModel>> GetAllEvents()
        {
            List<EventModel> events = new List<EventModel>();
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync("api/Events/all");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    events = JsonSerializer.Deserialize<List<EventModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EventModel>();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return events;
        }
        //end of method

        //start of method
        public async Task<EventModel?> GetEventById(int id)
        {
            EventModel? ev = null;
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/Events/details/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    ev = JsonSerializer.Deserialize<EventModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return ev;
        }
        //end of method

        //start of method
        public async Task<string> CreateEvent(EventModel ev)
        {
            string message = "";
            string url = "api/Events/Organizers" +
     $"?EventName={ev.EventName}" +
     $"&Eventdescription={ev.Eventdescription}" +
     $"&EventDate={ev.EventDate:yyyy-MM-dd}" +
     $"&EventLocation={ev.EventLocation}" +
     $"&MaxParticipants={ev.MaxParticipants}" +
     $"&Eventstatus={ev.Eventstatus}" +
     $"&organizerEmail={ev.organizerEmail}" +
     $"&categoryName={ev.categoryName}";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsync(url, null);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to create event...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<string> UpdateEvent(int id, EventModel ev)
        {
            string message = "";
            string url = "api/Events/Organizers" +
    $"?EventName={ev.EventName}" +
    $"&Eventdescription={ev.Eventdescription}" +
    $"&EventDate={ev.EventDate:yyyy-MM-dd}" +
    $"&EventLocation={ev.EventLocation}" +
    $"&MaxParticipants={ev.MaxParticipants}" +
    $"&Eventstatus={ev.Eventstatus}" +
    $"&organizerEmail={ev.organizerEmail}" +
    $"&categoryName={ev.categoryName}";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PutAsync(url, null);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to update event...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<string> DeleteEvent(int id)
        {
            string message = "";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"api/Events/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to delete event...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<List<CategoryModel>> GetAllCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync("api/Categories/all");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<CategoryModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryModel>();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return categories;
        }
        //end of method

        //start of method
        public async Task<string> CreateCategory(CategoryModel cat)
        {
            string message = "";
            string url = "api/Categories/organizers" +
            $"?CategoryName={cat.categoryName}" +
            $"&CategoryDescrition={cat.categoryDescrition}" +
            $"&MinAge={cat.minAge}" +
            $"&MaxAge={cat.maxAge}" +
            $"&OrganizerEmail={cat.organizerEmail}";
            
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsync(url, null);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to create category...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method

        //start of method
        public async Task<List<EnrollmentModel>> GetAllEnrollments()
        {
            List<EnrollmentModel> enrollments = new List<EnrollmentModel>();
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync("api/Enrollments/all");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    enrollments = JsonSerializer.Deserialize<List<EnrollmentModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EnrollmentModel>();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return enrollments;
        }
        //end of method

        //start of method
        public async Task<List<EnrollmentModel>> GetMyEnrollments(string email)
        {
            List<EnrollmentModel> enrollments = new List<EnrollmentModel>();
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/Enrollments/MyEnrollments?email={email}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    enrollments = JsonSerializer.Deserialize<List<EnrollmentModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EnrollmentModel>();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("GetMyEnrollments error: " + error.Message);
            }
            return enrollments;
        }
        //end of method

        //start of method
        public async Task<string> CreateEnrollment(EnrollmentModel enroll)
        {
            string message = "";
            string url = "api/Enrollments/createenrollment/organizers" +
                $"?BibNumber={enroll.BibNumber}" +
                $"&Enrollmentstatus={enroll.Enrollmentstatus}" +
                $"&EventName={enroll.eventName}" +
                $"&Email={enroll.participantEmail}";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsync(url, null);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to enroll...";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }
        //end of method

        //start of method
        public async Task<List<ResultModel>> GetAllResults()
        {
            List<ResultModel> results = new List<ResultModel>();
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync("api/Results/all");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    results = JsonSerializer.Deserialize<List<ResultModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ResultModel>();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return results;
        }

        //start of method
        public async Task<List<ResultModel>> GetMyResults(string email)
        {
            List<ResultModel> results = new List<ResultModel>();
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/Results/MyResults?email={email}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    results = JsonSerializer.Deserialize<List<ResultModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ResultModel>();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return results;
        }
        //end of method

        //start of method
        public async Task<string> CaptureResult(ResultModel result)
        {
            string message = "";
            string url = "api/Results/CaptureResults" +
     $"?finishTime={result.finishTime}" +
     $"&position={result.position}" +
     $"&ResultStatus={result.ResultStatus}" +
     $"&BibNumber={result.BibNumber}" +
     $"&EventName={result.eventName}" +
     $"&Email={result.organizerEmail}";
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsync(url, null);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    message = "Failed to capture result...";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        //end of method
    }
}
