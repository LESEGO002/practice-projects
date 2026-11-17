using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;
using System.Net.NetworkInformation;

namespace RaceDay_poe_part2.Controllers
{//start of namespace

    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserController : ControllerBase
    {//start of controller class


        OrganizersModel orgn;

        //httpGet request to Get the organiser detailes from db
        [HttpGet("Organizers & Users")]

        public string OrganiserInfo() 
        {//start of method

            Organizers org = new Organizers();
            string organ = org.OrganisersInfo();

            return organ;
        
        }//end of method


        [HttpGet("{OrganizerId}")]

        public string Organiserby_id_Info(int OrganizerId)
        {//start of method

            Organizers orgId = new Organizers();
            string organId = orgId.OrganisersInfo_byId(OrganizerId);

            return organId;

        }//end of method

    }//end of controller class
}//end of namespace
