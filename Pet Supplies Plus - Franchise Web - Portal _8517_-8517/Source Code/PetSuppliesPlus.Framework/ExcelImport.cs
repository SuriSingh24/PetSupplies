using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Web;
using System.Linq;

using OfficeOpenXml;
using System.Xml.XPath;

namespace PetSuppliesPlus.Framework
{
    public class ExcelImport
    {
        public static DataSet ImportExcelXLS(HttpPostedFile file, bool hasHeaders)
        {
            string fileName = Path.GetTempFileName();
            file.SaveAs(fileName);

            return ImportExcelXLS(fileName, hasHeaders, file.FileName);
        }
        

        public static Int32 getMaximumRowNumber(ExcelWorksheet worksheet)
        {
            XPathNavigator nav = worksheet.WorksheetXml.CreateNavigator();
            XPathExpression exp = nav.Compile("//*[name()='row']/@r");
            exp.AddSort("../@r", XmlSortOrder.Descending, XmlCaseOrder.None, "", XmlDataType.Number);
            XmlNode node = nav.SelectSingleNode(exp).UnderlyingObject as XmlNode;
            return Convert.ToInt32(node.InnerText);
        }

        public static Int32 getMaximumCellColumn(ExcelWorksheet worksheet)
        {
            XPathNavigator nav = worksheet.WorksheetXml.CreateNavigator();
            XPathExpression exp = nav.Compile("//*[name()='c']/@colNumber");
            exp.AddSort("../@colNumber", XmlSortOrder.Descending, XmlCaseOrder.None, "", XmlDataType.Number);
            XmlNode node = nav.SelectSingleNode(exp).UnderlyingObject as XmlNode;
            return Convert.ToInt32(node.InnerText);
        }


        public static DataSet ImportExcelXLS(string FileName, bool hasHeaders, string fname)
        {

            try
            {
                string HDR = hasHeaders ? "Yes" : "No";
                string strConn;
                string ext = Path.GetExtension(fname);
                if (ext.ToLower() == ".xls")
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 12.0 Xml;HDR=" + HDR + ";IMEX=2" + Convert.ToChar(34).ToString();
                //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 12.0;HDR=" + HDR + ";IMEX=2" + Convert.ToChar(34).ToString();
                // = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 12.0;HDR=Yes;IMEX=2" + Convert.ToChar(34).ToString();

                DataSet output = new DataSet();


                if (ext.ToLower() == ".xlsx")
                {

                    FileInfo file = new FileInfo(FileName);
                    using (ExcelPackage xlPackage = new ExcelPackage(file))
                    {
                        // get the first worksheet in the workbook
                        //ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                        foreach (ExcelWorksheet worksheet in xlPackage.Workbook.Worksheets)
                        {
                            //Int32 row = getMaximumRowNumber(worksheet);
                            //Int32 col = getMaximumCellColumn(worksheet);

                            if (worksheet.Dimension == null)
                            {
                                return output;
                            }
                            Int32 row = worksheet.Dimension.End.Row;
                            Int32 col = worksheet.Dimension.End.Column;
                            DataTable dt = new DataTable();

                            for (int iRow = 1; iRow < 2; iRow++)
                            {
                                for (int icol = 1; icol <= col; icol++)
                                {
                                    //string s = worksheet.Cells Cell(iRow, icol).Value;


                                    var s = worksheet.GetValue(iRow, icol);

                                    // string s=worksheet.GetValue(iRow,icol,string.Empty);
                                    // string s=worksheet.Cells[iRow,icol].Value.ToString();
                                    if (s != null)
                                    {

                                        if ((s.ToString().ToLower().Replace(" ", "") == "eventid") || (s.ToString().ToLower().Replace(" ", "") == "totaltickets") || (s.ToString().ToLower().Replace(" ", "") == "tickets(total)") || (s.ToString().ToLower().Replace(" ", "") == "basis") || (s.ToString().ToLower().Replace(" ", "") == "proceeds") || ((s.ToString().ToLower().Replace(" ", "") == "dealid")))
                                        {
                                            dt.Columns.Add(s.ToString(), System.Type.GetType("System.Double"));
                                        }
                                        else if (s.ToString().ToLower().Replace(" ", "") == "eventdate")
                                        {
                                            dt.Columns.Add(s.ToString(), System.Type.GetType("System.DateTime"));
                                        }
                                        else
                                        {
                                            dt.Columns.Add(s.ToString(), System.Type.GetType("System.String"));
                                        }
                                    }


                                }
                            }



                            // output the data in column 2
                            for (int iRow = 2; iRow <= row; iRow++)
                            {
                                DataRow dr;
                                dr = dt.NewRow();

                                for (int icol = 1; icol <= col; icol++)
                                {

                                    var s1 = worksheet.GetValue(iRow, icol);


                                    if (icol == 4)
                                    {
                                        s1 = worksheet.Cells[iRow, icol].Text.ToString();
                                    }
                                    if (s1 != null && s1 != "")
                                    {
                                        if (worksheet.GetValue(1, icol).ToString().ToLower().Replace(" ", "") == "eventdate")
                                        {
                                            s1 = worksheet.Cells[iRow, icol].Text.ToString();
                                            //DateTime _eventdate = DateTime.FromOADate(Convert.ToDouble(s1));
                                            dr[icol - 1] = Convert.ToDateTime(s1);
                                        }
                                        else
                                        {

                                            dr[icol - 1] = worksheet.Cells[iRow, icol].Value.ToString().Trim();
                                        }

                                        if (icol == 4)
                                        {
                                            dr[icol - 1] = worksheet.Cells[iRow, icol].Text.ToString().Trim();
                                        }
                                    }

                                }

                                dt.Rows.Add(dr);
                            }


                            DataTable stable = dt.AsEnumerable().Where(row1 => row1.ItemArray.Any(field => !(field is DBNull))).ToList().CopyToDataTable();

                            output.Tables.Add(stable);
                        }


                        return output;

                    }
                }


                else
                {


                    using (OleDbConnection conn = new OleDbConnection(strConn))
                    {
                        conn.Open();



                        DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                        foreach (DataRow row in dt.Rows)
                        {
                            string sheet = row["TABLE_NAME"].ToString();

                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                            cmd.CommandType = CommandType.Text;

                            DataTable outputTable = new DataTable(sheet);

                            //output.Tables.Add(outputTable);


                            new OleDbDataAdapter(cmd).Fill(outputTable);

                            var lst = outputTable.AsEnumerable().Where(row1 => row1.ItemArray.Any(field => !(field is DBNull))).ToList();
                            if (lst.Count() > 0)
                            {
                                DataTable s1 = lst.CopyToDataTable();
                                output.Tables.Add(s1);
                            }

                        }
                    }
                    return output;

                }
            }
            catch (Exception ee)
            {
                throw (ee);
            }


        }





        //public static string ImportExcelXLS(string FileName, bool hasHeaders,string fname)
        //{

        //    try
        //    {
        //        string HDR = hasHeaders ? "Yes" : "No";
        //        string strConn;
        //        string ext = Path.GetExtension(fname);
        //        if (ext.ToLower() == ".xls")
        //            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
        //        else
        //            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
        //        //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 12.0 Xml;HDR=" + HDR + ";IMEX=2" + Convert.ToChar(34).ToString();
        //        //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 12.0;HDR=" + HDR + ";IMEX=2" + Convert.ToChar(34).ToString();
        //        // = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 12.0;HDR=Yes;IMEX=2" + Convert.ToChar(34).ToString();

        //        DataSet output = new DataSet();



        //        if (ext.ToLower() == ".xlsx")
        //        {

        //            FileInfo file = new FileInfo(fname);

        //            using (ExcelPackage xlPackage = new ExcelPackage(file))
        //            {
        //                // get the first worksheet in the workbook

        //                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
        //                return "NO";
        //                Int32 row = getMaximumRowNumber(worksheet);
        //                Int32 col = getMaximumCellColumn(worksheet);


        //                return row.ToString() + col.ToString();
        //              //  DataTable dt = new DataTable();

        //              //  for (int iRow = 1; iRow < 2; iRow++)
        //              //  {
        //              //      for (int icol = 1; icol <= col; icol++)
        //              //      {
        //              //          string s = worksheet.Cell(iRow, icol).Value;
        //              //          if ((s.ToLower().Replace(" ", "") == "eventid") || (s.ToLower().Replace(" ", "") == "totaltickets") || (s.ToLower().Replace(" ", "") == "basis") || (s.ToLower().Replace(" ", "") == "proceeds") || ((s.ToLower().Replace(" ", "") == "dealid")))
        //              //          {
        //              //              dt.Columns.Add(s, System.Type.GetType("System.Double"));
        //              //          }
        //              //          else
        //              //          {
        //              //              dt.Columns.Add(s, System.Type.GetType("System.String"));
        //              //          }



        //              //      }
        //              //  }



        //              //  // output the data in column 2
        //              //  for (int iRow = 2; iRow <= row; iRow++)
        //              //  {

        //              //      DataRow dr = dt.NewRow();
        //              //      for (int icol = 1; icol <= col; icol++)
        //              //      {
        //              //          dr[icol - 1] = worksheet.Cell(iRow, icol).Value;


        //              //      }

        //              //      dt.Rows.Add(dr);
        //              //  }


        //              ////  DataTable s1 = dt.AsEnumerable().Where(row1 => row1.ItemArray.Any(field => !(field is DBNull))).ToList().CopyToDataTable();

        //              //  output.Tables.Add(dt);

        //              // // return output;

        //            }
        //        }
        //        return "No";


        //    }
        //    catch (Exception ee)
        //    {
        //        throw (ee);
        //    }


        //}



        struct ColumnType
        {
            public Type type;
            private string name;
            public ColumnType(Type type) { this.type = type; this.name = type.ToString().ToLower(); }
            public object ParseString(string input)
            {
                if (String.IsNullOrEmpty(input))
                    return DBNull.Value;
                switch (type.ToString())
                {
                    case "system.datetime":
                        return DateTime.Parse(input);
                    case "system.decimal":
                        return decimal.Parse(input);
                    case "system.boolean":
                        return bool.Parse(input);
                    default:
                        return input;
                }
            }
        }
        public static DataSet ImportExcelXML(HttpPostedFile file, bool hasHeaders, bool autoDetectColumnType)
        {
            return ImportExcelXML(file.InputStream, hasHeaders, autoDetectColumnType);
        }
        public static DataSet ImportExcelXML(Stream inputFileStream, bool hasHeaders, bool autoDetectColumnType)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new XmlTextReader(inputFileStream));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            nsmgr.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

            DataSet ds = new DataSet();

            foreach (XmlNode node in doc.DocumentElement.SelectNodes("//ss:Worksheet", nsmgr))
            {
                DataTable dt = new DataTable(node.Attributes["ss:Name"].Value);
                ds.Tables.Add(dt);
                XmlNodeList rows = node.SelectNodes("ss:Table/ss:Row", nsmgr);
                if (rows.Count > 0)
                {
                    List<ColumnType> columns = new List<ColumnType>();
                    int startIndex = 0;
                    if (hasHeaders)
                    {
                        foreach (XmlNode data in rows[0].SelectNodes("ss:Cell/ss:Data", nsmgr))
                        {
                            columns.Add(new ColumnType(typeof(string)));//default to text
                            dt.Columns.Add(data.InnerText, typeof(string));
                        }
                        startIndex++;
                    }
                    if (autoDetectColumnType && rows.Count > 0)
                    {
                        XmlNodeList cells = rows[startIndex].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            ColumnType autoDetectType = getType(cell.SelectSingleNode("ss:Data", nsmgr));

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                dt.Columns.Add("Column" + actualCellIndex.ToString(), autoDetectType.type);
                                columns.Add(autoDetectType);
                            }
                            else
                            {
                                dt.Columns[actualCellIndex].DataType = autoDetectType.type;
                                columns[actualCellIndex] = autoDetectType;
                            }

                            actualCellIndex++;
                        }
                    }
                    for (int i = startIndex; i < rows.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        XmlNodeList cells = rows[i].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            XmlNode data = cell.SelectSingleNode("ss:Data", nsmgr);

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                for (int ii = dt.Columns.Count; ii < actualCellIndex; ii++)
                                {
                                    dt.Columns.Add("Column" + actualCellIndex.ToString(), typeof(string));
                                    columns.Add(getDefaultType());
                                }
                                ColumnType autoDetectType = getType(cell.SelectSingleNode("ss:Data", nsmgr));
                                dt.Columns.Add("Column" + actualCellIndex.ToString(), typeof(string));
                                columns.Add(autoDetectType);
                            }
                            if (data != null)
                                row[actualCellIndex] = data.InnerText;

                            actualCellIndex++;
                        }

                        dt.Rows.Add(row);
                    }
                }
            }
            return ds;

            //<?xml version="1.0"?>
            //<?mso-application progid="Excel.Sheet"?>
            //<Workbook>
            // <Worksheet ss:Name="Sheet1">
            //  <Table>
            //   <Row>
            //    <Cell><Data ss:Type="String">Item Number</Data></Cell>
            //    <Cell><Data ss:Type="String">Description</Data></Cell>
            //    <Cell ss:StyleID="s21"><Data ss:Type="String">Item Barcode</Data></Cell>
            //   </Row>
            // </Worksheet>
            //</Workbook>
        }
        private static ColumnType getDefaultType()
        {
            return new ColumnType(typeof(String));
        }
        private static ColumnType getType(XmlNode data)
        {
            string type = null;
            if (data.Attributes["ss:Type"] == null || data.Attributes["ss:Type"].Value == null)
                type = "";
            else
                type = data.Attributes["ss:Type"].Value;

            switch (type)
            {
                case "DateTime":
                    return new ColumnType(typeof(DateTime));
                case "Boolean":
                    return new ColumnType(typeof(Boolean));
                case "Number":
                    return new ColumnType(typeof(Decimal));
                case "":
                    decimal test2;
                    if (data == null || String.IsNullOrEmpty(data.InnerText) || decimal.TryParse(data.InnerText, out test2))
                    {
                        return new ColumnType(typeof(Decimal));
                    }
                    else
                    {
                        return new ColumnType(typeof(String));
                    }
                default://"String"
                    return new ColumnType(typeof(String));
            }
        }
    }
}