using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class NewProjectLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Page.Title = "New Project Location";
            checkPermission();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.NewProjectLocation";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectLocation", this.txtProjectLocation.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        this.txtProjectLocation.Text = "";
    }

    protected void checkPermission()
    {
        bool isReadOnly = Convert.ToBoolean(Session["ISREADONLYPROJECT"]);

        if (isReadOnly == true)
        {
            btnSave.Visible = false;
        }
    }
}