using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Maverick.Models
{
    public class InvoiceModelNEAS
    {
        public InvoiceModelNEAS()
        {
            DateCreated = DateTime.Now;   //Adds creation date on create
            //Add current user to CreatedByUserID
        }
        
        public int ID { get; set; }
        public DateTime DateCreated { get; private set; }
        
        //[ForeignKey("AspNetUsers")]
        //public int CreatedByUserID { get; set; }   // One-to-One
        public int InvoiceNumber { get; set; }
        
        [ForeignKey("Projects")]    // One-to-One
        public int ProjectID { get; set; }        

        //Controlled Invoice
        
        //[ForeignKey("AspNetUsers")]
        //public int ControlledByUserID { get; set; } // One-to-One
        public DateTime ControlDate { get; set; }
        public bool Controlled { get; set; }

        //Approved Invoice
        
        // [ForeignKey("AspNetUsers")]
        // public int ApproveByUserID { get; set; }  // One-to-One
        public bool Approved { get; set; }
        public DateTime ApproveDate { get; set; }  

        //Content
        public virtual ICollection<InvoicePosts> InvoicePosts { get; set; }
        public virtual Project Projects { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }
        public virtual ApplicationUser ApproveByUser { get; set; }
        public virtual ApplicationUser ControlledByUser { get; set; }

    }
}