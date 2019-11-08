using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class BulkUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindGrid();
            checkPermission();
        }
    }

    private void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("select Project_Number, Project_Name, Migrate_Scheduled_Date, Migrate_Completed_Date from dbo.ProjectList where Migrate_Scheduled_Date is null order by Project_Location, Project_Number");
        gvCustomers.DataSource = this.ExecuteQuery(cmd, "SELECT");
        gvCustomers.DataBind();
    }

    private DataTable ExecuteQuery(SqlCommand cmd, string action)
    {
        using (SqlConnection con = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            cmd.Connection = con;
            switch (action)
            {
                case "SELECT":
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                case "UPDATE":
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    break;
            }
            return null;
        }
    }

    protected void Update(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCustomers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                if (isChecked)
                {
                    string Project_Number = row.Cells[1].Controls.OfType<TextBox>().FirstOrDefault().Text;
                    string Migrate_Scheduled_Date = row.Cells[3].Controls.OfType<TextBox>().FirstOrDefault().Text;
                    string Migrate_Completed_Date = row.Cells[4].Controls.OfType<TextBox>().FirstOrDefault().Text;

                    SqlCommand cmd = new SqlCommand("UPDATE ProjectList SET Migrate_Scheduled_Date = @Migrate_Scheduled_Date, Migrate_Completed_Date = @Migrate_Completed_Date WHERE Project_Number = @Project_Number");
                    if (Migrate_Scheduled_Date == string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@Migrate_Scheduled_Date", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Migrate_Scheduled_Date", Migrate_Scheduled_Date);
                    }
                    if (Migrate_Completed_Date == string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@Migrate_Completed_Date", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Migrate_Completed_Date", Migrate_Completed_Date);
                    }
                    cmd.Parameters.AddWithValue("@Project_Number", Migrate_Completed_Date);
                    this.ExecuteQuery(cmd, "SELECT");
                }
            }
        }
        btnUpdate.Visible = false;
        this.BindGrid();
    }

    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvCustomers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvCustomers.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvCustomers.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = !isChecked;
                    if (row.Cells[i].Controls.OfType<TextBox>().ToList().Count > 0)
                    {
                        row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = isChecked;
                    }
                    if (row.Cells[i].Controls.OfType<DropDownList>().ToList().Count > 0)
                    {
                        row.Cells[i].Controls.OfType<DropDownList>().FirstOrDefault().Visible = isChecked;
                    }
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;
                    }
                }
            }
        }
        btnUpdate.Visible = isUpdateVisible;
    }

    protected void checkPermission()
    {
        bool isReadOnly = Convert.ToBoolean(Session["ISREADONLYPROJECT"]);

        if (isReadOnly == true)
        {
            this.btnUpdate.Visible = false;
        }
    }
}