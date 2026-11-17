using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI_01.Models;

namespace WebApplicationAPI_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {//start of class

        //crate an object for user class
        User users = new User() 
        { //opening curly brace
            PropertyId =500,
            PropertyName = "John",
            Age = 25

        };//closing curly brace
        //create and iaction method getinfo()
        [HttpGet]
        public IActionResult getInfo() { 

        return Ok(users);
        }
        //create an action method getinfo2()
        [HttpDelete]
        public string deleteInfoidget(int PropertyId)
        {
            return "Delete method is called";
        }
    }//end of class
}
