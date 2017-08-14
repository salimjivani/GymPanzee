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

    public class InsertActivtyData
    {
        public int userids { get; set; }
        public int facilityids { get; set; }
        public int exercisemachineids { get; set; }
        public int repss { get; set; }
        public int weightss { get; set; }
        public int time { get; set; }
        public string others { get; set; }
    }

    public class PreviousActivity
    {
        public int? Userid { get; set; }
        public int? ExerciseMachineID { get; set; }
        public int? Reps { get; set; }
        public int? Weights { get; set; }
        public int? Time { get; set; }
        public string Other { get; set; }
    }

}