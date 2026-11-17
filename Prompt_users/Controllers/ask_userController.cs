using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prompt_users.database;

namespace Prompt_users.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class ask_userController : ControllerBase
    {//start of class

        //post http request to send user info
        //to the database
        [HttpPost]
        //method to send user info to the database
        public string get_userInfo(string Name,string Surname,int age) 
        {//start of method
            Store_info store = new Store_info();
            string message = store.Store_Userinfo(Name,Surname,age);


            return message;
        }//end of method


    }//end of class
}//end of namespace
