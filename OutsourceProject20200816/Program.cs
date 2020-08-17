using Newtonsoft.Json;
using OutsourceProject20200816.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutsourceProject20200816
{
    static class Program
    {
        public static Config Config { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Prepare
            var jsonConfig = File.ReadAllText("config.json");
            Config = JsonConvert.DeserializeObject<Config>(jsonConfig);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }
    }
}
