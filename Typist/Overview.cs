/**
 * Typist - Typing proficiency tool
 * 
 * The Apache License, Version 2.0
 * Copyright (c) 2014 Matthew Roberts, http://github.com/codingmr

 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
 * except in compliance with the License. You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under the 
 * License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language governing permissions 
 * and limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Typist
{
    public partial class Overview : MetroForm
    {
        public Overview()
        {
            InitializeComponent();
        }
        public string connstr;
        public string user;

        public static string dirMeBIN = Path.GetDirectoryName(Application.StartupPath);
        public static string dirMe = dirMeBIN.Remove((dirMeBIN.IndexOf("bin")));
        public static string typekey_dataPath = (dirMe + "typekey_data" + (char) 92);
        public string[] groupPath = Directory.GetDirectories(typekey_dataPath);

        private List<string> groupNames = new List<string>();

        private string GetString(MySqlDataReader reader, int colIndex)
        {
            // Checks if the field is empty and if it is it returns an empty string
            // Stops dr.read() from returning false when it reaches field with null value
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        private void login_show()
        {

            tabcontrol.Hide();
            this.Size = new Size(342, 432);
            login.Show();
            this.Style = MetroFramework.MetroColorStyle.Teal;
        }

        private void overview_show()
        {
            login.Hide();
            this.Size = new Size(594, 533);
            tabcontrol.Show();
            this.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void Overview_Load(object sender, EventArgs e)
        {

            // SHOW LOGIN
            login_show();

            gotoRegister.Click += new EventHandler(gotoRegister_Click);
            gotoLogin.Click += new EventHandler(gotoLogin_Click);
            completeLogin.Click += new EventHandler(completeLogin_Click);
            completeRegister.Click += new EventHandler(completeRegister_Click);

            if (autoLogin.Checked) connectMySQL_AsUSER(Login_userBox.Text, Login_passBox.Text);

            groupSelect.Text = "Global";
            
            for (int i = 0; i < groupPath.Length; i++)
            {
                // Isolate folder names form paths
                groupNames.Add(groupPath[i].Remove(0, typekey_dataPath.Length));
                // Add folder names to groupSelect.combobox
                groupSelect.Items.Add(groupNames[i]);
            }
        }

        private void gotoRegister_Click(object sender, EventArgs e)
        {
            // Change z order of the panel stack
            login.SendToBack();
            login.Visible = false;
            register.BringToFront();
            register.Visible = true;
        }

        private void gotoLogin_Click(object sender, EventArgs e)
        {
            login.BringToFront();
            register.Visible = false;
            register.SendToBack();
            login.Visible = true;
        }

        private void createUser_AsROOT(string user, string pass, string name)
        {
            string connstr = "server=127.0.0.1;uid=root;port=3306;pwd=homebase;";
            
            try 
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    // Set the authentication to sha256 passwords
                    // Cannot use SHA256 passwords until mysql is configured with SSL

                    //SHA256 cmd.CommandText = "CREATE USER '" + user + "'@'127.0.0.1' IDENTIFIED WITH sha256_password;";
                    cmd.CommandText = "CREATE USER '" + user + "'@'127.0.0.1' IDENTIFIED BY '" + pass + "';";

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        // RETURN ERROR
                    }

                    //SHA256 cmd.CommandText = "SET old_passwords = 2;";
                    // Set password
                    //SHA256 cmd.CommandText = "SET PASSWORD FOR '" + user + "'@'127.0.0.1' = PASSWORD('" + pass + "');";

                    // Create user's table data database
                    cmd.CommandText = "CREATE DATABASE IF NOT EXISTS " + user + "_db";
                    cmd.ExecuteNonQuery();


                    // Give user access to the database created for them
                    // Char 92 is '\'
                    cmd.CommandText = "GRANT ALL PRIVILEGES ON `" + user + (char) 92 + "_db`.* TO '" + user + "'@'127.0.0.1' WITH GRANT OPTION";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS `" + user + "_db" + @"`.`global` (
                                `Date started` VARCHAR(18) NOT NULL DEFAULT '" + DateTime.Today.ToString("yyyy-MM-dd") + @"',
                                `Date` VARCHAR(18) NULL,
                                `Name` VARCHAR(36) NULL,
                                `Time spent in typekey-documents` FLOAT UNSIGNED ZEROFILL NULL,
                                `Keys per millisecond` FLOAT UNSIGNED ZEROFILL NULL,
                                `Number of incorrectly pressed keys` BIGINT UNSIGNED ZEROFILL NULL,
                                `Number of correctly pressed keys` BIGINT UNSIGNED ZEROFILL NULL,
                                `Longest time inbetween pressed keys` FLOAT UNSIGNED ZEROFILL NULL,
                                `Quickest time inbetween pressed keys` FLOAT UNSIGNED ZEROFILL NULL,
                                `Typekey-documents completed` INT UNSIGNED ZEROFILL NULL,
                                `Most Recent typekey-document completed` INT UNSIGNED ZEROFILL NULL,
                                PRIMARY KEY (`Date started`));";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "USE " + user + "_db";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO `" + user + @"_db`.`global`
                                        (`Name`)
                                        VALUES
                                        ('" + name + "');";
                    cmd.ExecuteNonQuery();

                }

            }
            catch (MySqlException MSex)
            {
                // RETURN ERROR
            }

            // RETURN CREATION SUCCESS

            // Goto login
            gotoLogin_Click(null, null);
        }

        private void connectMySQL_AsUSER(string user, string pass)
        {
            string connstr = "server=127.0.0.1;database=" + user + "_db;" + "uid=" + user + ";port=3306;pwd=" + pass + ";";
            try
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    // RETURN LOGIN SUCCESS
                    overview_show();
                    // See [Design] for Data binding: uid, pwd and checkOnLogin
                    Properties.Settings.Default.Save();
                }
            }
            catch (MySqlException MSex)
            {
                // RETURN ERROR
            }

        }

        private void completeLogin_Click(object sender, EventArgs e)
        {
            connectMySQL_AsUSER(Login_userBox.Text, Login_passBox.Text);
        }

        private void completeRegister_Click(object sender, EventArgs e)
        {
            createUser_AsROOT(Register_userBox.Text, Register_passBox.Text, Register_nameBox.Text);
        }

        private void gendocTables(string docName)
        {
            try
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();

                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS `" + user + "_db" + "`.`" + docName + @"` (
                                `Date started` VARCHAR(18) NOT NULL DEFAULT '" + DateTime.Today.ToString("yyyy-MM-dd") + @"',
                                `Date completed` VARCHAR(18) NULL,
                                `Document name` VARCHAR(36) NULL,
                                `Time spent in typekey-documents` FLOAT UNSIGNED ZEROFILL NULL,
                                `Keys per millisecond` FLOAT UNSIGNED ZEROFILL NULL,
                                `Number of incorrectly pressed keys` BIGINT UNSIGNED ZEROFILL NULL,
                                `Number of correctly pressed keys` BIGINT UNSIGNED ZEROFILL NULL,
                                `Longest time inbetween pressed keys` FLOAT UNSIGNED ZEROFILL NULL,
                                `Quickest time inbetween pressed keys` FLOAT UNSIGNED ZEROFILL NULL,
                                `Completed` VARCHAR(9) NOT NULL DEFAULT 'FALSE',
                                PRIMARY KEY (`Date started`));";
                    cmd.ExecuteNonQuery();
                 }
            }
            catch (MySqlException MSex)
            {
                MessageBox.Show(MSex.Message);
            }
        }

        private void readGlobaldata()
        {
            try
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();

                    cmd.CommandText = "SELECT * FROM `" + user + "_db`.`global`;";
                    cmd.ExecuteNonQuery();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            this.Text = "Overview - " + GetString(dr, 2);
                            group_dateStarted.Text = GetString(dr, 0);
                            group_timeInside.Text = GetString(dr, 3);
                            group_keys.Text = GetString(dr, 4);
                            group_correctKeys.Text = GetString(dr, 5);
                            group_incorrectKeys.Text = GetString(dr, 6);
                            group_longestKeys.Text = GetString(dr, 7);
                            group_quickestKeys.Text = GetString(dr, 8);
                            group_completed.Text = GetString(dr, 9);

                            // Calculated
                            //group_words.Text =;
                            //group_lines.Text =;
                            //group_accuracy.Text =;

                            if (GetString(dr, 10) == "")
                            {
                                docSelect.Visible = false;
                            }
                            else
                            {
                                docSelect.Visible = true;

                            }

                        }
                    }
                }

            }
            catch (MySqlException MSex)
            {
                MessageBox.Show(MSex.Message);
            }
        }

        private bool getdoc_completed()
        {
            // Read table docName_completed
            // return if selected doc is completed
            return false;
        }

        private void groupSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Global can only have 1 sub group "Most Recent"
            // Scan selected folder
            // Get file names
            // Add file names into docSelect.combobox
            docSelect.Items.Clear();
            if (groupSelect.Text == "Global")
            {
                docSelect.Items.Add("Most Recent");
                docSelect.Text = "Most Recent";

                // Load global data into groupPanel
                // Load most recent data into docPanel
                // Change playTile.text to Quick Play
                readGlobaldata();

                playTile.Text = "Quick Play";

            }
            else
            {
                string groupPath = typekey_dataPath + groupSelect.Text + (char) 92;
                string[] docFiles = Directory.GetFiles(groupPath);

                // Load data into groupPanel

                for (int i = 0; i < docFiles.Length; i++)
                {
                    // Isolate file names from paths
                    string docNameEX = docFiles[i].Remove(0, groupPath.Length);
                    // Isolate file names from .extensions
                    string docName = docNameEX.Remove(docNameEX.IndexOf(".txt"));
                    // Add file names to docSelect.combobox
                 
                    docSelect.Items.Add(docName);
                    // Create table for docName if not exists
                    gendocTables(docName);
                }
                
                docSelect.SelectedIndex = 0;

                for (int i = 0; i < docSelect.Items.Count; i++)
                {
                    if (!getdoc_completed())
                    {
                        // Found closest incomplete
                        docSelect.SelectedIndex = i;
                        break;
                    }
                }

                if (docSelect.Items.Count == 0)
                {
                    docPanel.Visible = false;
                }
                else
                {
                    docPanel.Visible = true;
                    // Load doc data
                }


                // Load file names
                // Load doc global data into groupPanel
                // Load doc data for docSelect.Index[0] into docPanel 
                // Change playTile.text to Play or Replay if doc data is completed
            }
        }

        private void docSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupSelect.Text != "Global")
            {
                // Load selected doc, doc data into docPanel 
                // Change playTile.text to Play or Replay if doc data is completed
            }
        }

        private void logoutTile_Click(object sender, EventArgs e)
        {
            connstr = String.Empty;
            user = String.Empty;
            Properties.Settings.Default.uid = String.Empty;
            Properties.Settings.Default.pwd = String.Empty;
            Properties.Settings.Default.checkAutoLogin = false;
            login_show();
            gotoLogin_Click(null, null);
        }

        private void settingsTile_Click(object sender, EventArgs e)
        {
            tabcontrol.SelectedIndex = 1;
        }

        private void playTile_Click(object sender, EventArgs e)
        {
            tabcontrol.SelectedIndex = 2;
        }
    }
}
