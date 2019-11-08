using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class CloseProjects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Close Project Summary";
            ShowGrid();
        }
    }

    protected void ShowGrid()
    {
        this.gvSummary.DataSource = null;

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsCloseProject";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
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