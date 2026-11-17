using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SessionsExample.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {//start of method

        //get request to register user
        [HttpPost]
        public string Register(string name) 
        {//start of method

            //registering a session so that
            //the data is stored when the program is running
            //last according to the time set in session
            HttpContext.Session.SetString("username",name);
            


            return "Saved " + name + " Memory";
        }//end of register method



        [HttpGet]

        public string Load() 
        {//start of Load method

            string name = HttpContext.Session.GetString("username");

            if (name == null)
            {
                return "session has expired";
            }
            else
            {

                return name + " session is not timed out yet";
            }
            
        }//end of load method

    }//end of method

}//end of namespace
