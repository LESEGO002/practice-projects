using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;


namespace RaceDay_poe_part2.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {//start of class

        EnrollmentsModel enrol;
        //post request to poste user enrollment
        [HttpPost("createenrollment/organizers")]

        public string CreateEnrollment(int BibNumber, string Enrollmentstatus, string EventName, string Email) 
        { //start of method

            Enrollments enroll = new Enrollments();
            string enrollresult = enroll.CreateEnrollment( BibNumber, Enrollmentstatus, EventName, Email);

            return enrollresult;
        }//end of method



        //post request to poste user enrollment
        [HttpGet("All enrollments")]

        public string GetAllEnrollments()
        { //start of method

            Enrollments enroll1 = new Enrollments();
            string enrollresult = enroll1.GetAllEnrollments();

            return enrollresult;
        }//end of  //post request to poste user enrollment


        [HttpGet("all")]
        public List<EnrollmentsModel> GetAllEnrollmentsJson()
        {
            Enrollments enroll = new Enrollments();
            return enroll.GetAllEnrollmentsJson();
        }

        [HttpGet("MyEnrollments")]
        public List<EnrollmentsModel> GetMyEnrollments(string email)
        {
            Enrollments enroll = new Enrollments();
            return enroll.GetMyEnrollmentsJson(email);
        }

        //method to get enrollmet by Id
        [HttpGet("{EnrollmentId}")]

        public string GetEnrollmentById(int EnrollmentId)
        { //start of method

            Enrollments enroll1 = new Enrollments();
            string enrollresult = enroll1.EnrollmentbyID(EnrollmentId);

            return enrollresult;
        }//end of  //post request to poste user enrollment


        //method to Update enrollment by Id
        [HttpPut("info_byId/{EnrollmentId}")]

        public string UpdateEnrollmentById(int EnrollmentId, int BibNumber, string Enrollmentstatus)
        { //start of method

            Enrollments enroll1 = new Enrollments();
            string enrollresult = enroll1.UpdateEnrollment_byId(EnrollmentId, BibNumber, Enrollmentstatus);

            return enrollresult;
        }//end of method



        //method to Delete enrollment by Id
        [HttpDelete("delete/{EnrollmentId}")]

        public string DeleteEnrollmentById(int EnrollmentId)
        { //start of method

            Enrollments enroll1 = new Enrollments();
            string enrollresult = enroll1.DeleteEnrollment_byId(EnrollmentId);

            return enrollresult;
        }//end of method




       
    }//end of class
}//end of method
