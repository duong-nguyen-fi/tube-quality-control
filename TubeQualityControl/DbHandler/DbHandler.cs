using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TubeQualityControl.DbHandler
{
    public class DbHandler
    {
        static string connString = "datasource=mysql.cc.puv.fi;database=e1500948_quality_control;username=e1500948 ;password=RXXT5tPH4YbW";
        MySqlConnection conn;
        MySqlDataAdapter mySqlDataAdapter;
        DataSet dataSet;


        public int Find_Max_Id()
        {
            int id = 0;
            conn = new MySqlConnection(connString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT max(id) FROM punch";

            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    id = Int32.Parse(dr["max(id)"].ToString());

                }
            }

            conn.Close();
            return id;
        }
    }
}
