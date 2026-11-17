using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice_webApi.backend;

namespace Practice_webApi.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class Prac_user_details : ControllerBase
    {//start of class

        //method to post user details
        //to the backend 
        [HttpPost]
        public string Post_userDetails(string userName,string userSurname,int userAge) 
        {//start of method
            prac_Storeuser set_user = new prac_Storeuser();
            string message = set_user.store_user_details(userName, userSurname, userAge);

            return message;
        }//end of method

        //method to get user details
        //from the backend
        [HttpGet]
        public string Get_userdetails(string userName,String userSurname) 
        {//tart of method
          
            prac_Getuser getuser = new prac_Getuser();
            string message = getuser.Get_userDetails(userName, userSurname);

            return message; 
        }//end of method


    }//end of class
}//end of namespace
