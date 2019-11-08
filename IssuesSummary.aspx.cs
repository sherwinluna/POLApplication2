using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class IssuesSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
            Panel masterPnl = (Panel)this.Master.FindControl("pnlSearch");
            masterPnl.Visible = false;

            Page.Title = "Incident Summary";
            ShowGrid("0");

            checkPermission();
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void gvSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow selectedRow = this.gvSummary.Rows[index];
            TableCell id = selectedRow.Cells[1];
            
            Response.Redirect("IssuesDetail.aspx?Id=" + id.Text.ToUpper() + "&Action=edit");
        }
    }

    protected void ShowGrid(string id)
    {
        this.gvSummary.DataSource = null;

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Summary4";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    this.gvSummary.DataSource = ds;
                    this.gvSummary.DataBind();
                }
            }
        }
    }

    protected void lplus_Click(object sender, EventArgs e)
    {
        Response.Redirect("IssuesDetail.aspx?Id=&Action=add");
    }

    protected void checkPermission()
    {
        bool isReadOnly = Convert.ToBoolean(Session["ISREADONLYINCIDENT"]);

        if (isReadOnly == true)
        {
            lplus.Visible = false;
        }
    }

    protected void btnSearchIncident_Click(object sender, EventArgs e)
    {
        this.gvSummary.DataSource = null;

        if (this.txtSearchIncident.Visible == true)
        {
            using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
            {
                string cmdText = "dbo.SelectIncidentByDescription";
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Detailed_Issue", this.txtSearchIncident.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        this.gvSummary.DataSource = ds;
                        this.gvSummary.DataBind();
                    }
                }
            }
        }

        if (this.ddSearchStatus.Visible == true)
        {
            using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
            {
                string cmdText = "dbo.SelectIncidentByStatus";
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", this.ddSearchStatus.SelectedValue);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        this.gvSummary.DataSource = ds;
                        this.gvSummary.DataBind();
                    }
                }
            }
        }

        if (this.txtSearchIncidentRaisedBy.Visible == true)
        {
            using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
            {
                string cmdText = "dbo.SelectIncidentByRaisedBy";
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RaisedBy", this.txtSearchIncidentRaisedBy.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        this.gvSummary.DataSource = ds;
                        this.gvSummary.DataBind();
                    }
                }
            }
        }
    }

    protected void ddSearchIncident_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddSearchIncident.SelectedValue == "Incident")
        {
            this.ddSearchStatus.Visible = false;
            this.txtSearchIncident.Visible = true;
            this.txtSearchIncidentRaisedBy.Visible = false;
        }
        else if (this.ddSearchIncident.SelectedValue == "Status")
        {
            this.ddSearchStatus.Visible = true;
            this.txtSearchIncident.Visible = false;
            this.txtSearchIncidentRaisedBy.Visible = false;
        }
        else if (this.ddSearchIncident.SelectedValue == "Raised By")
        {
            this.ddSearchStatus.Visible = false;
            this.txtSearchIncident.Visible = false;
            this.txtSearchIncidentRaisedBy.Visible = true;
        }
    }
}