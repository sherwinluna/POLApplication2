using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Xml;
using System.Collections;
using System.Text;

public partial class ExtractSQL : System.Web.UI.Page
{
    string serverAppPath = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Extract Data";
            serverAppPath = Server.MapPath("~/tmpExtract/");
            //Response.Write(serverAppPath);
        }
    }

    protected void btnConnect_Click(object sender, EventArgs e)
    {
        string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + this.ddserver.SelectedValue + ")(PORT=" + this.txtPort.Text + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + this.ddserver.SelectedItem.Text + ")));User Id=" + this.txtUsername.Text + "; Password=" + this.txtPassword.Text + ";";
        GlobalProperties.OracleConnectionString = connString;

        using (OracleConnection conn = new OracleConnection(connString))
        {
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                retrieveSchemas(connString);
            }
            conn.Close();
        }
    }

    private void retrieveSchemas(string connString)
    {
        this.ddSchema.Enabled = true;
        this.btnExtract.Enabled = true;

        this.Button1.Enabled = true;
        this.Button2.Enabled = true;
        this.Button3.Enabled = true;
        this.Button4.Enabled = true;
        this.Button5.Enabled = true;

        using (OracleConnection conn = new OracleConnection(connString))
        {
            string cmdText = "select USERNAME as Id, USERNAME as Name from SYS.ALL_USERS where USERNAME like 'F%PROARC' order by USERNAME";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Connection = conn;
                conn.Open();

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    this.ddSchema.DataSource = ds;
                    this.ddSchema.DataTextField = "Id";
                    this.ddSchema.DataValueField = "Name";
                    this.ddSchema.DataBind();
                }

                conn.Close();
            }
        }
    }

    protected void btnExtract_Click(object sender, EventArgs e)
    {
        projectInformation(this.ddSchema.SelectedItem.Text);
        createTriggerFiles(this.ddSchema.SelectedItem.Text);
        createViewFiles(this.ddSchema.SelectedItem.Text);
        this.lblStatus.Text = "Successfully Extracted!";
    }

    private void projectInformation(string schema)
    {
        string path = this.txtPath.Text + schema + @"\";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string filePath = path + "ProjectInformation" + ".txt";

        using (OracleConnection conn = new OracleConnection(GlobalProperties.OracleConnectionString))
        {
            conn.Open();

            string cmdText = "select '" + this.ddSchema.SelectedItem.Value.ToString() + "' SCHEMANAME," +
                             "(select count(1) from all_objects where object_type = 'TABLE' and owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "') TABLECOUNT," +
                             "(select count(1) from all_objects where object_type = 'VIEW' and owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "') VIEWCOUNT," +
                             "(select count(1) from all_objects where object_type = 'INDEX' and owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "') INDEXCOUNT," +
                             "(select count(1) from dba_triggers where owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "') TRIGGERCOUNT," +
                             "(select count(1) from dba_sequences where sequence_owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "') SEQUENCECOUNT," +
                             "(select count(1) from all_tab_columns where owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "' and table_name not like 'BIN%') COLUMNCOUNT," +
                             "(select count(1) from " + this.ddSchema.SelectedItem.Value.ToString() + ".da_type a inner join " + this.ddSchema.SelectedItem.Value.ToString() + ".docbase db on a.DB_RNO = db.DB_RNO) ATTRIBUTECOUNT," +
                             "(select count(1) from " + this.ddSchema.SelectedItem.Value.ToString() + ".da_init) INITIALIZATIONRULECOUNT," +
                             "(select count(1) from " + this.ddSchema.SelectedItem.Value.ToString() + ".sql_script) WORKFLOWCOUNT," +
                             "(select count(1) from " + this.ddSchema.SelectedItem.Value.ToString() + ".rep_report) REPORTCOUNT," +
                             "(select count(1) from " + this.ddSchema.SelectedItem.Value.ToString() + ".LS_LISTSETUP) RESULTLISTCOUNT " +
                             "from dual";

            using (OracleCommand cmd = new OracleCommand(cmdText, conn))
            {
                OracleDataReader reader = cmd.ExecuteReader();
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            sw.WriteLine("Schema: " + reader[0].ToString());
                            sw.WriteLine("Table Count: " + reader[1].ToString());
                            sw.WriteLine("View Count: " + reader[2].ToString());
                            sw.WriteLine("Index Count: " + reader[3].ToString());
                            sw.WriteLine("Trigger Count: " + reader[4].ToString());
                            sw.WriteLine("Sequence Count: " + reader[5].ToString());
                            sw.WriteLine("Column Count: " + reader[6].ToString());

                            sw.WriteLine("Attribute Count: " + reader[7].ToString());
                            sw.WriteLine("Initialization Count: " + reader[8].ToString());
                            sw.WriteLine("Workflow Count: " + reader[9].ToString());
                            sw.WriteLine("Report Count: " + reader[10].ToString());
                            sw.WriteLine("Result List Count: " + reader[11].ToString());
                        }
                        catch (Exception e)
                        {
                            sw.WriteLine(e.ToString());
                        }
                    }
                }
            }

            conn.Close();
        }
    }

    private void createTriggerFiles(string schema)
    {
        string path = this.txtPath.Text + schema + @"\Triggers\";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using (OracleConnection conn = new OracleConnection(GlobalProperties.OracleConnectionString))
        {
            conn.Open();

            string cmdText = "select trigger_name from dba_triggers where owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "'";
            using (OracleCommand cmd = new OracleCommand(cmdText, conn))
            {
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string tname = reader["trigger_name"].ToString();
                    string filePath = path + tname + ".txt";
                    if (!File.Exists(filePath))
                    {
                        string cmdText2 = "select text from dba_source where owner = '" + this.ddSchema.SelectedItem.Value.ToString().ToUpper() + "' and name like '" + tname.ToUpper() + "'";
                        using (OracleCommand cmd2 = new OracleCommand(cmdText2, conn))
                        {
                            OracleDataReader reader2 = cmd2.ExecuteReader();
                            using (StreamWriter sw = File.CreateText(filePath))
                            {
                                sw.WriteLine("create or replace ");
                                while (reader2.Read())
                                {
                                    try
                                    {
                                        string text = reader2[0] == DBNull.Value ? "" : reader2[0].ToString();
                                        sw.WriteLine(text);
                                    }
                                    catch (Exception e)
                                    {
                                        sw.WriteLine(e.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }

            conn.Close();
        }
    }

    private void createViewFiles(string schema)
    {
        string path = this.txtPath.Text + schema + @"\Views\";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using (OracleConnection conn = new OracleConnection(GlobalProperties.OracleConnectionString))
        {
            conn.Open();

            string cmdText = "select object_name from all_objects where object_type = 'VIEW' and owner = '" + this.ddSchema.SelectedItem.Value.ToString() + "'";
            using (OracleCommand cmd = new OracleCommand(cmdText, conn))
            {
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string vname = reader["object_name"].ToString();
                    string filePath = path + vname + ".txt";
                    if (!File.Exists(filePath))
                    {
                        string cmdText2 = "select dbms_xmlgen.getxml('select text from all_views where owner = ''" + this.ddSchema.SelectedItem.Value.ToString().ToUpper() + "'' and view_name like ''" + vname.ToUpper() + "''') from dual";
                        using (OracleCommand cmd2 = new OracleCommand(cmdText2, conn))
                        {
                            OracleDataReader reader2 = cmd2.ExecuteReader();
                            using (StreamWriter sw = File.CreateText(filePath))
                            {
                                if (reader2.HasRows == true)
                                {
                                    while (reader2.Read())
                                    {
                                        try
                                        {
                                            Oracle.ManagedDataAccess.Types.OracleString text = reader2.GetOracleString(0);//reader2[0] == DBNull.Value ? "" : reader2[0].ToString();
                                            sw.WriteLine("create view " + vname);
                                            sw.WriteLine("( ");
                                            sw.WriteLine(getColumns(conn, this.ddSchema.SelectedItem.Value.ToString().ToUpper(), vname.ToUpper()));
                                            sw.WriteLine(") ");
                                            sw.WriteLine("as ");
                                            sw.WriteLine("begin ");
                                            sw.WriteLine(getText(text));
                                            sw.WriteLine("end ");
                                        }
                                        catch (Exception e)
                                        {
                                            sw.WriteLine(e.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            conn.Close();
        }
    }

    private string getColumns(OracleConnection conn, string schema, string viewname)
    {
        string strColumns = string.Empty;
        string cmdText = "select column_name from all_tab_columns where owner = '" + schema + "' and table_name not like 'BIN%' and table_name = '" + viewname + "'";

        using (OracleCommand cmd = new OracleCommand(cmdText, conn))
        {
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strColumns = strColumns + reader[0].ToString() + ",";
            }
        }
        return strColumns.Remove(strColumns.Trim().Length - 1);
    }

    protected string getText(Oracle.ManagedDataAccess.Types.OracleString xmlString)
    {
        string text = string.Empty;

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlString.ToString());

        XmlNodeList xnList = xml.SelectNodes("/ROWSET/ROW");
        foreach (XmlNode xn in xnList)
        {
            text = xn["TEXT"].InnerText;
        }
        return text;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        //ExportCSV(this.ddSchema.SelectedItem.Text, "(select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".da_type)", "da_type.csv");

        ExportCSV(this.ddSchema.SelectedItem.Text, "da_type".ToUpper(), "(select dbms_xmlgen.getxml('select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".da_type where rownum <= 10') from dual)", "da_type.csv");
        this.lblStatus.Text = "Successfully Extracted (da_type)!";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //ExportCSV(this.ddSchema.SelectedItem.Text, "(select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".da_init)", "da_init.csv");

        ExportCSV(this.ddSchema.SelectedItem.Text, "da_init".ToUpper(), "(select dbms_xmlgen.getxml('select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".da_init where rownum <= 10') from dual)", "da_init.csv");
        this.lblStatus.Text = "Successfully Extracted (da_init)!";
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //ExportCSV(this.ddSchema.SelectedItem.Text, "(select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".sql_script)", "sql_script.csv");

        ExportCSV(this.ddSchema.SelectedItem.Text, "sql_script".ToUpper(), "(select dbms_xmlgen.getxml('select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".sql_script where rownum <= 10') from dual)", "sql_script.csv");
        this.lblStatus.Text = "Successfully Extracted (sql_script)!";
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //ExportCSV(this.ddSchema.SelectedItem.Text, "(select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".rep_report)", "rep_report.csv");

        ExportCSV(this.ddSchema.SelectedItem.Text, "rep_report".ToUpper(), "(select dbms_xmlgen.getxml('select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".rep_report where rownum <= 10') from dual)", "rep_report.csv");
        this.lblStatus.Text = "Successfully Extracted (rep_report)!";
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        //ExportCSV(this.ddSchema.SelectedItem.Text, "(select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".LS_LISTSETUP)", "LS_LISTSETUP.csv");

        ExportCSV(this.ddSchema.SelectedItem.Text, "LS_LISTSETUP".ToUpper(), "(select dbms_xmlgen.getxml('select * from " + this.ddSchema.SelectedItem.Value.ToString() + ".LS_LISTSETUP where rownum <= 10') from dual)", "LS_LISTSETUP.csv");
        this.lblStatus.Text = "Successfully Extracted (LS_LISTSETUP)!";
    }

    protected void ExportCSV(string schema, string tableName, string queryData, string filename)
    {
        string csv = string.Empty;
        string queryTable = "select column_name from all_tab_columns where owner = '" + schema + "' and table_name not like 'BIN%' and table_name = '" + tableName + "'"; ;

        using (OracleConnection conn = new OracleConnection(GlobalProperties.OracleConnectionString))
        {
            conn.Open();

            int columnCount = 0;
            Hashtable ht = new Hashtable();
            using (OracleCommand cmd = new OracleCommand(queryTable, conn))
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        columnCount = columnCount + 1;
                        ht.Add(columnCount, reader[0].ToString());
                        csv += reader[0].ToString() + ',';
                    }
                    csv += "\r\n";
                }
            }

            using (OracleCommand cmd2 = new OracleCommand(queryData, conn))
            {
                OracleDataReader reader2 = cmd2.ExecuteReader();
                {
                    if (reader2.HasRows == true)
                    {
                        while (reader2.Read())
                        {
                            Oracle.ManagedDataAccess.Types.OracleString text = reader2.GetOracleString(0);
                            csv += getRow(text, ht);
                        }
                    }
                }
            }
            conn.Close();
        }

        string path = this.txtPath.Text + schema + @"\";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string filePath = path + filename + ".csv";

        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.Write(csv);
        }
    }

    protected string getRow(Oracle.ManagedDataAccess.Types.OracleString xmlString, Hashtable list)
    {
        string text = string.Empty;

        XmlDocument xml = new XmlDocument();

        try
        {
            xml.LoadXml(xmlString.ToString());
            /*.Replace("&","&amp;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;")*/
        }
        catch (Exception e)
        {
            using (FileStream fs = File.Create(filepath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(xmlString.ToString());
                fs.Write(info, 0, info.Length);
            }

            xml = LoadXMLDoc();
        }



        XmlNodeList xnList = xml.SelectNodes("/ROWSET/ROW");
        foreach (XmlNode xn in xnList)
        {
            for (int i = 1; i <= list.Count; i++)
            {
                try
                {
                    text += xn[list[i].ToString()].InnerText.ToString().Replace(",", ";") + ',';
                }
                catch (Exception e)
                {
                    text += ",";
                }
            }

            text += "\r\n";
        }

        return text;
    }

    #region
    string filepath = "";
    private void ReplaceSpecialChars(long linenumber)
    {
        //filepath = this.txtPath.Text + "MyXML.xml";
        //string tempfile = this.txtPath.Text + "MyTempXML.xml";
        filepath = serverAppPath + this.ddSchema.SelectedItem.Text  + "/" + "MyXML.xml";
        string tempfile = serverAppPath + this.ddSchema.SelectedItem.Text + "/" + "MyTempXML.xml";

        System.IO.StreamReader strm;
        string strline;
        string strreplace = " ";
        
        try
        {
            System.IO.File.Copy(filepath, tempfile, true);
        }
        catch (Exception ex)
        {
        }

        StreamWriter strmwriter = new StreamWriter(filepath);
        strmwriter.AutoFlush = true;
        strm = new StreamReader(tempfile);
        long i = 0;
        while (i < linenumber - 1)
        {
            strline = strm.ReadLine();
            strmwriter.WriteLine(strline);
            i = i + 1;
        }

        strline = strm.ReadLine();
        Int32 lineposition;

        lineposition = strline.IndexOf("&");
        if (lineposition > 0)
        {
            strreplace = "&amp;";
        }
        else
        {
            lineposition = strline.IndexOf("<", 1);
            if (lineposition > 0)
            {
                strreplace = "<";
            }
            else
            {
                lineposition = strline.IndexOf("\'");
                if (lineposition > 0)
                {
                    strreplace = "&apos;";
                }
                else
                {
                    lineposition = strline.IndexOf("\"");
                    if (lineposition > 0)
                    {
                        strreplace = "&quot;";
                    }
                    else
                    {
                        lineposition = strline.IndexOf(">", 1);
                        if (lineposition > 0)
                        {
                            strreplace = ">";
                        }
                    }
                }
            }
        }

        strline = strline.Substring(0, lineposition - 1) + strreplace + strline.Substring(lineposition + 1);
        strmwriter.WriteLine(strline);

        strline = strm.ReadToEnd();
        strmwriter.WriteLine(strline);

        strm.Close();
        strm = null;

        strmwriter.Flush();
        strmwriter.Close();
        strmwriter = null;

    }

    public XmlDocument LoadXMLDoc()
    {
        //filepath = this.txtPath.Text + "MyXML.xml";
        filepath = serverAppPath + this.ddSchema.SelectedItem.Text + "/" + "MyXML.xml";

        XmlDocument xdoc;
        long lnum;

        try
        {
            xdoc = new XmlDocument();
            xdoc.Load(filepath);
        }
        catch (XmlException ex)
        {
            lnum = ex.LineNumber;
            ReplaceSpecialChars(lnum);

            xdoc = LoadXMLDoc();
        }
        return (xdoc);
    }

    #endregion
}