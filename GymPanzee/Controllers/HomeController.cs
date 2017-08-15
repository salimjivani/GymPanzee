﻿using System;
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
        public JsonResult InsertUsers()
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
        public JsonResult InsertActivities(InsertActivtyData model)
        {
            GympanzeeDBDataContext insertactivitiesDB = new GympanzeeDBDataContext();

            insertactivitiesDB.insertactivity(model.userids, model.facilityids, model.exercisemachineids, model.repss, model.weightss, model.time, model.others);

            return Json("saved", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Activities(int User, int Machine)
        //public JsonResult Activities()
        {
            GympanzeeDBDataContext Pastactivity = new GympanzeeDBDataContext();

            var ActivityDataSet = (from a in Pastactivity.Activities where a.UserID == User && a.ExerciseMachineID == Machine orderby a.Date descending select new { a.UserID, a.ExerciseMachineID, a.FacilityID, a.Reps, a.Weights, a.Time, a.Other}).ToList();

            var PrevActivity = new List<PreviousActivity>();

            foreach (var a in ActivityDataSet)
            {
                PreviousActivity PreviousActivityjson = new PreviousActivity()
                {
                    Other = a.Other,
                    ExerciseMachineID = a.ExerciseMachineID,
                    Userid = a.UserID,
                    Reps = a.Reps,
                    Weights = a.Weights,
                    Time = a.Time
                };
                PrevActivity.Add(PreviousActivityjson);
            }
            
            return Json(PrevActivity, JsonRequestBehavior.AllowGet);
        }


        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}