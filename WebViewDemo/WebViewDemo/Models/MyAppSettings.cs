using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Collections;

namespace WebViewDemo.Models
{
    public class MyAppSettings
    {
        public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string ConfigPath = BaseDirectory + "config.dat";
        public static readonly string IconDirectory = BaseDirectory + "Icon" + Path.DirectorySeparatorChar;
        public static readonly double DefaultWindowWidth = 1200;
        public static readonly double DefaultWindowHeight = 1000;

        public WebInformation WebInformation { get; set; } = new WebInformation();
        public Configuration Configuration { get; set; } = new Configuration();
    }
}
