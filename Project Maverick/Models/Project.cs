using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Project_Maverick.Models
{
    public class Project
    {
        public Project()
        {
            CreatedAt = DateTime.Now; // Set creation date to NOW.


        }
        public int Id { get; set; }
        public int PID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt  { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<InvoiceModelNEAS> Invoices { get; set; }
            
    }
}