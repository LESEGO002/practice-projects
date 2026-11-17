using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;

namespace RaceDay_poe_part2.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {//start of class

        //HttpPost request to send the user info to the database
        AuthModel enduser1;

        public enum role
        {
            Organizer,
            Participant
        }

        [HttpPost("Register")]

        //method to send user info to the database
       
        public string RegisterInfo(string FirstName, string LastName, int age, string Email, string passwordHashed, role UserRole)
        {//start of method

            HttpContext.Session.SetString("FirstName",FirstName);
            HttpContext.Session.SetString("LastName", LastName);
            HttpContext.Session.SetInt32("age", age);
            HttpContext.Session.SetString("Email", FirstName);
            HttpContext.Session.SetString("password", passwordHashed);

            enduser1 = new AuthModel()
            {
                FirstName = FirstName,
                LastName = LastName,
                age = age,
                Email = Email,
                passwordHashed = passwordHashed,
                //createdAt = createdAt,
                
            };

            //check the plain password before it gets hashed
            Authentication pass = new Authentication();
            string passwordMessage = pass.PasswordCheck(passwordHashed);

            if (passwordMessage != "")
            {
                return passwordMessage;
            }

            //...your existing hash line and RegisterUserInfo call stay below, unchanged

            //hash the users passoerd
            string hashedpassword = BCrypt.Net.BCrypt.HashPassword(passwordHashed);

            //call the user class into this method
            Authentication store = new Authentication();
            string result = store.RegisterUserInfo(enduser1.FirstName, enduser1.LastName, enduser1.age, enduser1.Email, hashedpassword, UserRole.ToString());

            

            return result + "  " + UserRole.ToString();
        }//end of method





        //httppost reqyest 
        [HttpGet("Login")]
        public string LoginUser(string Email, string passwordHashed)
        {//start of login method

            HttpContext.Session.SetString("email", Email);
            HttpContext.Session.SetString("password", passwordHashed);

            enduser1 = new AuthModel()
            {
                Email = Email,
                passwordHashed = passwordHashed
                

            };


            Authentication use = new Authentication();
            string reply = use.LogInInfo(Email, passwordHashed);

            //verify the hashed password
           string hashed_password = BCrypt.Net.BCrypt.HashPassword(passwordHashed);

            bool correctpassword = BCrypt.Net.BCrypt.Verify(passwordHashed, hashed_password);

           if (correctpassword.Equals(hashed_password))
            {//start of if
                return reply;

            }//end of if
            else
            {//start of els
                return reply;
            }//end of else


        }//end of login method

        [HttpGet("GetUserRole")]
        public string GetUserRole(string Email)
        {//start of method

            Authentication auth = new Authentication();
            string role = auth.GetUserRole(Email);
            return role;

        }//end of method

    }//end of class
}//end of namespace
