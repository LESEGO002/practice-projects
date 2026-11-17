using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using password_hashing.database;
using System.Data;


namespace password_hashing.Controllers
{//start of namexpace

    [Route("api/[controller]")]
    [ApiController]
    public class hashing : ControllerBase
    {//start of class

        //method to post password & email
        //using http post
        [HttpPost]

        public string register_user(string email,string password) 
        {//start of method

            //hash the password and store it in
            //the database
            string hashed_password = BCrypt.Net.BCrypt.HashPassword(password);

            //creating instance to connect to the class
            store_info store = new store_info();
            string result_message = store.store_userinfo(email, hashed_password);

            return result_message;

        }//end of method



        [HttpGet]
        public string login_user(string email ,string password)
        {//start of login

            get_userInfo store = new get_userInfo();
            string result = store.get_info(email,password);

            //temp varibale to hold pre-hashed password
            //take it as it is from the database
            string password_fromTable = BCrypt.Net.BCrypt.HashPassword(password);

            //chaeck if the password entered by the user is the same one which 
            //is hashed from the table,which is password is incorrect
            Boolean correct_password = BCrypt.Net.BCrypt.Verify(password, password_fromTable);

            //check if the password the user entered is found true or false
            if (correct_password.Equals(password_fromTable))
            {
                return "Password Is Verified";

            }
            else
            {
                
                return "Password Failed to Verifiy....";

            }

            
        }//end of login

       

    }//end of class
}//end of namespace
