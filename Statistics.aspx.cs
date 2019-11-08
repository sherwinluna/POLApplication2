using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Statistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Project Statistics";
            viewStatistics(DateTime.Now.Year);
        }
    }

    protected void viewStatistics(int year)
    {
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.Statistics1";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvStatistics.DataSource = ds;
                    gvStatistics.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.SiteCategory";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvSiteCategory.DataSource = ds;
                    gvSiteCategory.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsMigration";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvMigrationStatus.DataSource = ds;
                    gvMigrationStatus.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsHis";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvStatisticsHis.DataSource = ds;
                    gvStatisticsHis.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsRTP";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvDataCenterStatistics1.DataSource = ds;
                    gvDataCenterStatistics1.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsSG";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvDataCenterStatistics2.DataSource = ds;
                    gvDataCenterStatistics2.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsPerth";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvDataCenterStatistics3.DataSource = ds;
                    gvDataCenterStatistics3.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsERR_RTP";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvStatisticsERR1.DataSource = ds;
                    gvStatisticsERR1.DataBind();
                }
            }
        }
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsERR_SG";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvStatisticsERR2.DataSource = ds;
                    gvStatisticsERR2.DataBind();
                }
            }
        }

        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsPD_RTP";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvStatisticsPD1.DataSource = ds;
                    gvStatisticsPD1.DataBind();
                }
            }
        }
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsPD_SG";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvStatisticsPD2.DataSource = ds;
                    gvStatisticsPD2.DataBind();
                }
            }
        }
        using (SqlConnection conn = new SqlConnection(GlobalProperties.SqlConnectionString()))
        {
            string cmdText = "dbo.StatisticsPD_CLOSED";
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    gvStatisticsPD_Closed.DataSource = ds;
                    gvStatisticsPD_Closed.DataBind();
                }
            }
        }
    }
}