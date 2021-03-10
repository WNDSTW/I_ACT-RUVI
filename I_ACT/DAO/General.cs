using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.DAO
{
    public class General
    {
        public static string connString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["IACTconstring"].ToString();
            }
        }


        public static string UMconnString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["UMconstring"].ToString();
            }
        }
    }
}