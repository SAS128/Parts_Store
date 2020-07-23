using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsStore.Models.TeleBot
{
    public class AppSettings
    {
        public static string Url { get; set; } = "https://telegrambotapp.azurewebsites.net:443/{0}";

        public static string Name { get; set; } = "STO_Hellperbot";

        public static string Key { get; set; } = "1230661420:AAGk5l8H-1llO0c35P0hyd8zHc-Nvckn8Lk";
    }
} 