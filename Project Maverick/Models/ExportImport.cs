using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CsvHelper;
using Project_Maverick.Models;

namespace Project_Maverick.CSVReader
{
        internal class Program
        {
            private static void Main(string[] args)
            {
                using (var sr = new StreamReader(@"hours.csv"))
                {
                    var reader = new CsvReader(sr);

                    //CSVReader will now read the whole file into an enumerable
                    IEnumerable<Hour> records = reader.GetRecords<Hour>();

                    //First 5 records in CSV file will be printed to the Output Window
                    foreach (Hour record in records.Take(5))
                    {
                        Debug.Print("{0} {1}, {2}, {3}", record.RegDateTime, record.user, record.Amount,
                            record.RegType, record.project, record.Comment); //Need to check this.
                    }
                }
            }

            //Finish this and add imports for projects, machines, users, hours.
            //Need xml import/export for Project/Job-files
            //Need PDF Exports for invoice/cost/hour reports etc.
           
        }
}

