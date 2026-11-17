using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;
using System.Diagnostics;

namespace RaceDay_poe_part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public enum catergory 
        {
            TrackDay ,
            TrackDayCarBike,
            RaceMeeting,
            DriftDay,
            BikeDay,
            UnofficialPractice, 
            Historics,
            Specialthemeddays
        
        }

        public enum catergoryUpdate
        {
            TrackDay,
            TrackDayCarBike,
            RaceMeeting,
            DriftDay,
            BikeDay,
            UnofficialPractice,
            Historics,
            Specialthemeddays

        }

        CatergoriesModel cat1;

        //httpPost request to Get the organiser detailes from db
        [HttpPost("organizers")]

        public string Category_CreatedInfo(catergory CategoryName, string CategoryDescrition, int MinAge, int MaxAge, string OrganizerEmail)
        {//start of method

            HttpContext.Session.SetString("CategoryDescrition",CategoryDescrition);

            cat1 = new CatergoriesModel()
            {
               // CategoryName = ,
               CategoryDescrition = CategoryDescrition,
               MinAge = MinAge,
               MaxAge=MaxAge
              

            };

            Categories Cat = new Categories();
            string Cate = Cat.CategoriesInfo(CategoryName.ToString(),cat1.CategoryDescrition,cat1.MinAge,cat1.MaxAge,OrganizerEmail);

            return Cate;

        }//end of method



        //httpGet request to Get the catergories detailes from db
        [HttpGet("all Categories")]

        public string CateGory_Info()
        {//start of method

            Categories Catinfo = new Categories();
            string Cateinfo = Catinfo.CateGoryInfo();

            return Cateinfo;

        }//end of method



        //httpGet request to Get the Catergories detailes from db
        [HttpGet("{CatergoryId}")]

        public string CateGoryInfo_byId(int CatergoryId)
        {//start of method

            cat1 = new CatergoriesModel()
            {
                CatergoryId = CatergoryId
            };

            Categories Catinfo = new Categories();
            string CateinfoId = Catinfo.CateGoryInfo_Id(cat1.CatergoryId);

            return CateinfoId;

        }//end of method



        //httpPut request to Get the Catergories detailes from db
        [HttpPut("update/{CatergoryId}")]

        public string UpdateCateInfo_byId(int CatergoryId, catergoryUpdate CatergoryName, string CategoryDescrition, int MinAge, int MaxAge)
        {//start of method

            cat1 = new CatergoriesModel()
            {
                CatergoryId = CatergoryId,
                CategoryDescrition = CategoryDescrition,
                MinAge = MinAge,
                MaxAge = MaxAge


            };

            Categories Catinfo = new Categories();
            string CateinfoId = Catinfo.Catergoryupdate_byId(cat1.CatergoryId,CatergoryName.ToString(),cat1.CategoryDescrition,cat1.MinAge,cat1.MaxAge);

            return CateinfoId;

        }//end of method


        //httpGet request to Get the Catergories detailes from db
        [HttpDelete("delete/{CatergoryId}")]

        public string DeleteCategoryInfo_byId(int CatergoryId)
        {//start of method

            Categories Catinfo = new Categories();
            string CateinfoId = Catinfo.DeleteCategory_byId(CatergoryId);

            return CateinfoId;

        }//end of method

        //HttpGet request to get all categories as JSON
        [HttpGet("all")]
        public List<CatergoriesModel> GetAllCategories()
        {//start of method

            Categories Catinfo = new Categories();
            return Catinfo.GetAllCategoriesJson();

        }//end of method


       
    }
}
