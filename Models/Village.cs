using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication0628.Models
{
    public class Village
    {
        public string VillageId { get; set; }
        public string VillageName { get; set; }

        /*public static void Connect()
        {
            string connString = "server=127.0.0.1;port=3306;user id=root;password=alicele1sdah;database=mvctest;charset=utf8;";
            MySqlConnection conn = new MySqlConnection();
            conn = new MySqlConnection(connString);
        }*/
    }
}