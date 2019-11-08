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

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = this.txtUsername.Text;
        string password = this.txtPassword.Text;

        if (getUserDB(username) == true)
        {
            if (getUserDomain(username, password) == true)
            {
                getUserPermission(username);

                Session.Add("VALIDUSERID", username);

                string mypage = getDefaultPage(username);
                Response.Redirect(mypage);
            }
            else
            {
                lblLoginRemark.Text = "Invalid credentials. Please use Domain userid and password.";
            }
        }
        else
        {
            lblLoginRemark.Text = "User have no permission.";
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

    protected bool getUserDomain(string username, string password)
    {
        bool valid = false;

        using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "FDNET"))
        {
            valid = context.ValidateCredentials(this.txtUsername.Text, this.txtPassword.Text);
        }

        return valid;
    }

    protected void getUserPermission(string username)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.GetPortalRolePermissionByUser";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        if (reader[3].ToString().ToUpper() == "ProjectsPortal".ToUpper())
                        {
                            if (reader[2].ToString().ToUpper() == "WRITE")
                            {
                                Session.Add("ISREADONLYPROJECT", false);
                            }
                            else
                            {
                                Session.Add("ISREADONLYPROJECT", true);
                            }
                        }

                        if (reader[3].ToString().ToUpper() == "IncidentsPortal".ToUpper())
                        {
                            if (reader[2].ToString().ToUpper() == "WRITE")
                            {
                                Session.Add("ISREADONLYINCIDENT", false);
                            }
                            else
                            {
                                Session.Add("ISREADONLYINCIDENT", true);
                            }
                        }
                    }
                }
                else
                {
                    Session.Add("ISREADONLYPROJECT", true);
                    Session.Add("ISREADONLYINCIDENT", true);
                }
                conn.Close();
            }
        }
    }

    protected string getDefaultPage(string username)
    {
        string defaultPage = string.Empty;

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.GetUserDefaultPage";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        defaultPage = reader[0].ToString().ToUpper();
                    }
                }
                else
                {
                    defaultPage = string.Empty;
                }
                conn.Close();
            }
        }

        return defaultPage;
    }

    #region
    //protected bool getUserPermission(string username)
    //{
    //    bool isREADONLY = true;

    //    using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
    //    {
    //        string cmdText = "dbo.GetUserPermission";
    //        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
    //        {
    //            conn.Open();
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@username", username);
    //            SqlDataReader reader = cmd.ExecuteReader();
    //            if (reader.HasRows == true)
    //            {
    //                while (reader.Read())
    //                {
    //                    if (reader[2].ToString().ToUpper() == "WRITE")
    //                    {
    //                        isREADONLY = false;
    //                    }
    //                    else
    //                    {
    //                        isREADONLY = true;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                isREADONLY = true;
    //            }
    //            conn.Close();
    //        }
    //    }

    //    return isREADONLY;
    //}
    #endregion
}