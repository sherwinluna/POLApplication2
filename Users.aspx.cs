using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.Title = "Users";
            showRoles();
            showUsers();

            checkPermission();
        }
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        string fname = Regex.Replace(this.txtFname.Text.ToLower(), @"^\w", m => m.Value.ToUpper());
        string lname = Regex.Replace(this.txtLname.Text.ToLower(), @"^\w", m => m.Value.ToUpper());
        int roleid = Convert.ToInt32(ddRole.SelectedItem.Value);

        string uname = findUserId(fname, lname).ToUpper();

        if (uname != null)
        {
            if (getUserDB(uname) == false)
            {
                addUser(uname, fname, lname, roleid);
                this.lblStatus.Text = "User " + fname + " " + lname + " was successfully added.";
            }
            else
            {
                this.lblStatus.Text = "Username already exist";
            }
        }
        else
        {
            this.lblStatus.Text = "User " + fname + " " + lname + " cannot be added. Please check if the name is correct.";
        }
    }

    private string findUserId(string fName, string lName)
    {
        string uid = null;

        using (var context = new PrincipalContext(ContextType.Domain, "FDNET.COM"))
        {
            using (var searcher = new PrincipalSearcher(new UserPrincipal(context) { GivenName = fName, Surname = lName }))
            {
                foreach (var result in searcher.FindAll())
                {
                    DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                    uid = de.Properties["samAccountName"].Value.ToString();
                }
            }
        }

        return uid;
    }

    private void showRoles()
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Roles";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    ddRole.DataSource = ds;
                    ddRole.DataTextField = "RoleName";
                    ddRole.DataValueField = "RoleId";
                    ddRole.DataBind();
                }
            }
        }

        ddRole.Items.Insert(0, new ListItem("", "0"));
    }

    private void addUser(string username, string firstname, string lastname, int roleid)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.InsertUser";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@FirstName", firstname);
                cmd.Parameters.AddWithValue("@LastName", lastname);
                cmd.Parameters.AddWithValue("@RoleId", roleid);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        Response.Redirect("Users.aspx");
    }

    private void showUsers()
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.GetUserList";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvUserList.DataSource = ds;
                    gvUserList.DataBind();
                }
            }
        }
    }

    protected void gvUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow selectedRow = this.gvUserList.Rows[index];
            TableCell username = selectedRow.Cells[1];

            using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
            {
                string cmdText = "dbo.DeleteUser";
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            //Response.Redirect("Users.aspx");
        }
    }

    protected bool getUserDB(string username)
    {
        bool valid = false;

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.GetUser";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
                conn.Close();
            }
        }

        return valid;
    }

    protected void checkPermission()
    {
        bool isReadOnly = Convert.ToBoolean(Session["ISREADONLYPROJECT"]);

        if (isReadOnly == true)
        {
            this.btnAddUser.Visible = false;
            this.gvUserList.Columns[0].Visible = false;
            this.Row1.Visible = false;
        }
    }
}