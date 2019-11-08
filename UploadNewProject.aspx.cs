using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


public partial class UploadNewProject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "New Project Request";

        hfUserId.Value = Session["VALIDUSERID"].ToString();

        checkPermission();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        /*
            Note: Excel with issues should be converted to excel workbook
        */
        try
        {
            string fileName = Server.MapPath("~/Files/") + Path.GetFileName(fuUpload.PostedFile.FileName);
            fuUpload.SaveAs(fileName);

            string project_name = GetCellValue(fileName, "POL New Project Request", "E8");
            string project_number = GetCellValue(fileName, "POL New Project Request", "I10");

            string sbu = GetCellValue(fileName, "POL New Project Request", "E9");
            string business_line = GetCellValue(fileName, "POL New Project Request", "E10");
            string responsible_office = GetCellValue(fileName, "POL New Project Request", "E11");

            File.Delete(fileName);

            InsertProjectCWX(project_name.ToUpper(), project_number.ToUpper(), sbu.ToUpper(), business_line.ToUpper(), responsible_office.ToUpper());

            Response.Redirect("Details.aspx?ProjectNo=" + project_number.ToUpper() + "&Action=edit&Type=");
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    public static string GetCellValue(string fileName, string sheetName, string addressName)
    {
        string value = null;

        // Open the spreadsheet document for read-only access.
        using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
        {
            // Retrieve a reference to the workbook part.
            WorkbookPart wbPart = document.WorkbookPart;

            // Find the sheet with the supplied name, and then use that 
            // Sheet object to retrieve a reference to the first worksheet.
            Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().
              Where(s => s.Name == sheetName).FirstOrDefault();

            // Throw an exception if there is no sheet.
            if (theSheet == null)
            {
                throw new ArgumentException("sheetName");
            }

            // Retrieve a reference to the worksheet part.
            WorksheetPart wsPart =
                (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

            // Use its Worksheet property to get a reference to the cell 
            // whose address matches the address you supplied.
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().
              Where(c => c.CellReference == addressName).FirstOrDefault();

            // If the cell does not exist, return an empty string.
            if (theCell != null)
            {
                value = theCell.InnerText;

                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that 
                // represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code 
                // looks up the corresponding value in the shared string 
                // table. For Booleans, the code converts the value into 
                // the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:

                            // For shared strings, look up the value in the
                            // shared strings table.
                            var stringTable =
                                wbPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            // If the shared string table is missing, something 
                            // is wrong. Return the index that is in
                            // the cell. Otherwise, look up the correct text in 
                            // the table.
                            if (stringTable != null)
                            {
                                value =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(value)).InnerText;
                            }
                            break;

                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }
        }
        return value;
    }

    public void InsertProjectCWX(string project_name, string project_number, string sbu, string business_line, string reponsible_office)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.InsertCWX";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectLocation", DBNull.Value);
                cmd.Parameters.AddWithValue("@DataCenter", DBNull.Value);
                cmd.Parameters.AddWithValue("@SBU", sbu);
                cmd.Parameters.AddWithValue("@BusinessLine", business_line);
                cmd.Parameters.AddWithValue("@ProjectNumber", project_number);
                cmd.Parameters.AddWithValue("@ProjectName", project_name);
                cmd.Parameters.AddWithValue("@RequestDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                cmd.Parameters.AddWithValue("@Priority", DBNull.Value);
                cmd.Parameters.AddWithValue("@ForecastCloseDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@PlanForCloseout", DBNull.Value);
                cmd.Parameters.AddWithValue("@InboundTVend", DBNull.Value);
                cmd.Parameters.AddWithValue("@InboundTEngg", DBNull.Value);
                cmd.Parameters.AddWithValue("@DocDistribution", DBNull.Value);
                cmd.Parameters.AddWithValue("@Squadcheck", DBNull.Value);
                cmd.Parameters.AddWithValue("@OutboundTransmittal", DBNull.Value);
                cmd.Parameters.AddWithValue("@WebAPI", DBNull.Value);
                cmd.Parameters.AddWithValue("@Brava", DBNull.Value);
                cmd.Parameters.AddWithValue("@MDR", DBNull.Value);
                cmd.Parameters.AddWithValue("@SetupDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@SetUpAssignedTo", DBNull.Value);
                cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    protected void checkPermission()
    {
        bool isReadOnly = Convert.ToBoolean(Session["ISREADONLYPROJECT"]);

        if (isReadOnly == true)
        {
            this.btnUpload.Visible = false;
        }
    }
}