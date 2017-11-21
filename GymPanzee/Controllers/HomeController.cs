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
        public JsonResult ExerciseMachines(int exercisemachineid)
        {
            GympanzeeDBDataContext userstable = new GympanzeeDBDataContext();

            var exercisecategory = (from em in userstable.ExerciseMachines
                                    join ec in userstable.ExerciseCategories on em.ExerciseCategoryID equals ec.ID
                                    join eq in userstable.ExerciseEquipmentCategories on ec.ExerciseEquipmentCategory equals eq.ID
                                    select new { em, ec, eq }).ToList();

            var exercisemachine = exercisecategory.Where(x => x.em.ID == exercisemachineid).ToList();

            var exerciselist = new List<Rootobject>();

            if (exercisemachine[0].eq.ID == 1)
            {
                Rootobject exercisemodel = new Rootobject()
                {
                    IDjson = exercisemachine[0].em.ID,
                    ExerciseCategoryjson = exercisemachine[0].em.ExerciseCategoryID.ToString(),
                    Typejson = exercisemachine[0].em.Type
                };
                exerciselist.Add(exercisemodel);
            }
            else
            {
                foreach (var ee in exercisecategory)
                {
                    if (ee.ec.ID == exercisemachine[0].ec.ID)
                    {
                        Rootobject exercisemodel = new Rootobject()
                        {
                            IDjson = ee.em.ID,
                            ExerciseCategoryjson = ee.em.ExerciseCategoryID.ToString(),
                            Typejson = ee.em.Type
                        };
                        exerciselist.Add(exercisemodel);
                    }
                }
            }

            return Json(exerciselist, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult GetUserID(string useremail)
        {
            GympanzeeDBDataContext Table = new GympanzeeDBDataContext();

            Table.Login(useremail);

            var UsersTable = (from a in Table.Users where a.Username == useremail select new { a.ID }).FirstOrDefault();

            var Username = new Activity()
            {
                UserID = UsersTable.ID
            };

            return Json(Username, JsonRequestBehavior.AllowGet);
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