using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymPanzee.Models
{
    public class Rootobject
    {
        public int IDjson { get; set; }
        public string ExerciseCategoryjson { get; set; }
        public string Typejson { get; set; }
    }

    public class Activity
    {
        public int? Userid { get; set; }
        public int? FacilityID { get; set; }
        public int? ExerciseMachineID { get; set; }
        public int? Reps { get; set; }
        public int? Weights { get; set; }
        public int? Time { get; set; }
        public string Other { get; set; }
        public string Date { get; set; }
    }

}