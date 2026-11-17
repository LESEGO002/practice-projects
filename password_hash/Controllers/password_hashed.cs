using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.VisualBasic;

namespace password_hash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class password_hashed : ControllerBase
    {
        [HttpPost]
        public string passwordHash(string passaword) 
        {
            string hashed_passaword = BCrypt.Net.BCrypt.HashPassword(passaword);

            Boolean correct_password = BCrypt.Net.BCrypt.Verify(passaword,hashed_passaword);

            return "your password is " + passaword + "and the hashed password is " + hashed_passaword;
        }

    }
}
