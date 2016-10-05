using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Maverick.Models
{
    public class ContractPostModel
    {

        public int ID { get; set; }
        public string PostCode { get; set; }
        public string Header { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }

    }
}