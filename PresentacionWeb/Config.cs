using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace PresentacionWeb
{
    public static class Config
    {
        public static string getCadConec
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionString"];
            }
        }

        public static int Profesor { get; set; }

        
    }
}