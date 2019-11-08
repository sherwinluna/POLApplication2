using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectMatrix : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "Project Automation and System Matrix";

        for (int c=2015; c<= 2025; c++)
        {
            ddYear.Items.Add(new ListItem(c.ToString(), c.ToString()));
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {

    }
}