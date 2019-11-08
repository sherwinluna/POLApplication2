using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class UploadProjectCSV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Upload Data";

            checkPermission();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string status = string.Empty;
        if (rdPOL.Checked == true)
        {
            uploadPOL();
            status = "POL Uploaded!";
        }

        if (rdCWX.Checked == true)
        {
            uploadCWX();
            status = "CWX Uploaded!";
        }

        if (rdCLS.Checked == true)
        {
            uploadCLS();
            status = "Closed Projects Uploaded!";
        }

        if (rdActivity.Checked == true)
        {
            uploadActivity();
            updateDMSSize();
            status = "Activity Tablespace Uploaded!";
        }

        if (rdIncidentsLog.Checked == true)
        {
            UploadIncidentsLog();
            status = "Incident Logs Uploaded!";
        }

        Label1.Text = status;
    }

    private void uploadPOL()
    {
        string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(fuUpload.PostedFile.FileName);
        fuUpload.SaveAs(csvPath);

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[27]
        {
            new DataColumn("Project_Location", typeof(string)),
            new DataColumn("Project_Count", typeof(string)),
            new DataColumn("Data_Center",typeof(string)),
            new DataColumn("Project_No", typeof(string)),
            new DataColumn("Project_Name", typeof(string)),
            new DataColumn("Request_Date",typeof(string)),
            new DataColumn("Forecast_Close_Date", typeof(string)),
            new DataColumn("Migrate_To_CWX", typeof(string)),
            new DataColumn("Migrate_To_CWX_Cnt",typeof(string)),
            new DataColumn("Inactive_1", typeof(string)),
            new DataColumn("Inactive_2", typeof(string)),
            new DataColumn("Plan_For_Closeout",typeof(string)),
            new DataColumn("ERR_Request_Date", typeof(string)),
            new DataColumn("Delete_Request_Date", typeof(string)),
            new DataColumn("Inactive_Project_For",typeof(string)),
            new DataColumn("Migration_Plan", typeof(string)),
            new DataColumn("Column1", typeof(string)),
            new DataColumn("Column2",typeof(string)),
            new DataColumn("Column3", typeof(string)),
            new DataColumn("Notes", typeof(string)),
            new DataColumn("Global_Template_Version",typeof(string)),
            new DataColumn("No_Active_PCS", typeof(string)),
            new DataColumn("EDR1", typeof(string)),
            new DataColumn("AP_Data_Center",typeof(string)),
            new DataColumn("EDR2", typeof(string)),
            new DataColumn("ERR_Use", typeof(string)),
            new DataColumn("No_ERR_Docs",typeof(string))
        });

        string csvData = File.ReadAllText(csvPath);
        foreach (string row in csvData.Split('\n'))
        {
            if (!string.IsNullOrEmpty(row))
            {
                dt.Rows.Add();
                int i = 0;
                foreach (string cell in row.Split(','))
                {
                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                    i++;
                }
            }
        }

        using (SqlConnection con = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                sqlBulkCopy.DestinationTableName = "dbo.tblStagePOL";
                con.Open();
                sqlBulkCopy.WriteToServer(dt);
                con.Close();
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Bulk_Upload1";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    private void uploadCWX()
    {
        string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(fuUpload.PostedFile.FileName);
        fuUpload.SaveAs(csvPath);

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[21] {
            new DataColumn("Project_Location", typeof(string)),
            new DataColumn("Project_Count", typeof(string)),
            new DataColumn("Data_Center",typeof(string)),
            new DataColumn("Project_No", typeof(string)),
            new DataColumn("Project_Name", typeof(string)),
            new DataColumn("Request_Date",typeof(string)),
            new DataColumn("Status", typeof(string)),
            new DataColumn("Notes", typeof(string)),
            new DataColumn("Priority",typeof(string)),
            new DataColumn("Forecast_Clos_Date", typeof(string)),
            new DataColumn("Plan_For_Closeout", typeof(string)),
            new DataColumn("SBU",typeof(string)),
            new DataColumn("Business_Line", typeof(string)),
            new DataColumn("ITV", typeof(string)),
            new DataColumn("ITE",typeof(string)),
            new DataColumn("Doc_Distribution", typeof(string)),
            new DataColumn("Squadcheck", typeof(string)),
            new DataColumn("Outboiund_Transmittal",typeof(string)),
            new DataColumn("Web_API", typeof(string)),
            new DataColumn("Brava", typeof(string)),
            new DataColumn("MDR",typeof(string))
            });

        string csvData = File.ReadAllText(csvPath);
        foreach (string row in csvData.Split('\n'))
        {
            if (!string.IsNullOrEmpty(row))
            {
                dt.Rows.Add();
                int i = 0;
                foreach (string cell in row.Split(','))
                {
                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                    i++;
                }
            }
        }

        using (SqlConnection con = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                sqlBulkCopy.DestinationTableName = "dbo.tblStageCWX";
                con.Open();
                sqlBulkCopy.WriteToServer(dt);
                con.Close();
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Bulk_Upload2";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    private void uploadCLS()
    {
        string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(fuUpload.PostedFile.FileName);
        fuUpload.SaveAs(csvPath);

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[8] {
            new DataColumn("Project_Location", typeof(string)),
            new DataColumn("Project_Count", typeof(string)),
            new DataColumn("Project_No", typeof(string)),
            new DataColumn("Project_Name", typeof(string)),
            new DataColumn("Project_Request_Date",typeof(string)),
            new DataColumn("Project_Close_Date", typeof(string)),
            new DataColumn("Project_Type", typeof(string)),
            new DataColumn("Project_Duration_Years",typeof(string))
            });

        string csvData = File.ReadAllText(csvPath);
        foreach (string row in csvData.Split('\n'))
        {
            if (!string.IsNullOrEmpty(row))
            {
                dt.Rows.Add();
                int i = 0;
                foreach (string cell in row.Split(','))
                {
                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                    i++;
                }
            }
        }

        using (SqlConnection con = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                sqlBulkCopy.DestinationTableName = "dbo.tblStageClose";
                con.Open();
                sqlBulkCopy.WriteToServer(dt);
                con.Close();
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Bulk_Upload3";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    private void uploadActivity()
    {
        string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(fuUpload.PostedFile.FileName);
        fuUpload.SaveAs(csvPath);

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[10] {
            new DataColumn("Project_No", typeof(string)),
            new DataColumn("Project_Name", typeof(string)),
            new DataColumn("Project", typeof(string)),
            new DataColumn("Location", typeof(string)),
            new DataColumn("Data_Center",typeof(string)),
            new DataColumn("Region", typeof(string)),
            new DataColumn("LastSeen", typeof(string)),
            new DataColumn("Size_in_MB",typeof(string)),
            new DataColumn("TS_FreeMB",typeof(string)),
            new DataColumn("Activity",typeof(string))
            });

        string csvData = File.ReadAllText(csvPath);
        foreach (string row in csvData.Split('\n'))
        {
            if (!string.IsNullOrEmpty(row))
            {
                dt.Rows.Add();
                int i = 0;
                foreach (string cell in row.Split('\t'))
                {
                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                    i++;
                }
            }
        }

        using (SqlConnection con = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                sqlBulkCopy.DestinationTableName = "dbo.DMS_ActivityTablespace";
                con.Open();
                sqlBulkCopy.WriteToServer(dt);
                con.Close();
            }
        }
    }

    private void UploadIncidentsLog()
    {
        string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(fuUpload.PostedFile.FileName);
        fuUpload.SaveAs(csvPath);

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[15] {
            new DataColumn("ReqNo", typeof(string)),
            new DataColumn("RaisedBy", typeof(string)),
            new DataColumn("Office", typeof(string)),
            new DataColumn("Project", typeof(string)),
            new DataColumn("Area",typeof(string)),
            new DataColumn("Date", typeof(string)),
            new DataColumn("Time", typeof(string)),
            new DataColumn("Detailed_Issue",typeof(string)),
            new DataColumn("POL_Coreworx",typeof(string)),
            new DataColumn("Status",typeof(string)),
            new DataColumn("AssignedTo",typeof(string)),
            new DataColumn("Solution",typeof(string)),
            new DataColumn("DateCompleted",typeof(string)),
            new DataColumn("TimeCompleted",typeof(string)),
            new DataColumn("CompletedBy",typeof(string))
            });

        string csvData = File.ReadAllText(csvPath);
        foreach (string row in csvData.Split('\n'))
        {
            if (!string.IsNullOrEmpty(row))
            {
                dt.Rows.Add();
                int i = 0;
                foreach (string cell in row.Split(','))
                {
                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                    i++;
                }
            }
        }

        using (SqlConnection con = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                sqlBulkCopy.DestinationTableName = "dbo.tblStageIssues";
                con.Open();
                sqlBulkCopy.WriteToServer(dt);
                con.Close();
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

    protected void updateDMSSize()
    {
        using (SqlConnection con = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            using (SqlCommand command = con.CreateCommand())
            {
                command.CommandText = "dbo.updateDMSSize";
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}