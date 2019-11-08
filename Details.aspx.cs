using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.IO;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

public partial class Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.Title = "Project Information";

            hfUserId.Value = Session["VALIDUSERID"].ToString();

            try
            {
                string projectNo = Request.QueryString["ProjectNo"].ToString().ToUpper().Trim();
                string action = Request.QueryString["Action"].ToString().ToUpper().Trim();
                string type = Request.QueryString["Type"].ToString().ToUpper().Trim();
                bool flag = Convert.ToBoolean(Request.QueryString["Flag"]);

                lblInactive1.Text = "Inactive Project (90 days) - " + DateTime.Now.ToString("MMMM");

                ShowProjectLocation();
                ShowSBU();
                ShowBusinessLine(Convert.ToInt32(this.ddSBU.SelectedValue.ToString()));
                ShowProjectTypes();
                ShowMigrationStatus();

                //Response.Write(action);

                if (action == "EDIT")
                {
                    ShowDetails(projectNo, flag);
                    this.txtPrjNumber.ReadOnly = true;
                    this.btnSaveEdit.Visible = true;
                    this.pnlFiles.Visible = true;
                    this.pnlDelete.Visible = true;
                    this.btnExtract.Enabled = true;
                }

                if (action == "ADD")
                {
                    this.txtPrjNumber.ReadOnly = false;
                    this.btnSaveAdd.Visible = true;
                    this.pnlFiles.Visible = false;
                    this.pnlDelete.Visible = false;
                    this.btnExtract.Enabled = false;

                    if (type == "CWX")
                    {
                        pnlCWX.Visible = true;
                        pnlPOL.Visible = false;
                        //pnlMIU.Visible = true;
                        //pnlCustomization.Visible = true;
                        hfFlag.Value = true.ToString();
                        this.txtSpecialInstruction.Text = "New CWX 6.7.1 Small PCS/DMS project";
                        this.lblPrjVersion.Text = "CWX 6.7.1";
                    }

                    if (type == "POL")
                    {
                        pnlCWX.Visible = false;
                        pnlPOL.Visible = true;
                        //pnlMIU.Visible = false;
                        //pnlCustomization.Visible = false;
                        hfFlag.Value = false.ToString();
                        this.txtSpecialInstruction.Text = "New CWX 6.2.2 Small PCS/DMS project";
                        this.lblPrjVersion.Text = "POL 6.2";
                    }
                }

                ShowFiles(projectNo, flag);

                checkPermission();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
            }
        }
    }

    protected void ShowProjectLocation()
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.PrjLocation";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //adapter.Fill(ds);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ddPrjLocation.DataSource = dt;
                ddPrjLocation.DataTextField = "Project_Location";
                ddPrjLocation.DataValueField = "Project_Location_Id";
                ddPrjLocation.DataBind();
            }
        }

        ddPrjLocation.Items.Insert(0, new ListItem("", "0"));
    }

    protected void ShowSBU()
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.SBUproc";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    ddSBU.DataSource = ds;
                    ddSBU.DataTextField = "SBUCode";
                    ddSBU.DataValueField = "ID";
                    ddSBU.DataBind();
                }
            }
        }

        ddSBU.Items.Insert(0, new ListItem("", "0"));
    }

    protected void ShowBusinessLine(int sbuID)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.BusinessLineProc";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@SBU", sbuID);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    ddBusinessLine.DataSource = ds;
                    ddBusinessLine.DataTextField = "Business_Line";
                    ddBusinessLine.DataValueField = "Business_Line";
                    ddBusinessLine.DataBind();
                }
            }
        }

        ddBusinessLine.Items.Insert(0, new ListItem("", "0"));
    }

    protected void ShowProjectTypes()
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.PrjTypes";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    ddProject_Type.DataSource = ds;
                    ddProject_Type.DataTextField = "Project_Type";
                    ddProject_Type.DataValueField = "Project_Type_Id";
                    ddProject_Type.DataBind();
                }
            }
        }

        ddProject_Type.Items.Insert(0, new ListItem("", "0"));
    }

    protected void ShowMigrationStatus()
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.MigrationStatus";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    ddMigrateStatus.DataSource = ds;
                    ddMigrateStatus.DataTextField = "Description";
                    ddMigrateStatus.DataValueField = "Id";
                    ddMigrateStatus.DataBind();

                    ddTWF.DataSource = ds;
                    ddTWF.DataTextField = "Description";
                    ddTWF.DataValueField = "Id";
                    ddTWF.DataBind();

                    ddNWF.DataSource = ds;
                    ddNWF.DataTextField = "Description";
                    ddNWF.DataValueField = "Id";
                    ddNWF.DataBind();
                }


            }
        }

        ddMigrateStatus.Items.Insert(0, new ListItem("", ""));
        ddTWF.Items.Insert(0, new ListItem("", ""));
        ddNWF.Items.Insert(0, new ListItem("", ""));
    }
    
    protected void ShowFiles(string projectNo, bool flag)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.SelectCWXFiles";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectNumber", projectNo);
                cmd.Parameters.AddWithValue("@Flag", flag);
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

    protected void ShowDetails(string projectNo, bool flag)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.SelectCWX";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectNumber", projectNo);
                cmd.Parameters.AddWithValue("@Flag", flag);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader["Is_CWX_671"].ToString()) == true)
                    {
                        pnlCWX.Visible = true;
                        pnlPOL.Visible = false;
                        //pnlMIU.Visible = true;
                        //pnlCustomization.Visible = true;
                        hfFlag.Value = true.ToString();
                        this.lblPrjVersion.Text = "CWX 6.7.1";
                    }
                    else
                    {
                        pnlCWX.Visible = false;
                        pnlPOL.Visible = true;
                        //pnlMIU.Visible = false;
                        //pnlCustomization.Visible = false;
                        hfFlag.Value = false.ToString();
                        this.lblPrjVersion.Text = "POL 6.2";
                    }

                    this.ddPrjLocation.SelectedItem.Text = reader[0].ToString();
                    this.ddDataCenter.SelectedItem.Text = reader[1].ToString();
                    this.ddSBU.SelectedItem.Text = reader[2].ToString();
                    this.ddBusinessLine.SelectedItem.Text = reader[3].ToString();
                    this.txtPrjNumber.Text = projectNo;
                    this.txtPrjName.Text = reader[4].ToString();
                    this.txtRequestDate.Text = reader[5].ToString();
                    this.ddStatus.SelectedItem.Text = reader[6].ToString();
                    this.txtNotes.Text = reader[7].ToString();
                    this.txtPriority.Text = reader[8].ToString();
                    //this.txtSetupDate.Text = reader[9].ToString();
                    //this.txtSetupAssignedTo.Text = reader[10].ToString();
                    this.txtForecastClose.Text = reader[11].ToString();
                    this.txtPlanForCloseout.Text = reader[12].ToString();

                    //this.txtITVendor.Text = reader[13].ToString();
                    //this.txtITEngineering.Text = reader[14].ToString();
                    //this.txtDocDist.Text = reader[15].ToString();
                    //this.txtSqck.Text = reader[16].ToString();
                    //this.txtOutbound.Text = reader[17].ToString();
                    //this.txtWebAPI.Text = reader[18].ToString();
                    //this.txtBrava.Text = reader[19].ToString();
                    //this.txtMDR.Text = reader[20].ToString();

                    this.txtInactive1.Text = reader[21].ToString();
                    this.txtInactive2.Text = reader[22].ToString();
                    this.txtInactive3.Text = reader[23].ToString();
                    this.txtERRRequestDate.Text = reader[24].ToString();
                    this.txtPDRequestDate.Text = reader[25].ToString();
                    this.txtGTVersion.Text = reader[26].ToString();
                    this.txtNoActivePCS.Text = reader[27].ToString();
                    this.txtAPDataCenter.Text = reader[28].ToString();
                    this.txtEDRCount.Text = reader[29].ToString();
                    this.txtEDRStatus.Text = reader[30].ToString();
                    this.txtERRUse.Text = reader[31].ToString();
                    this.txtNoERRDocs.Text = reader[32].ToString();
                    this.chkMigrate.Checked = Convert.ToBoolean(reader[33]);
                    this.txtMigratePlan.Text = reader[34].ToString();
                    this.txtSCDate.Text = reader[35].ToString();
                    this.txtAssignedTo.Text = reader[36].ToString();
                    this.txtMCDate.Text = reader[37].ToString();
                    this.txtMigrationComment.Text = reader[38].ToString();
                    this.ddMigrateStatus.SelectedItem.Text = reader[39].ToString();
                    this.txtSpecialInstruction.Text = reader[40].ToString();
                    this.txtTargetCompletion.Text = reader[41].ToString();

                    this.txtmpwa.Text = reader[42].ToString();
                    this.txtmkof.Text = reader[43].ToString();
                    this.txtpc.Text = reader[44].ToString();
                    this.txtrear.Text = reader[45].ToString();
                    this.txtsta.Text = reader[46].ToString();
                    this.txtsdc.Text = reader[47].ToString();
                    this.txtatpwc.Text = reader[48].ToString();
                    this.chkptcipol622.Checked = Convert.ToBoolean(reader[49]);
                    this.txtPIM.Text = reader[50].ToString();

                    this.chkCIM.Checked = Convert.ToBoolean(reader[51]);
                    this.chkWebAPI.Checked = Convert.ToBoolean(reader[52]);
                    this.chkCM.Checked = Convert.ToBoolean(reader[53]);
                    this.chkRFI.Checked = Convert.ToBoolean(reader[54]);
                    this.chkOther.Checked = Convert.ToBoolean(reader[55]);
                    this.chkCustom1.Checked = Convert.ToBoolean(reader[56]);
                    this.chkCustom2.Checked = Convert.ToBoolean(reader[57]);
                    this.chkCustom3.Checked = Convert.ToBoolean(reader[58]);
                    this.chkCustom4.Checked = Convert.ToBoolean(reader[59]);
                    this.chkCustom5.Checked = Convert.ToBoolean(reader[60]);
                    this.chkCustom6.Checked = Convert.ToBoolean(reader[61]);
                    this.chkCustom7.Checked = Convert.ToBoolean(reader[62]);
                    this.txtCustom1.Text = reader[63].ToString();
                    this.txtCustom2.Text = reader[64].ToString();
                    this.txtCustom3.Text = reader[65].ToString();
                    this.txtCustom4.Text = reader[66].ToString();
                    this.txtCustom5.Text = reader[67].ToString();
                    this.txtCustom6.Text = reader[68].ToString();
                    this.txtCustom7.Text = reader[69].ToString();
                    this.chkCustom8.Checked = Convert.ToBoolean(reader[70]);
                    this.txtCustom8.Text = reader[71].ToString();
                    this.txtPM.Text = reader[72].ToString();
                    this.ddTWF.SelectedItem.Text = reader[73].ToString();
                    this.ddNWF.SelectedItem.Text = reader[74].ToString();
                    this.txtears.Text = reader[75].ToString();
                    this.txtDMSSize.Text = reader[76].ToString();
                    this.txtPCSSize.Text = reader[77].ToString();
                    this.chkOnLegalHold.Checked = Convert.ToBoolean(reader[78]);
                }
                reader.Close();
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
                    string cmdText = "dbo.InsertCWXFile";
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectNumber", this.txtPrjNumber.Text);
                        cmd.Parameters.AddWithValue("@DocumentFileName", filename);
                        cmd.Parameters.AddWithValue("@DocumentDescription", this.txtUpload.Text);
                        cmd.Parameters.AddWithValue("@DocumentOwner", hfUserId.Value);
                        cmd.Parameters.AddWithValue("@DocumentContent", contentType);
                        cmd.Parameters.AddWithValue("@DocumentFile", bytes);
                        cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                        cmd.Parameters.AddWithValue("@Flag", Convert.ToBoolean(hfFlag.Value));
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
            string cmdText = "dbo.DownloadCWXFile";
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
            string cmdText = "dbo.DeleteCWXFile";
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

    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {
        if (checkProjectExist(this.txtPrjNumber.Text, this.txtPrjName.Text) <= 0)
        {
            if (hfFlag.Value == true.ToString())
            {
                using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
                {
                    string cmdText = "dbo.InsertCWX";
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectLocation", this.ddPrjLocation.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@DataCenter", this.ddDataCenter.SelectedValue);
                        cmd.Parameters.AddWithValue("@SBU", this.ddSBU.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@BusinessLine", this.ddBusinessLine.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ProjectNumber", this.txtPrjNumber.Text);
                        cmd.Parameters.AddWithValue("@ProjectName", this.txtPrjName.Text);
                        if (this.txtRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", this.txtRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Status", this.ddStatus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Notes", this.txtNotes.Text);
                        if (this.txtTargetCompletion.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", this.txtTargetCompletion.Text);
                        }
                        cmd.Parameters.AddWithValue("@Priority", this.txtPriority.Text);
                        if (this.txtForecastClose.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", this.txtForecastClose.Text);
                        }
                        if (this.txtPlanForCloseout.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", this.txtPlanForCloseout.Text);
                        }
                        //cmd.Parameters.AddWithValue("@InboundTVend", this.txtITVendor.Text);
                        //cmd.Parameters.AddWithValue("@InboundTEngg", this.txtITEngineering.Text);
                        //cmd.Parameters.AddWithValue("@DocDistribution", this.txtDocDist.Text);
                        //cmd.Parameters.AddWithValue("@Squadcheck", this.txtSqck.Text);
                        //cmd.Parameters.AddWithValue("@OutboundTransmittal", this.txtOutbound.Text);
                        //cmd.Parameters.AddWithValue("@WebAPI", this.txtWebAPI.Text);
                        //cmd.Parameters.AddWithValue("@Brava", this.txtBrava.Text);
                        //cmd.Parameters.AddWithValue("@MDR", this.txtMDR.Text);
                        //if (this.txtSetupDate.Text == string.Empty)
                        //{
                        //    cmd.Parameters.AddWithValue("@SetupDate", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@SetupDate", this.txtSetupDate.Text);
                        //}
                        //cmd.Parameters.AddWithValue("@SetUpAssignedTo", this.txtSetupAssignedTo.Text);
                        cmd.Parameters.AddWithValue("@InboundTVend", DBNull.Value);
                        cmd.Parameters.AddWithValue("@InboundTEngg", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DocDistribution", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Squadcheck", DBNull.Value);
                        cmd.Parameters.AddWithValue("@OutboundTransmittal", DBNull.Value);
                        cmd.Parameters.AddWithValue("@WebAPI", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Brava", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MDR", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SetupDate", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SetUpAssignedTo", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SpecialInstruction", this.txtSpecialInstruction.Text);
                        cmd.Parameters.AddWithValue("@miuCIM", this.chkCIM.Checked);
                        cmd.Parameters.AddWithValue("@miuWebAPI", this.chkWebAPI.Checked);
                        cmd.Parameters.AddWithValue("@miuCM", this.chkCM.Checked);
                        cmd.Parameters.AddWithValue("@miuRFI", this.chkRFI.Checked);
                        cmd.Parameters.AddWithValue("@miuOther", this.chkOther.Checked);
                        cmd.Parameters.AddWithValue("@pcWF", this.chkCustom1.Checked);
                        cmd.Parameters.AddWithValue("@pcTVF", this.chkCustom2.Checked);
                        cmd.Parameters.AddWithValue("@pcSSRSReport", this.chkCustom3.Checked);
                        cmd.Parameters.AddWithValue("@pcCIM", this.chkCustom4.Checked);
                        cmd.Parameters.AddWithValue("@pcCM", this.chkCustom5.Checked);
                        cmd.Parameters.AddWithValue("@pcRFI", this.chkCustom6.Checked);
                        cmd.Parameters.AddWithValue("@pcOther", this.chkCustom7.Checked);
                        cmd.Parameters.AddWithValue("@pcWFtxt", this.txtCustom1.Text);
                        cmd.Parameters.AddWithValue("@pcTVFtxt", this.txtCustom2.Text);
                        cmd.Parameters.AddWithValue("@pcSSRSReporttxt", this.txtCustom3.Text);
                        cmd.Parameters.AddWithValue("@pcCIMtxt", this.txtCustom4.Text);
                        cmd.Parameters.AddWithValue("@pcCMtxt", this.txtCustom5.Text);
                        cmd.Parameters.AddWithValue("@pcRFItxt", this.txtCustom6.Text);
                        cmd.Parameters.AddWithValue("@pcOthertxt", this.txtCustom7.Text);
                        cmd.Parameters.AddWithValue("@pcDF", this.chkCustom8.Checked);
                        cmd.Parameters.AddWithValue("@pcDFtxt", this.txtCustom8.Text);
                        cmd.Parameters.AddWithValue("@PIM", this.txtPIM.Text);
                        cmd.Parameters.AddWithValue("@PM", this.txtPM.Text);

                        if (this.txtERRRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", this.txtERRRequestDate.Text);
                        }
                        if (this.txtPDRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", this.txtPDRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@OnLegalHold", this.chkOnLegalHold.Checked);

                        cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
                {
                    string cmdText = "dbo.InsertPOL";
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectLocation", this.ddPrjLocation.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@DataCenter", this.ddDataCenter.SelectedValue);
                        cmd.Parameters.AddWithValue("@SBU", this.ddSBU.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@BusinessLine", this.ddBusinessLine.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ProjectNumber", this.txtPrjNumber.Text);
                        cmd.Parameters.AddWithValue("@ProjectName", this.txtPrjName.Text);
                        if (this.txtRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", this.txtRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Status", this.ddStatus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Notes", this.txtNotes.Text);
                        if (this.txtTargetCompletion.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", this.txtTargetCompletion.Text);
                        }
                        cmd.Parameters.AddWithValue("@Priority", this.txtPriority.Text);
                        if (this.txtForecastClose.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", this.txtForecastClose.Text);
                        }
                        if (this.txtPlanForCloseout.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", this.txtPlanForCloseout.Text);
                        }
                        cmd.Parameters.AddWithValue("@Inactive1", this.txtInactive1.Text);
                        cmd.Parameters.AddWithValue("@Inactive2", this.txtInactive2.Text);
                        cmd.Parameters.AddWithValue("@InactiveFor", this.txtInactive3.Text);
                        if (this.txtERRRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", this.txtERRRequestDate.Text);
                        }
                        if (this.txtPDRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", this.txtPDRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Template", this.txtGTVersion.Text);
                        cmd.Parameters.AddWithValue("@ActivePCSOnly", this.txtNoActivePCS.Text);
                        cmd.Parameters.AddWithValue("@APDataCenter", this.txtAPDataCenter.Text);
                        cmd.Parameters.AddWithValue("@EDR1", this.txtEDRCount.Text);
                        cmd.Parameters.AddWithValue("@EDR2", this.txtEDRStatus.Text);
                        cmd.Parameters.AddWithValue("@ERRUse", this.txtERRUse.Text);
                        cmd.Parameters.AddWithValue("@NoERRDocs", this.txtNoERRDocs.Text);
                        cmd.Parameters.AddWithValue("@MigrateFlage", this.chkMigrate.Checked);
                        cmd.Parameters.AddWithValue("@MigratePlan", this.txtMigratePlan.Text);
                        if (this.txtSCDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@MigrateScheduledDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MigrateScheduledDate", this.txtSCDate.Text);
                        }
                        if (this.txtMCDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@MigrateCompletedDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MigrateCompletedDate", this.txtMCDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Migration_Assigned_To", this.txtAssignedTo.Text);
                        cmd.Parameters.AddWithValue("@Migration_Comment", this.txtMigrationComment.Text);
                        cmd.Parameters.AddWithValue("@MigrationStatusDesc", this.ddMigrateStatus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SpecialInstruction", this.txtSpecialInstruction.Text);

                        if (this.txtmpwa.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Migration_Assessment", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Migration_Assessment", this.txtmpwa.Text);
                        }
                        if (this.txtmkof.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Migration_KickOff", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Migration_KickOff", this.txtmkof.Text);
                        }
                        if (this.txtpc.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Prework_Completed", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Prework_Completed", this.txtpc.Text);
                        }
                        if (this.txtrear.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Run_ERR_AuditReport", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Run_ERR_AuditReport", this.txtrear.Text);
                        }
                        if (this.txtsta.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Stage1_Test_Approval", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Stage1_Test_Approval", this.txtsta.Text);
                        }
                        if (this.txtsdc.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Stage1_DMS_Completed", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Stage1_DMS_Completed", this.txtsdc.Text);
                        }
                        if (this.txtatpwc.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Approval_Proceed_CotOver", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Approval_Proceed_CotOver", this.txtatpwc.Text);
                        }
                        cmd.Parameters.AddWithValue("@PlantoCloseinPOL622", this.chkptcipol622.Checked);
                        cmd.Parameters.AddWithValue("@PIM", this.txtPIM.Text);

                        cmd.Parameters.AddWithValue("@miuCIM", this.chkCIM.Checked);
                        cmd.Parameters.AddWithValue("@miuWebAPI", this.chkWebAPI.Checked);
                        cmd.Parameters.AddWithValue("@miuCM", this.chkCM.Checked);
                        cmd.Parameters.AddWithValue("@miuRFI", this.chkRFI.Checked);
                        cmd.Parameters.AddWithValue("@miuOther", this.chkOther.Checked);
                        cmd.Parameters.AddWithValue("@pcWF", this.chkCustom1.Checked);
                        cmd.Parameters.AddWithValue("@pcTVF", this.chkCustom2.Checked);
                        cmd.Parameters.AddWithValue("@pcSSRSReport", this.chkCustom3.Checked);
                        cmd.Parameters.AddWithValue("@pcCIM", this.chkCustom4.Checked);
                        cmd.Parameters.AddWithValue("@pcCM", this.chkCustom5.Checked);
                        cmd.Parameters.AddWithValue("@pcRFI", this.chkCustom6.Checked);
                        cmd.Parameters.AddWithValue("@pcOther", this.chkCustom7.Checked);
                        cmd.Parameters.AddWithValue("@pcWFtxt", this.txtCustom1.Text);
                        cmd.Parameters.AddWithValue("@pcTVFtxt", this.txtCustom2.Text);
                        cmd.Parameters.AddWithValue("@pcSSRSReporttxt", this.txtCustom3.Text);
                        cmd.Parameters.AddWithValue("@pcCIMtxt", this.txtCustom4.Text);
                        cmd.Parameters.AddWithValue("@pcCMtxt", this.txtCustom5.Text);
                        cmd.Parameters.AddWithValue("@pcRFItxt", this.txtCustom6.Text);
                        cmd.Parameters.AddWithValue("@pcOthertxt", this.txtCustom7.Text);
                        cmd.Parameters.AddWithValue("@pcDF", this.chkCustom8.Checked);
                        cmd.Parameters.AddWithValue("@pcDFtxt", this.txtCustom8.Text);
                        cmd.Parameters.AddWithValue("@PM", this.txtPM.Text);
                        cmd.Parameters.AddWithValue("@TWF", this.ddTWF.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@NWF", this.ddNWF.SelectedItem.Text);
                        if (this.txtears.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Sent_ERR_AuditReport", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Sent_ERR_AuditReport", this.txtears.Text);
                        }
                        if (this.txtDMSSize.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@DMSSize", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DMSSize", Convert.ToDecimal(this.txtDMSSize.Text));
                        }
                        if (this.txtPCSSize.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@PCSSize", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PCSSize", Convert.ToDecimal(this.txtPCSSize.Text));
                        }
                        cmd.Parameters.AddWithValue("@OnLegalHold", this.chkOnLegalHold.Checked);


                        cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

            Response.Redirect("Details.aspx?ProjectNo=" + this.txtPrjNumber.Text.ToUpper() + "&Flag=" + this.hfFlag.Value.ToString() + "&Action=edit&Type=");
        }
        else
        {
            lblMessage.Text = "Project Number/Project Name must be unique";
        }
    }

    protected void btnSaveEdit_Click(object sender, EventArgs e)
    {
        //if (checkProjectExist(this.txtPrjNumber.Text, this.txtPrjName.Text) <= 0)
        //{
            if (hfFlag.Value == true.ToString())
            {
                using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
                {
                    string cmdText = "dbo.UpdateCWX";
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectLocation", this.ddPrjLocation.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@DataCenter", this.ddDataCenter.SelectedValue);
                        cmd.Parameters.AddWithValue("@SBU", this.ddSBU.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@BusinessLine", this.ddBusinessLine.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ProjectNumber", this.txtPrjNumber.Text);
                        cmd.Parameters.AddWithValue("@ProjectName", this.txtPrjName.Text);
                        if (this.txtRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", this.txtRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Status", this.ddStatus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Notes", this.txtNotes.Text);
                        if (this.txtTargetCompletion.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", this.txtTargetCompletion.Text);
                        }
                        cmd.Parameters.AddWithValue("@Priority", this.txtPriority.Text);
                        if (this.txtForecastClose.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", this.txtForecastClose.Text);
                        }
                        if (this.txtPlanForCloseout.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", this.txtPlanForCloseout.Text);
                        }
                        //cmd.Parameters.AddWithValue("@InboundTVend", this.txtITVendor.Text);
                        //cmd.Parameters.AddWithValue("@InboundTEngg", this.txtITEngineering.Text);
                        //cmd.Parameters.AddWithValue("@DocDistribution", this.txtDocDist.Text);
                        //cmd.Parameters.AddWithValue("@Squadcheck", this.txtSqck.Text);
                        //cmd.Parameters.AddWithValue("@OutboundTransmittal", this.txtOutbound.Text);
                        //cmd.Parameters.AddWithValue("@WebAPI", this.txtWebAPI.Text);
                        //cmd.Parameters.AddWithValue("@Brava", this.txtBrava.Text);
                        //cmd.Parameters.AddWithValue("@MDR", this.txtMDR.Text);
                        //if (this.txtSetupDate.Text == string.Empty)
                        //{
                        //    cmd.Parameters.AddWithValue("@SetupDate", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@SetupDate", this.txtSetupDate.Text);
                        //}
                        //cmd.Parameters.AddWithValue("@SetUpAssignedTo", this.txtSetupAssignedTo.Text);
                        cmd.Parameters.AddWithValue("@InboundTVend", DBNull.Value);
                        cmd.Parameters.AddWithValue("@InboundTEngg", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DocDistribution", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Squadcheck", DBNull.Value);
                        cmd.Parameters.AddWithValue("@OutboundTransmittal", DBNull.Value);
                        cmd.Parameters.AddWithValue("@WebAPI", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Brava", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MDR", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SetupDate", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SetUpAssignedTo", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SpecialInstruction", this.txtSpecialInstruction.Text);
                        cmd.Parameters.AddWithValue("@miuCIM", this.chkCIM.Checked);
                        cmd.Parameters.AddWithValue("@miuWebAPI", this.chkWebAPI.Checked);
                        cmd.Parameters.AddWithValue("@miuCM", this.chkCM.Checked);
                        cmd.Parameters.AddWithValue("@miuRFI", this.chkRFI.Checked);
                        cmd.Parameters.AddWithValue("@miuOther", this.chkOther.Checked);
                        cmd.Parameters.AddWithValue("@pcWF", this.chkCustom1.Checked);
                        cmd.Parameters.AddWithValue("@pcTVF", this.chkCustom2.Checked);
                        cmd.Parameters.AddWithValue("@pcSSRSReport", this.chkCustom3.Checked);
                        cmd.Parameters.AddWithValue("@pcCIM", this.chkCustom4.Checked);
                        cmd.Parameters.AddWithValue("@pcCM", this.chkCustom5.Checked);
                        cmd.Parameters.AddWithValue("@pcRFI", this.chkCustom6.Checked);
                        cmd.Parameters.AddWithValue("@pcOther", this.chkCustom7.Checked);
                        cmd.Parameters.AddWithValue("@pcWFtxt", this.txtCustom1.Text);
                        cmd.Parameters.AddWithValue("@pcTVFtxt", this.txtCustom2.Text);
                        cmd.Parameters.AddWithValue("@pcSSRSReporttxt", this.txtCustom3.Text);
                        cmd.Parameters.AddWithValue("@pcCIMtxt", this.txtCustom4.Text);
                        cmd.Parameters.AddWithValue("@pcCMtxt", this.txtCustom5.Text);
                        cmd.Parameters.AddWithValue("@pcRFItxt", this.txtCustom6.Text);
                        cmd.Parameters.AddWithValue("@pcOthertxt", this.txtCustom7.Text);
                        cmd.Parameters.AddWithValue("@pcDF", this.chkCustom8.Checked);
                        cmd.Parameters.AddWithValue("@pcDFtxt", this.txtCustom8.Text);
                        cmd.Parameters.AddWithValue("@PIM", this.txtPIM.Text);
                        cmd.Parameters.AddWithValue("@PM", this.txtPM.Text);

                        if (this.txtERRRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", this.txtERRRequestDate.Text);
                        }
                        if (this.txtPDRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", this.txtPDRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@OnLegalHold", this.chkOnLegalHold.Checked);

                        cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
                {
                    string cmdText = "dbo.UpdatePOL";
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectLocation", this.ddPrjLocation.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@DataCenter", this.ddDataCenter.SelectedValue);
                        cmd.Parameters.AddWithValue("@SBU", this.ddSBU.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@BusinessLine", this.ddBusinessLine.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ProjectNumber", this.txtPrjNumber.Text);
                        cmd.Parameters.AddWithValue("@ProjectName", this.txtPrjName.Text);
                        if (this.txtRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RequestDate", this.txtRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Status", this.ddStatus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Notes", this.txtNotes.Text);
                        if (this.txtTargetCompletion.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TargetCompletion", this.txtTargetCompletion.Text);
                        }
                        cmd.Parameters.AddWithValue("@Priority", this.txtPriority.Text);
                        if (this.txtForecastClose.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ForecastCloseDate", this.txtForecastClose.Text);
                        }
                        if (this.txtPlanForCloseout.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PlanForCloseout", this.txtPlanForCloseout.Text);
                        }
                        cmd.Parameters.AddWithValue("@Inactive1", this.txtInactive1.Text);
                        cmd.Parameters.AddWithValue("@Inactive2", this.txtInactive2.Text);
                        cmd.Parameters.AddWithValue("@InactiveFor", this.txtInactive3.Text);
                        if (this.txtERRRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ERRRequestDate", this.txtERRRequestDate.Text);
                        }
                        if (this.txtPDRequestDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DeletionRequestDate", this.txtPDRequestDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Template", this.txtGTVersion.Text);
                        cmd.Parameters.AddWithValue("@ActivePCSOnly", this.txtNoActivePCS.Text);
                        cmd.Parameters.AddWithValue("@APDataCenter", this.txtAPDataCenter.Text);
                        cmd.Parameters.AddWithValue("@EDR1", this.txtEDRCount.Text);
                        cmd.Parameters.AddWithValue("@EDR2", this.txtEDRStatus.Text);
                        cmd.Parameters.AddWithValue("@ERRUse", this.txtERRUse.Text);
                        cmd.Parameters.AddWithValue("@NoERRDocs", this.txtNoERRDocs.Text);
                        cmd.Parameters.AddWithValue("@MigrateFlage", this.chkMigrate.Checked);
                        cmd.Parameters.AddWithValue("@MigratePlan", this.txtMigratePlan.Text);
                        if (this.txtSCDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@MigrateScheduledDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MigrateScheduledDate", this.txtSCDate.Text);
                        }
                        if (this.txtMCDate.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@MigrateCompletedDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MigrateCompletedDate", this.txtMCDate.Text);
                        }
                        cmd.Parameters.AddWithValue("@Migration_Assigned_To", this.txtAssignedTo.Text);
                        cmd.Parameters.AddWithValue("@Migration_Comment", this.txtMigrationComment.Text);
                        cmd.Parameters.AddWithValue("@MigrationStatusDesc", this.ddMigrateStatus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SpecialInstruction", this.txtSpecialInstruction.Text);
                        if (this.txtmpwa.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Migration_Assessment", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Migration_Assessment", this.txtmpwa.Text);
                        }
                        if (this.txtmkof.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Migration_KickOff", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Migration_KickOff", this.txtmkof.Text);
                        }
                        if (this.txtpc.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Prework_Completed", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Prework_Completed", this.txtpc.Text);
                        }
                        if (this.txtrear.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Run_ERR_AuditReport", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Run_ERR_AuditReport", this.txtrear.Text);
                        }
                        if (this.txtsta.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Stage1_Test_Approval", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Stage1_Test_Approval", this.txtsta.Text);
                        }
                        if (this.txtsdc.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Stage1_DMS_Completed", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Stage1_DMS_Completed", this.txtsdc.Text);
                        }
                        if (this.txtatpwc.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Approval_Proceed_CotOver", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Approval_Proceed_CotOver", this.txtatpwc.Text);
                        }
                        cmd.Parameters.AddWithValue("@PlantoCloseinPOL622", this.chkptcipol622.Checked);
                        cmd.Parameters.AddWithValue("@PIM", this.txtPIM.Text);

                        cmd.Parameters.AddWithValue("@miuCIM", this.chkCIM.Checked);
                        cmd.Parameters.AddWithValue("@miuWebAPI", this.chkWebAPI.Checked);
                        cmd.Parameters.AddWithValue("@miuCM", this.chkCM.Checked);
                        cmd.Parameters.AddWithValue("@miuRFI", this.chkRFI.Checked);
                        cmd.Parameters.AddWithValue("@miuOther", this.chkOther.Checked);
                        cmd.Parameters.AddWithValue("@pcWF", this.chkCustom1.Checked);
                        cmd.Parameters.AddWithValue("@pcTVF", this.chkCustom2.Checked);
                        cmd.Parameters.AddWithValue("@pcSSRSReport", this.chkCustom3.Checked);
                        cmd.Parameters.AddWithValue("@pcCIM", this.chkCustom4.Checked);
                        cmd.Parameters.AddWithValue("@pcCM", this.chkCustom5.Checked);
                        cmd.Parameters.AddWithValue("@pcRFI", this.chkCustom6.Checked);
                        cmd.Parameters.AddWithValue("@pcOther", this.chkCustom7.Checked);
                        cmd.Parameters.AddWithValue("@pcWFtxt", this.txtCustom1.Text);
                        cmd.Parameters.AddWithValue("@pcTVFtxt", this.txtCustom2.Text);
                        cmd.Parameters.AddWithValue("@pcSSRSReporttxt", this.txtCustom3.Text);
                        cmd.Parameters.AddWithValue("@pcCIMtxt", this.txtCustom4.Text);
                        cmd.Parameters.AddWithValue("@pcCMtxt", this.txtCustom5.Text);
                        cmd.Parameters.AddWithValue("@pcRFItxt", this.txtCustom6.Text);
                        cmd.Parameters.AddWithValue("@pcOthertxt", this.txtCustom7.Text);
                        cmd.Parameters.AddWithValue("@pcDF", this.chkCustom8.Checked);
                        cmd.Parameters.AddWithValue("@pcDFtxt", this.txtCustom8.Text);
                        cmd.Parameters.AddWithValue("@PM", this.txtPM.Text);
                        cmd.Parameters.AddWithValue("@TWF", this.ddTWF.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@NWF", this.ddNWF.SelectedItem.Text);
                        if (this.txtears.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@Sent_ERR_AuditReport", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Sent_ERR_AuditReport", this.txtears.Text);
                        }
                        if (this.txtDMSSize.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@DMSSize", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DMSSize", Convert.ToDecimal(this.txtDMSSize.Text));
                        }
                        if (this.txtPCSSize.Text == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@PCSSize", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PCSSize", Convert.ToDecimal(this.txtPCSSize.Text));
                        }
                        cmd.Parameters.AddWithValue("@OnLegalHold", this.chkOnLegalHold.Checked);

                        cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        //}
        //else
        //{
            //lblMessage.Text = "Project Number/Project Name must be unique";
        //}
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.DeleteProject";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_Number", this.txtPrjNumber.Text);
                cmd.Parameters.AddWithValue("@Project_Type", this.ddProject_Type.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@UserId", hfUserId.Value);
                cmd.Parameters.AddWithValue("@Flag", Convert.ToBoolean(hfFlag.Value));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        Response.Redirect("Statistics.aspx");
    }

    protected void ddSBU_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowBusinessLine(Convert.ToInt32(this.ddSBU.SelectedValue.ToString()));
    }

    protected int checkProjectExist(string projectNo, string projectName)
    {
        int count = 0;
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.CheckProjectExist";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectNumber", projectNo);
                cmd.Parameters.AddWithValue("@ProjectName", projectName);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                reader.Close();
            }
        }

        return count;
    }

    protected void btnExtract_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = Server.MapPath("~/Templates/") + "Project Change Request Form Rev1_Blank.xlsx";
            string newfileName = Server.MapPath("~/Templates/") + "Project Change Request Form Rev1_" + this.txtPrjNumber.Text + ".xlsx";

            if (File.Exists(newfileName))
            {
                File.Delete(newfileName);
            }

            CopyFile(fileName, newfileName);

            document = SpreadsheetDocument.Open(newfileName, true);
            wbPart = document.WorkbookPart;

            string wsName = "POL Business";
            UpdateValue(wsName, "A2", "New Project", 0, true);
            UpdateValue(wsName, "B2", DateTime.Now.ToShortDateString(), 0, true);
            UpdateValue(wsName, "C2", this.ddPrjLocation.SelectedItem.Text, 0, true);
            UpdateValue(wsName, "D2", "No", 0, true);
            UpdateValue(wsName, "E2", this.txtPrjName.Text, 0, true);
            UpdateValue(wsName, "F2", this.txtPrjNumber.Text, 0, true);
            UpdateValue(wsName, "G2", this.txtSpecialInstruction.Text, 0, true);
            UpdateValue(wsName, "H2", "Projects OnLine Support", 0, true);
            UpdateValue(wsName, "I2", "", 0, true);
            document.Close();

            FileInfo file = new FileInfo(newfileName);
            if (file.Exists)
            {
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/vnd.ms-excel";
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    protected void checkPermission()
    {
        bool isReadOnly = Convert.ToBoolean(Session["ISREADONLYPROJECT"]);
        //Response.Write(isReadOnly.ToString());

        if (isReadOnly == true)
        {
            btnSaveAdd.Visible = false;
            btnSaveEdit.Visible = false;
            btnExtract.Visible = false;
            pnlFiles.Visible = false;
            pnlDelete.Visible = false;
        }
    }

    #region "XLSX"
    WorkbookPart wbPart = null;
    SpreadsheetDocument document = null;

    private string CopyFile(string source, string dest)
    {
        string result = "Copied file";
        try
        {
            // Overwrites existing files
            File.Copy(source, dest, true);
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }

    // Given a Worksheet and an address (like "AZ254"), either return a cell reference, or 
    // create the cell reference and return it.
    private Cell InsertCellInWorksheet(Worksheet ws, string addressName)
    {
        SheetData sheetData = ws.GetFirstChild<SheetData>();
        Cell cell = null;

        UInt32 rowNumber = GetRowIndex(addressName);
        Row row = GetRow(sheetData, rowNumber);

        // If the cell you need already exists, return it.
        // If there is not a cell with the specified column name, insert one.  
        Cell refCell = row.Elements<Cell>().
            Where(c => c.CellReference.Value == addressName).FirstOrDefault();
        if (refCell != null)
        {
            cell = refCell;
        }
        else
        {
            cell = CreateCell(row, addressName);
        }
        return cell;
    }

    private Cell CreateCell(Row row, String address)
    {
        Cell cellResult;
        Cell refCell = null;

        // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
        foreach (Cell cell in row.Elements<Cell>())
        {
            if (string.Compare(cell.CellReference.Value, address, true) > 0)
            {
                refCell = cell;
                break;
            }
        }

        cellResult = new Cell();
        cellResult.CellReference = address;

        row.InsertBefore(cellResult, refCell);
        return cellResult;
    }

    private Row GetRow(SheetData wsData, UInt32 rowIndex)
    {
        var row = wsData.Elements<Row>().
        Where(r => r.RowIndex.Value == rowIndex).FirstOrDefault();
        if (row == null)
        {
            row = new Row();
            row.RowIndex = rowIndex;
            wsData.Append(row);
        }
        return row;
    }

    private UInt32 GetRowIndex(string address)
    {
        string rowPart;
        UInt32 l;
        UInt32 result = 0;

        for (int i = 0; i < address.Length; i++)
        {
            if (UInt32.TryParse(address.Substring(i, 1), out l))
            {
                rowPart = address.Substring(i, address.Length - i);
                if (UInt32.TryParse(rowPart, out l))
                {
                    result = l;
                    break;
                }
            }
        }
        return result;
    }

    public bool UpdateValue(string sheetName, string addressName, string value, UInt32Value styleIndex, bool isString)
    {
        // Assume failure.
        bool updated = false;

        Sheet sheet = wbPart.Workbook.Descendants<Sheet>().Where((s) => s.Name == sheetName).FirstOrDefault();

        if (sheet != null)
        {
            Worksheet ws = ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;
            Cell cell = InsertCellInWorksheet(ws, addressName);

            if (isString)
            {
                // Either retrieve the index of an existing string,
                // or insert the string into the shared string table
                // and get the index of the new item.
                int stringIndex = InsertSharedStringItem(wbPart, value);

                cell.CellValue = new CellValue(stringIndex.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }
            else
            {
                cell.CellValue = new CellValue(value);
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
            }

            if (styleIndex > 0)
                cell.StyleIndex = styleIndex;

            // Save the worksheet.
            ws.Save();
            updated = true;
        }

        return updated;
    }

    // Given the main workbook part, and a text value, insert the text into the shared
    // string table. Create the table if necessary. If the value already exists, return
    // its index. If it doesn't exist, insert it and return its new index.
    private int InsertSharedStringItem(WorkbookPart wbPart, string value)
    {
        int index = 0;
        bool found = false;
        var stringTablePart = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

        // If the shared string table is missing, something's wrong.
        // Just return the index that you found in the cell.
        // Otherwise, look up the correct text in the table.
        if (stringTablePart == null)
        {
            // Create it.
            stringTablePart = wbPart.AddNewPart<SharedStringTablePart>();
        }

        var stringTable = stringTablePart.SharedStringTable;
        if (stringTable == null)
        {
            stringTable = new SharedStringTable();
        }

        // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
        foreach (SharedStringItem item in stringTable.Elements<SharedStringItem>())
        {
            if (item.InnerText == value)
            {
                found = true;
                break;
            }
            index += 1;
        }

        if (!found)
        {
            stringTable.AppendChild(new SharedStringItem(new Text(value)));
            stringTable.Save();
        }

        return index;
    }

    // Used to force a recalc of cells containing formulas. The
    // CellValue has a cached value of the evaluated formula. This
    // will prevent Excel from recalculating the cell even if 
    // calculation is set to automatic.
    private bool RemoveCellValue(string sheetName, string addressName)
    {
        bool returnValue = false;

        Sheet sheet = wbPart.Workbook.Descendants<Sheet>().
            Where(s => s.Name == sheetName).FirstOrDefault();
        if (sheet != null)
        {
            Worksheet ws = ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;
            Cell cell = InsertCellInWorksheet(ws, addressName);

            // If there is a cell value, remove it to force a recalc
            // on this cell.
            if (cell.CellValue != null)
            {
                cell.CellValue.Remove();
            }

            // Save the worksheet.
            ws.Save();
            returnValue = true;
        }

        return returnValue;
    }
    #endregion

}