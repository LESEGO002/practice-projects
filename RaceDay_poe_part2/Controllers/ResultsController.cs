using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;
using System.Numerics;

namespace RaceDay_poe_part2.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {//start of class


        ResultsModel res;

        //post request to post the results in the database
        [HttpPost("CaptureResults")]

        public string Captureresults(string finishTime, int position, string ResultStatus, int BibNumber, string EventName, string Email) 
        {//start of method
         //
            Result cap = new Result();
            string capres = cap.CreateResult( finishTime, position, ResultStatus, BibNumber, EventName,Email);

            return capres;
        
        }//end of method


        [HttpGet("GetAllResults")]
        public string GetAllResults()
        {//start of method

            Result cap = new Result();
            string Getres = cap.GetAllResults();

            return Getres;

        }//end of method


        //get request result Id
        [HttpGet("ResultbyID/{ResultId}")]
        public string ResultbyID(int ResultId)
        {//start of method

            Result cap = new Result();
            string GetresId = cap.ResultbyID(ResultId);

            return GetresId;
        }//end of method



        //update request
        [HttpPut("UpdateResult/{ResultId}")]
        public string UpdateResult(int ResultId, string finishTime, int position, string ResultStatus)
        {//start of merhod

            Result cap = new Result();
            string Upres = cap.UpdateResult_byId(ResultId, finishTime, position, ResultStatus);


            return Upres;

        }//end of method



        [HttpDelete("DeleteResult/{ResultId}")]
        public string DeleteResult(int ResultId)
        {//Start of method

            Result cap = new Result();
            string Delres = cap.ResultbyID(ResultId);

            return Delres;




        }//end of methd


        [HttpGet("all")]
        public List<ResultsModel> GetAllResultsJson()
        {
            Result cap = new Result();
            return cap.GetAllResultsJson();
        }


        [HttpGet("MyResults")]
        public List<ResultsModel> GetMyResults(string email)
        {
            Result cap = new Result();
            return cap.GetMyResultsJson(email);
        }

    }//end of class
}//end of namespace
