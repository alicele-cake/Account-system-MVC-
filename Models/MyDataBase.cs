using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication0628.Models
{
    public class MyDataBase
    {
        public bool AddUserData(UserData data)
        {
            try
            {
                string connString = "server=127.0.0.1;port=3306;user id=root;password=alicele1sdah;database=mvctest;charset=utf8;";
                MySqlConnection conn = new MySqlConnection();
                conn = new MySqlConnection(connString);
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Connect();//等於上面幾行，不會寫function
                string id = Guid.NewGuid().ToString();
                string strSQL = @"INSERT INTO `userdata` (`id`, `account`, `password`, `city`, `village`, `address`)
                          VALUES (@id, @account, @password, @city, @village, @address)";
                MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@account", MySqlDbType.VarChar).Value = data.account;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = data.password1;
                cmd.Parameters.Add("@city", MySqlDbType.VarChar).Value = data.city;
                cmd.Parameters.Add("@village", MySqlDbType.VarChar).Value = data.village;
                cmd.Parameters.Add("@address", MySqlDbType.VarChar).Value = data.address;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                //Disconnect();
            }
        }

        public List<City> GetCityList()
        {
            try
            {
                string connString = "server=127.0.0.1;port=3306;user id=root;password=alicele1sdah;database=mvctest;charset=utf8;";
                MySqlConnection conn = new MySqlConnection();
                conn = new MySqlConnection(connString);
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Connect();//等於上面幾行，不會寫function
                string sql = @" SELECT `id`, `city` FROM `city`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                List<City> list = new List<City>();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        City city = new City();
                        city.CityId = dr["id"].ToString();
                        city.CityName = dr["city"].ToString();
                        list.Add(city);
                    }
                }
                return list;
            }
            catch (Exception ex)//!!
            {
                string error = ex.ToString();
                return null;
            }
            finally
            {
                //Disconnect();
            }
        }

        public List<Village> GetVillageList(string id)
        {
            try
            {
                string connString = "server=127.0.0.1;port=3306;user id=root;password=alicele1sdah;database=mvctest;charset=utf8;";
                MySqlConnection conn = new MySqlConnection();
                conn = new MySqlConnection(connString);
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Connect();
                string sql = @" SELECT `VillageId`, `Village` FROM `Village` WHERE `CityId` = @cityId";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.Parameters.Add("@cityId", id);//問題 https://blog.csdn.net/lbuskeep/article/details/38517615
                MySqlParameter param = new MySqlParameter();
                param.MySqlDbType = MySqlDbType.String;
                param.ParameterName = "cityId";
                param.Value = id;
                cmd.Parameters.Add(param);  //改用這五行
                List<Village> list = new List<Village>();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Village data = new Village();
                        data.VillageId = dr["VillageId"].ToString();
                        data.VillageName = dr["Village"].ToString();
                        list.Add(data);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
            finally
            {
                //Disconnect();
            }
        }


        public bool CheckUserData(string account, string password)
        {
            try
            {
                string connString = "server=127.0.0.1;port=3306;user id=root;password=alicele1sdah;database=mvctest;charset=utf8;";
                MySqlConnection conn = new MySqlConnection();
                conn = new MySqlConnection(connString);
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Connect();
                string strSQL = "SELECT * FROM `userdata` WHERE `account` = @account AND `password` = @password;";
                MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                cmd.Parameters.Add("@account", MySqlDbType.VarChar).Value = account;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return false;
            }
            finally
            {
                //Disconnect();
            }
        }
    }

}