using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TubeQualityControl.Entity;

namespace TubeQualityControl.DbHandler
{
    public class DbHandler
    {
        static string connString = "datasource=mysql.cc.puv.fi;database=e1500948_quality_control;username=e1500948 ;password=RXXT5tPH4YbW";
               

        public DbHandler()
        {
            
            //conn.Open();
        }


        public int Find_Max_Id()
        {
            int id = 0;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT max(id) FROM punch";
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        id = Int32.Parse(dr["max(id)"].ToString());

                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }


            
            return id;
        }

        public MeasurePoint GetLatestMeasurePoint()
        {
            MeasurePoint point = null;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM punch ORDER BY id DESC LIMIT 1";
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        var x = Double.Parse(dr["x"].ToString());
                        var y = Double.Parse(dr["y"].ToString());
                        var z = Double.Parse(dr["z"].ToString());

                        point = new MeasurePoint(x, y, z);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            

            
            
            return point;
        }
    }
}
