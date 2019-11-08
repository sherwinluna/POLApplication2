using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class IssuesDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Panel masterPnl = (Panel)this.Master.FindControl("pnlSearch");
            masterPnl.Visible = false;

            this.Page.Title = "Incident Details";

            hfUserId.Value = Session["VALIDUSERID"].ToString();

            try
            {
                string id = Request.QueryString["Id"].ToString().ToUpper().Trim();
                string action = Request.QueryString["Action"].ToString().ToUpper().Trim();

                if (action == "EDIT")
                {
                    txtReqNo.ReadOnly = true;
                    this.pnlFiles.Visible = true;
                    this.btnSaveEdit.Visible = true;
                    ShowDetails(id);
                }

                if (action == "ADD")
                {
                    this.pnlFiles.Visible = false;
                    txtReqNo.ReadOnly = true;
                    this.btnSaveAdd.Visible = true;
                }

                ShowFiles(id);

                checkPermission();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
            }
        }
    }

    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.InsertIncident";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RaisedBy", this.txtRaisedBy.Text);
                cmd.Parameters.AddWithValue("@Office", this.txtOffice.Text);
                cmd.Parameters.AddWithValue("@Project", this.txtProject.Text);
                cmd.Parameters.AddWithValue("@Area", this.txtArea.Text);
                if (this.txtDate.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@Date", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Date", this.txtDate.Text);
                }
                if (this.txtTime.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@Time", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Time", this.txtTime.Text);
                }
                cmd.Parameters.AddWithValue("@Detailed_Issue", this.txtIssue.Text);
                cmd.Parameters.AddWithValue("@POL_Coreworx", this.ddPolCwx.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Status", this.ddStatus.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@AssignedTo", this.txtAssignedTo.Text);
                cmd.Parameters.AddWithValue("@Solution", this.txtSolution.Text);
                if (this.txtDate.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@DateCompleted", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DateCompleted", this.txtDateCompleted.Text);
                }
                if (this.txtTime.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@TimeCompleted", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TimeCompleted", this.txtTimeCompleted.Text);
                }
                cmd.Parameters.AddWithValue("@CompletedBy", this.txtCompletedBy.Text);
                cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        Response.Redirect("IssuesSummary.aspx");
    }

    protected void btnSaveEdit_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.UpdateIncident";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqNo", this.txtReqNo.Text);
                cmd.Parameters.AddWithValue("@RaisedBy", this.txtRaisedBy.Text);
                cmd.Parameters.AddWithValue("@Office", this.txtOffice.Text);
                cmd.Parameters.AddWithValue("@Project", this.txtProject.Text);
                cmd.Parameters.AddWithValue("@Area", this.txtArea.Text);
                if (this.txtDate.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@Date", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Date", this.txtDate.Text);
                }
                if (this.txtTime.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@Time", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Time", this.txtTime.Text);
                }
                cmd.Parameters.AddWithValue("@Detailed_Issue", this.txtIssue.Text);
                cmd.Parameters.AddWithValue("@POL_Coreworx", this.ddPolCwx.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Status", this.ddStatus.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@AssignedTo", this.txtAssignedTo.Text);
                cmd.Parameters.AddWithValue("@Solution", this.txtSolution.Text);
                if (this.txtDate.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@DateCompleted", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DateCompleted", this.txtDateCompleted.Text);
                }
                if (this.txtTime.Text == string.Empty)
                {
                    cmd.Parameters.AddWithValue("@TimeCompleted", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TimeCompleted", this.txtTimeCompleted.Text);
                }
                cmd.Parameters.AddWithValue("@CompletedBy", this.txtCompletedBy.Text);
                cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    protected void ShowDetails(string ReqNo)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.SelectIncident";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqNo", ReqNo);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.txtReqNo.Text = reader[0].ToString();
                    this.txtRaisedBy.Text = reader[1].ToString();
                    this.txtOffice.Text = reader[2].ToString();
                    this.txtProject.Text = reader[3].ToString();
                    this.txtArea.Text = reader[4].ToString();
                    this.txtDate.Text = reader[5].ToString();
                    this.txtTime.Text = reader[6].ToString();
                    this.txtIssue.Text = reader[7].ToString();
                    this.ddPolCwx.SelectedItem.Text = reader[8].ToString();
                    this.ddStatus.SelectedItem.Text = reader[9].ToString();
                    this.txtAssignedTo.Text = reader[10].ToString();
                    this.txtSolution.Text = reader[11].ToString();
                    this.txtDateCompleted.Text = reader[12].ToString();
                    this.txtTimeCompleted.Text = reader[13].ToString();
                    this.txtCompletedBy.Text = reader[14].ToString();
                }
                reader.Close();
            }
        }
    }

    protected void ShowFiles(string projectNo)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.SelectIncidentFiles";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectNumber", projectNo);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvFiles.DataSource = ds;
                    gvFiles.DataBind();
                }
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(fuUpload.PostedFile.FileName);
        string contentType = fuUpload.PostedFile.ContentType;
        using (Stream fs = fuUpload.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);

                using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
                {
                    string cmdText = "dbo.InsertIncidentFile";
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectNumber", this.txtReqNo.Text);
                        cmd.Parameters.AddWithValue("@DocumentFileName", filename);
                        cmd.Parameters.AddWithValue("@DocumentDescription", this.txtUpload.Text);
                        cmd.Parameters.AddWithValue("@DocumentOwner", hfUserId.Value);
                        cmd.Parameters.AddWithValue("@DocumentContent", contentType);
                        cmd.Parameters.AddWithValue("@DocumentFile", bytes);
                        cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.DownloadIncidentFile";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    bytes = (byte[])reader["DocumentFile"];
                    contentType = reader["DocumentContent"].ToString();
                    fileName = reader["DocumentFileName"].ToString();
                }
                conn.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.DeleteIncidentFile";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void checkPermission()
    {
        bool isReadOnly = Convert.ToBoolean(Session["ISREADONLYINCIDENT"]);

        if (isReadOnly == true)
        {
            btnSaveAdd.Visible = false;
            btnSaveEdit.Visible = false;
            pnlFiles.Visible = false;
        }
    }
}