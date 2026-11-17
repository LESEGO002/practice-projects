using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;

namespace RaceDay_poe_part2.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class participantController : ControllerBase
    {//start of class

        ParticipantsModel par;

        //httpGet request to Get the organiser detailes from db
        [HttpGet("participants & Users")]

        public string ParticipantInfo()
        {//start of method

            Participants par = new Participants();
            string Part = par.ParticipantsInfo();

            return Part;

        }//end of method



        [HttpGet("{ParticipantId}")]

        public string Participantby_id_Info(int ParticipantId)
        {//start of method

            Participants par = new Participants();
            string Part = par.ParticipantInfo_byId(ParticipantId);

            return "Details for Participant " + Part ;

        }//end of method
        

    }//end of class
}//end of namespace
