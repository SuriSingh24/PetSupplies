using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PetSuppliesPlus.Framework
{
    /// <summary>
    /// Convert CSV to Datatable
    /// </summary>
    public static class CSVtoDatatable
    {
        public static DataTable ProcessCSV(string fileName)
        {
            //Set up our variables
            var dt = new DataTable();
            // work out where we should split on comma, but not in a sentence
            var r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            var sr = new StreamReader(fileName);

            //Read the first line and split the string at , with our regular expression in to an array
            string line = sr.ReadLine();
            string[] strArray = r.Split(line);

            //For each item in the new split array, dynamically builds our Data columns. Save us having to worry about it.
            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));

            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                DataRow row = dt.NewRow();

                //add our current value to our data row
                row.ItemArray = r.Split(line.Trim());

                dt.Rows.Add(row);
            }

            //Tidy Streameader up
            sr.Dispose();

            //return a the new DataTable
            return dt;

        }
        
     }
}