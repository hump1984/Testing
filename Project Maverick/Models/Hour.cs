using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


using System.Data;
using System.Data.Entity;

using System.Net;

using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using Microsoft.AspNet.Identity;

using Project_Maverick.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project_Maverick.Models
{
    public class Hour
    {
        public Hour()
        {


        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegDateTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        public DateTime StartDateTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        public DateTime StopDateTime { get; set; }

        public int Amount { get; set; } //sum of time between start and stop, subtract time for breaks?
        //public string User_Id { get; set; } //Has one user
                
        public ApplicationUser user { get; set; }
        //public int Project_Id { get; set; } //Has one project
        public Project project { get; set; }
        public string Comment { get; set; }
        public HourRegType RegType { get; set; } //May need its own table in DB

        //Must have machineType, workType,

}

    public enum HourRegType
    {
        Normal,
        Overtime1,
        Overtime2
    }

}