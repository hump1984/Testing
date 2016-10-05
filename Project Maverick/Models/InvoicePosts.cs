using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Maverick.Models
{
    public class InvoicePosts
    {
        public int ID { get; set; }
        
        [ForeignKey("InvoiceModelNEAS")]
        public int InvoiceID { get; set; }  // One-to-One
        
        [ForeignKey("ContractPostModels")]
        public int ContractPostID { get; set; }  // One-to-One  // Holds static info posts
        public decimal Amount { get; set; } //Adds non-static info
        public decimal Total { get; set; } //Set this in code on creation Amount*Contractpost.Price -  Possibly dont store it at all.

        //public virtual Project Project { get; set; }
        public virtual InvoiceModelNEAS InvoiceModelNEAS { get; set; }
        public virtual ContractPostModel ContractPostModels { get; set; }



    }

}