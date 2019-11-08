using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for GlobalProperties
/// </summary>
public class GlobalProperties
{
    public static string OracleConnectionString;
    //public static string SqlConnectionString = @"Data Source=ML0004680336\SQLEXPRESS;Initial Catalog=CoreworxMigrationDB;Integrated Security=True";
    //                                            "Data Source=ML0004680336\SQLEXPRESS;Initial Catalog=CoreworxMigrationDB;User ID=sa;Password=P@$$w0rd100"

    public GlobalProperties()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string SqlConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
    }
}