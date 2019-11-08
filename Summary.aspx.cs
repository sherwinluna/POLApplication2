using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!IsPostBack)
            //{
                Page.Title = "Project Summary";

                string type = Request.QueryString["Type"];
                if (type == "SEARCH")
                {
                    string CriteriaType = Request.QueryString["CriteriaType"];
                    string CriteriaValue = Request.QueryString["CriteriaValue"];

                    ShowGridsearch(CriteriaType, CriteriaValue);
                }
                else
                {
                    string id = Request.QueryString["Id"];
                    if (id == "13")
                    {
                        Response.Redirect("CloseProjects.aspx");
                    }
                    else
                    {
                        ShowGrid(id, 2015);
                    }
                }
            //}
        }
        catch (Exception ex)
        {

        }
    }

    protected void ShowGrid(string id, int year)
    {
        this.gvSummary.DataSource = null;

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Summary1";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Year", year.ToString());
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

    protected void gvSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow selectedRow = this.gvSummary.Rows[index];
            TableCell projectNumber = selectedRow.Cells[3];
            TableCell isCWXFlag = selectedRow.Cells[10];

            Response.Redirect("Details.aspx?ProjectNo=" + projectNumber.Text.ToUpper() + "&Flag=" + isCWXFlag.Text + "&Action=edit&Type=");
        }
    }

    protected void ShowGridsearch(string CriteriaType, string CriteriaValue)
    {
        this.gvSummary.DataSource = null;

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Search1";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CriteriaType", CriteriaType);
                cmd.Parameters.AddWithValue("@CriteriaValue", CriteriaValue);
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