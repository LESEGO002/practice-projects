using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;

namespace RaceDay_poe_part2.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {//start ofclass

        //httpget request to get user information from the db
        //using the user Id
        public enum roles
        {
            Organizer,
            Participant
        }

        UserModel use;

        [HttpGet("view/{UserId}")]

        //method to get user information from the db 
        public string User_byId(int UserId)
        {//start of method

            

            User end = new User();
            return "Detailes for " + UserId + " is " + end.users(UserId);


        }//end of method

        

        //httpPut method to update user informaton by Id
        [HttpPut("update/{UserId}")]

        //method to update user information
        public string updatedBy_Id(int UserId, string FirstName, string LastName, int age, string Email, string passwordHashed, roles UserRole) 
        {//start of method

        
            //hash the users passoerd
            string hashedpassword = BCrypt.Net.BCrypt.HashPassword(passwordHashed);

            User use = new User();
            string output = use.update_byId(UserId, FirstName, LastName, age, Email, hashedpassword, UserRole.ToString());

            return output + UserRole.ToString();
        }//end of method



        [HttpDelete("delete/{UserId}")]

        //method to get user information from the db 
        public string deleteUser_byId(int UserId)
        {//start of method

            User end = new User();
            string message =end.DeleteEvent(UserId);

            return message;
        }//end of method

    }//end of class
}//end of namespace
