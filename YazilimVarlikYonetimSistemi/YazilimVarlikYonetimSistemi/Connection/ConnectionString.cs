using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Connection
{
    public class ConnectionString
    {
        public static string connectionString { get; } = "data source=DESKTOP-2MQBITI\\MSSQLSERVER01; database=YazilimVarlikYonetimSistemi; integrated security= True;";
    }
}