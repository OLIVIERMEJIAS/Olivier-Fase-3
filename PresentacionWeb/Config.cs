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

        public static bool Profesor { get; set; }

        public static bool Director { get; set; }

        public static bool Asistente { get; set; }
    }
}