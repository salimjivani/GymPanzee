using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymPanzee.Models;
using System.Web.Services;
using System.Web.Script.Services;

namespace GymPanzee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ExerciseMachines()
        {
            GympanzeeDBDataContext userstable = new GympanzeeDBDataContext();

            var exercisemachinemodel = userstable.ExerciseMachines.ToList();

            var testinglist = new List<Rootobject>();

            foreach (var a in exercisemachinemodel)
            {
                Rootobject testing = new Rootobject()
                {
                    IDjson = a.ID,
                    ExerciseCategoryjson = a.ExerciseCategoryID.ToString(),
                    Typejson = a.Type
                };
                testinglist.Add(testing);
            }

            return Json(testinglist, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult InsertActivities(Activity model)
        {
            GympanzeeDBDataContext insertactivitiesDB = new GympanzeeDBDataContext();

            insertactivitiesDB.insertactivity(model.UserID, model.FacilityID, model.ExerciseMachineID, model.Reps, model.Weights, model.Time, model.Other);

            return Json("saved", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Activities(int User, int Machine)
        {
            GympanzeeDBDataContext Pastactivity = new GympanzeeDBDataContext();

            var ActivityDataSet = (from a in Pastactivity.Activities where a.UserID == User && a.ExerciseMachineID == Machine orderby a.Date descending select new { a.UserID, a.ExerciseMachineID, a.FacilityID, a.Reps, a.Weights, a.Time, a.Other, a.Date}).ToList();

            var PrevActivity = new List<Activity>();

            foreach (var a in ActivityDataSet)
            {
                Activity PreviousActivityjson = new Activity()
                {
                    Other = a.Other,
                    ExerciseMachineID = a.ExerciseMachineID,
                    UserID = a.UserID,
                    Reps = a.Reps,
                    Weights = a.Weights,
                    Time = a.Time,
                    Date = a.Date
                };
                PrevActivity.Add(PreviousActivityjson);
            }
            
            return Json(PrevActivity, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Activity()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}