using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Master_Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string mySession = string.Empty;

        if (HttpContext.Current.Session == null && HttpContext.Current.Session["VALIDUSERID"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        else
        {
            mySession = (string)HttpContext.Current.Session["VALIDUSERID"];
            if (mySession == null)
            {
                Response.Redirect("Index.aspx");
            }
            else
            {
                if (mySession == string.Empty)
                {
                    Response.Redirect("Index.aspx");
                }
            }
        }

        if (!this.IsPostBack)
        {
            DataTable dt = this.GetData(mySession, 0);
            PopulateMenu(dt, 0, null, mySession);
            this.lblUserName.Text = getUserName(mySession);
        }
    }

    protected void LinkLogoff_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Index.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Summary.aspx?Type=SEARCH&CriteriaType="+this.ddlSearch.SelectedItem.Value+"&CriteriaValue="+this.txtSearch.Text);
    }

    private void PopulateMenu(DataTable dt, int parentMenuId, MenuItem parentMenuItem, string username)
    {
        string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
        foreach (DataRow row in dt.Rows)
        {
            MenuItem menuItem = new MenuItem
            {
                Value = row["MenuId"].ToString(),
                Text = row["Title"].ToString(),
                NavigateUrl = row["Url"].ToString(),
                Selected = row["Url"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
            };
            if (parentMenuId == 0)
            {
                Menu1.Items.Add(menuItem);
                DataTable dtChild = this.GetData(username, int.Parse(menuItem.Value));
                PopulateMenu(dtChild, int.Parse(menuItem.Value), menuItem, username);
            }
            else
            {
                parentMenuItem.ChildItems.Add(menuItem);
            }
        }
    }

    private DataTable GetData(string username, int parentMenuId)
    {
        DataTable dt = new DataTable();

        string cmdText = "dbo.GetMenuList";
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@ParentMenuId", parentMenuId);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dt);
            }
            conn.Close();
        }

        return dt;
    }

    protected string getUserName(string userid)
    {
        string wname = string.Empty;

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.GetUserName";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        wname = reader[0].ToString();
                    }
                }
                conn.Close();
            }
        }

        return wname;
    }
}
