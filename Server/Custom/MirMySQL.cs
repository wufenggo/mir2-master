using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.MirDatabase;
using MySql.Data.MySqlClient;
using Server.MirObjects;

namespace Server.Custom
{
    public class MirMySQL
    {
        MySqlConnection con;
        public static string ConnectionString = string.Format("SERVER={0};PORT={1};DATABASE={2};UID={3};PASSWORD={4};", Settings.SQL_IP, Settings.SQL_PORT, Settings.SQL_DBName, Settings.SQL_UName, Settings.SQL_Password);


        public bool Connected = false;

        public MirMySQL()
        {

        }

        public void SQLError(MySqlException ex)
        {
            switch (ex.ErrorCode)
            {
                case 0:
                    SMain.Enqueue(string.Format("Cannot connect to MySQL Database :\tIP : {0}\tPort : {1}\tDatabase : {2}", Settings.SQL_IP, Settings.SQL_PORT, Settings.SQL_DBName));
                    break;
                case 1045:
                    SMain.Enqueue(string.Format("Login failure for MySQL Database, check details!"));
                    break;
                case 1053:
                    SMain.Enqueue(string.Format("MySQL Server is shutting down.."));
                    break;
                case 1054:
                    SMain.Enqueue(string.Format("MySQL Column not found in table."));
                    break;
                case 1055:
                    SMain.Enqueue(string.Format("MySQL Item isn't in GROUP BY."));
                    break;
                case 1056:
                    SMain.Enqueue(string.Format("MySQL Cannot GROUP BY on table."));
                    break;
                case 1059:
                    SMain.Enqueue(string.Format("MySQL Identifier name is too long."));
                    break;
                case 1060:
                    SMain.Enqueue(string.Format("MySQL Column duplicate in statement."));
                    break;
                case 1061:
                    SMain.Enqueue(string.Format("MySQL Key Name duplicate in statement."));
                    break;
                case 1062:
                    SMain.Enqueue(string.Format("MySQL Duplicate entry for key in statement."));
                    break;
                case 1063:
                    SMain.Enqueue(string.Format("MySQL Incorrect column specifier."));
                    break;
                case 1065:
                    SMain.Enqueue(string.Format("MySQL Empty Query."));
                    break;
                case 1077:
                    SMain.Enqueue(string.Format("MySQL Shutting down.."));
                    break;
                case 1078:
                    SMain.Enqueue(string.Format("MySQL Aborting : Reason receive Signal."));
                    break;
                case 1079:
                    SMain.Enqueue(string.Format("MySQL Shutdown complete."));
                    break;
                case 1102:
                    SMain.Enqueue(string.Format("MySQL Incorrect database name."));
                    break;
                case 1105:
                    SMain.Enqueue("MySQL Unknown Error.");
                    break;
                case 1146:
                    SMain.Enqueue(string.Format("Table does not exist on MySQL Database."));
                    break;
                default:
                    SMain.Enqueue(string.Format("MySQL insert characterinfo data error : {0}", ex.ToString()));
                    break;
            }
        }

        public void Connect()
        {
            try
            {
                if (con == null)
                {
                    con = new MySqlConnection(ConnectionString);
                    con.Open();
                    Connected = true;
                }
                else
                {
                    con.Open();
                    Connected = true;
                }
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
            }
            finally
            {

            }
        }

        public void Disconnect()
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                    Connected = false;
                }
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
            }
        }

        #region Check Exists
        /*          CHECK if Objects already exist within the MySQL Database        */

        #region Check Hero Info exists
        public bool CheckExist(HeroObject hInfo)
        {
            if (hInfo == null)
                return false;
            if (hInfo.Master == null)
                return false;
            PlayerObject tmp = null;
            if (hInfo.Master is PlayerObject)
                tmp = (PlayerObject)hInfo.Master;

            if (tmp == null)
                return false;
            bool found = false;
            try
            {
                if (!Connected)
                    Connect();
                else
                {
                    Disconnect();
                    Connect();
                }
                string temp = string.Format("SELECT * FROM mir_db.t_hero WHERE a_hero_name = {0} AND a_owner = {1};", hInfo.HeroName, tmp.Name);
                using (MySqlCommand cmd = new MySqlCommand(temp, con))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    found = reader.HasRows;
                    reader.Close();
                }
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
                return false;
            }
            finally
            {
                con.Close();
            }
            if (found)
                return true;
            else
                return false;
        }
        #endregion

        #region Check Account Info exists
        public bool CheckExist(AccountInfo info)
        {
            if (info == null)
                return false;
            bool found = false;
            try
            {
                if (!Connected)
                    Connect();
                else
                {
                    Disconnect();
                    Connect();
                }
                string temp = string.Format("SELECT * FROM mir_db.t_account WHERE a_index = {0} ;", info.Index);
                using (MySqlCommand cmd = new MySqlCommand(temp, con))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    found = reader.HasRows;
                    reader.Close();
                }
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
                return false;
            }
            finally
            {
                con.Close();
            }
            if (found)
                return true;
            else
                return false;
        }
        #endregion

        #region Check CharacterInfo exists
        public bool CheckExist(CharacterInfo info)
        {
            if (info == null)
                return false;
            bool found = false;
            try
            {
                if (!Connected)
                    Connect();
                else
                {
                    Disconnect();
                    Connect();
                }
                string temp = string.Format("SELECT * FROM mir_db.t_character WHERE a_index = {0} ;", info.Index);
                using (MySqlCommand cmd = new MySqlCommand(temp, con))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    found = reader.HasRows;
                    reader.Close();
                }
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
                return false;
            }
            finally
            {
                con.Close();
            }
            if (found)
                return true;
            else
                return false;
        }
        #endregion

        #region Check Custom Exists
        public bool CheckExist(string sql)
        {
            if (sql == null || sql.Length <= 0)
                return false;
            bool found = false;
            try
            {
                if (!Connected)
                    Connect();
                else
                {
                    Disconnect();
                    Connect();
                }
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    found = reader.HasRows;
                }

            }
            catch (MySqlException ex)
            {
                SQLError(ex);
                return false;
            }
            finally
            {
                con.Close();
            }
            if (found)
                return true;
            else
                return false;
        }
        #endregion

        #region Check Donations
        public int[] CheckDonations(AccountInfo info)
        {
            int[] returnResult = new int[]
            {
                -1,
                -1
            };
            string sql = string.Format("SELECT * FROM t_donation WHERE a_user_idx = @a_user_idx AND a_claimed = 0");
            try
            {
                if (!Connected)
                    Connect();
                else
                {
                    Disconnect();
                    Connect();
                }
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = con,
                    CommandText = sql
                };

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@a_user_idx", info.Index);
                MySqlDataReader reader = cmd.ExecuteReader();
                bool hasRows = reader.HasRows;
                int Amount = 1;
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        if (Amount != 0)
                        {
                            returnResult[0] = Convert.ToInt32(reader["a_index"]);
                            returnResult[1] = Convert.ToInt32(reader["a_amount"]);
                            Amount = 0;
                        }
                    }
                }
                
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
            }
            finally
            {
                con.Close();
            }
            return returnResult;
        }
        #endregion
        /*          END of Checking                                                 */
        #endregion
        #region Update Data
        /*          UPDATE MySQL Tables                                             */

        #region Update Donation
        public void UpdateDonation(int idx)
        {
            string sql = string.Format("UPDATE mir_db.t_donation SET a_claimed = 1 WHERE a_index = @a_index");
            try
            {
                if (!Connected)
                    Connect();
                else
                {
                    Disconnect();
                    Connect();
                }
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = con,
                    CommandText = sql,
                };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@a_index", idx);
                cmd.ExecuteNonQuery();
                
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Update Hero table
        public void UpdateData(HeroObject aInfo, bool IsOnline = false)
        {
            if (aInfo == null)
                return;

            if (aInfo.Master == null)
                return;
            PlayerObject tmp = null;
            if (aInfo.Master is PlayerObject)
                tmp = (PlayerObject)aInfo.Master;

            if (tmp == null)
                return;

            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = con,
                    CommandText = "UPDATE mir_db.t_hero SET a_hero_name = @a_hero_name , a_level = @a_level , a_exp = @a_exp , a_class = @a_class WHERE a_owner = @a_owner AND a_hero_name = @a_hero_name"
                };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@a_hero_name", aInfo.HeroName);
                cmd.Parameters.AddWithValue("@a_level", aInfo.HeroLevel);
                cmd.Parameters.AddWithValue("@a_exp", aInfo.HeroExperience);
                cmd.Parameters.AddWithValue("@a_class", aInfo.HeroClass.ToString());
                cmd.Parameters.AddWithValue("@a_owner", tmp.Name);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
            }
            finally
            {
                con.Close();
            }

        }
        #endregion

        #region Update Account Info
        public void UpdateData(AccountInfo aInfo, bool IsOnline = false)
        {
            if (aInfo == null)
                return;
            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = con,
                    CommandText = "UPDATE mir_db.t_account SET a_account_credits = @a_acccred, a_account_mail = @a_mail, a_account_pw = @a_accpw WHERE a_index = @a_index"
                };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@a_index", aInfo.Index);
                cmd.Parameters.AddWithValue("@a_acccred", aInfo.Credit);
                cmd.Parameters.AddWithValue("@a_mail", aInfo.EMailAddress);
                cmd.Parameters.AddWithValue("@a_accpw", aInfo.Password);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
            }
            finally
            {
                con.Close();
            }

        }

        public void UpdateData(AccountInfo aInfo, bool IsOnline = false, bool NewAdded = false)
        {
            if (aInfo == null)
                return;
            if (!NewAdded)
                return;
            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = con,
                    CommandText = "UPDATE mir_db.t_account SET a_added = @a_added WHERE a_index = @a_index"
                };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@a_index", aInfo.Index);
                cmd.Parameters.AddWithValue("@a_added", aInfo.Credit);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
            }
            finally
            {
                con.Close();
            }

        }
        #endregion

        #region Update Character Info
        public void UpdateData(CharacterInfo cInfo, bool IsOnline = false)
        {
            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            if (cInfo != null)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = con,
                        CommandText = "UPDATE mir_db.t_character SET a_level = @a_level, a_exp = @a_exp, a_online = @a_online WHERE a_index=@a_index"
                    };
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@a_level", cInfo.Level);
                    cmd.Parameters.AddWithValue("@a_exp", cInfo.Experience);
                    cmd.Parameters.AddWithValue("@a_index", cInfo.Index);
                    cmd.Parameters.AddWithValue("@a_online", IsOnline);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    SQLError(ex);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        /*          End Of MySQL UPDATES                                            */
        #endregion
        #region Insert Data
        /*          INSERT MySQL Tables                                             */

        #region Insert Hero Info
        public void InsetData(HeroObject hInfo, bool IsOnline = false)
        {
            if (hInfo == null)
                return;
            if (hInfo.Master == null)
                return;
            PlayerObject tmp = null;
            if (hInfo.Master is PlayerObject)
                tmp = (PlayerObject)hInfo.Master;

            if (tmp == null)
                return;

            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            if (hInfo != null)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = con,
                        CommandText = "INSERT INTO mir_db.t_hero (a_level, a_exp, a_hero_name, a_class, a_owner) VALUES (@a_level, @a_exp, @a_hero_name, @a_class, @a_owner);"
                    };
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@a_level", hInfo.HeroLevel);
                    cmd.Parameters.AddWithValue("@a_exp", hInfo.HeroExperience);
                    cmd.Parameters.AddWithValue("@a_hero_name", hInfo.HeroName);
                    cmd.Parameters.AddWithValue("@a_class", hInfo.HeroClass.ToString());
                    cmd.Parameters.AddWithValue("@a_owner", tmp.Name);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    SQLError(ex);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region Insert Account Info
        public void InsetData(AccountInfo aInfo, bool IsOnline = false)
        {
            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            if (aInfo != null)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = con,
                        CommandText = "INSERT INTO mir_db.t_account (" +
                                                                    "a_index, " +
                                                                    "a_account_id, " +
                                                                    "a_account_pw, " +
                                                                    "a_account_credits, " +
                                                                    "a_account_mail, " +
                                                                    "a_account_verifiy, " +
                                                                    "a_account_ban, " +
                                                                    "a_admin) " +
                                                                    "VALUES " +
                                                                    "(" +
                                                                    "@a_index, " +
                                                                    "@a_accid, " +
                                                                    "@a_accpw, " +
                                                                    "@a_acccred, " +
                                                                    "@a_mail, " +
                                                                    "@a_account_verifiy, " +
                                                                    "@a_account_ban, " +
                                                                    "@a_admin);"
                    };
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@a_index", aInfo.Index);
                    cmd.Parameters.AddWithValue("@a_accid", aInfo.AccountID);
                    cmd.Parameters.AddWithValue("@a_accpw", aInfo.Password);
                    cmd.Parameters.AddWithValue("@a_acccred", aInfo.Credit);
                    cmd.Parameters.AddWithValue("@a_mail", aInfo.EMailAddress);
                    cmd.Parameters.AddWithValue("@a_account_verifiy", 1);
                    cmd.Parameters.AddWithValue("@a_account_ban", aInfo.Banned == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@a_admin", aInfo.AdminAccount == true ? 1 : aInfo.DevAccount == true ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    SQLError(ex);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region Insert Character Info
        public void InsetData(CharacterInfo cInfo, bool IsOnline = false)
        {
            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            if (cInfo != null)
            {
                try
                {
                    for (int i = 0; i < SMain.Envir.Players.Count; i++)
                    {
                        if (SMain.Envir.Players[i].Name == cInfo.Name)
                            IsOnline = true;
                    }
                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = con,
                        CommandText = "INSERT INTO mir_db.t_character (a_index, a_name, a_level, a_exp, a_class, a_online) VALUES (@a_index, @a_name, @a_level, @a_exp, @a_class, @a_online);"
                    };
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@a_index", cInfo.Index);
                    cmd.Parameters.AddWithValue("@a_name", cInfo.Name);
                    cmd.Parameters.AddWithValue("@a_level", cInfo.Level);
                    cmd.Parameters.AddWithValue("@a_exp", cInfo.Experience);
                    cmd.Parameters.AddWithValue("@a_class", cInfo.Class.ToString());
                    cmd.Parameters.AddWithValue("@a_online", IsOnline);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    SQLError(ex);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        /*          End Of MySQL INSERTS                                            */
        #endregion
        #region Retrive New Accounts
        public List<AccountInfo> GetNewAccounts()
        {
            List<AccountInfo> temp = new List<AccountInfo>();
            if (!Connected)
                Connect();
            else
            {
                Disconnect();
                Connect();
            }
            try
            {
                string sql = string.Format("SELECT * FROM mir_db.t_account WHERE a_account_verifiy = 1 AND a_account_added = 0");
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                AccountInfo tmp = new AccountInfo
                                {
                                    Index = Convert.ToInt32(reader["a_index"]),
                                    AccountID = reader["a_account_id"].ToString(),
                                    Password = reader["a_account_pw"].ToString(),
                                    EMailAddress = reader["a_account_mail"].ToString(),
                                    CreationDate = DateTime.Now,
                                    Banned = Convert.ToBoolean(reader["a_account_ban"]),
                                    AdminAccount = Convert.ToBoolean(reader["a_admin"]),
                                    Credit = Convert.ToUInt32(reader["a_account_credits"]),
                                    SecretQuestion = reader["a_account_question"].ToString(),
                                    SecretAnswer = reader["a_account_answer"].ToString(),
                                    UserName = reader["a_account_username"].ToString(),
                                    BirthDate = Convert.ToDateTime(reader["a_dob"])
                                };
                                temp.Add(tmp);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                SQLError(ex);
                return null;
            }
            finally
            {
                con.Close();
            }
            if (temp.Count > 0)
                return temp;
            else
                return null;
        }
        #endregion

    }
}
