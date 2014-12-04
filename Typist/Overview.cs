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

using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Forms;
using MetroFramework.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Typist
{
    public partial class Overview : MetroForm
    {
        public Overview()
        {
            InitializeComponent();
        }
        private string connstr;
        private string user;
        private string password = Properties.Settings.Default.pwd;

        private Boolean loggedin;

        private static string dirMeBIN = Path.GetDirectoryName(Application.StartupPath);
        private static string dirMe = dirMeBIN.Remove((dirMeBIN.IndexOf("bin")));
        private static string typekey_dataPath = (dirMe + "typekey_data" + (char)92);
        private string[] groupPath = Directory.GetDirectories(typekey_dataPath);

        private List<string> groupNames = new List<string>();

        private string docPath;
        
        // LOAD
        private void Overview_Load(object sender, EventArgs e)
        {
            //savetest();
            //loadtest();
            // SHOW LOGIN
            login_show();

            hidePassword();
        }

        // ALL

        private void savetest()
        {
            //table = "typekey-document 1";
            connstr = "server=127.0.0.1;database=" + user + "_db;" + "uid=" + user + ";port=3306;pwd=123;";
            try
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "INSERT INTO `codingmr_db`.`typekey-document 1` (`ID`, `Date started`, `Keys per millisecond`) VALUES ('0', 'today', '50');";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException MSex)
            {
                // RETURN ERROR
            }
        }

        private string GetDBString(string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader[SqlFieldName].Equals(DBNull.Value) ? String.Empty : Reader.GetString(SqlFieldName);
        }

        private void loadtest()
        {
            //table = "typekey-document 1";
            connstr = "server=127.0.0.1;database=" + user + "_db;" + "uid=" + user + ";port=3306;pwd=123;";
            try
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = @"SELECT * FROM `codingmr_db`.`typekey-document 1`";
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    while (Reader.Read())
                    {
                        MessageBox.Show(Reader.GetString(0));
                    }
                    Reader.Close();
                }
            }
            catch (MySqlException MSex)
            {
                // RETURN ERROR
            }
        }

        private void login_show()
        {
            tabcontrol.Hide();
            this.Size = new Size(342, 432);
            login.Show();
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Refresh();
        }
        private void overview_show()
        {
            groupSelect.Text = "Global";

            for (int i = 0; i < groupPath.Length; i++)
            {
                // Isolate folder names form paths
                groupNames.Add(groupPath[i].Remove(0, typekey_dataPath.Length));
                // Add folder names to groupSelect.combobox
                groupSelect.Items.Add(groupNames[i]);
                // Create table for groupName if not exists
                gengroupTables(groupNames[i]);
            }
            login.Hide();
            this.ResizeRedraw = true;
            this.Size = new Size(594, 533);
            tabcontrol.Show();
            tabcontrol.SelectedIndex = 0;
            this.Style = MetroFramework.MetroColorStyle.Green;

            this.Refresh();
            
            // triggers groupselect index change
            groupSelect.SelectedIndex = 1;
            groupSelect.SelectedIndex = 0;

            //savetest();
            //loadtest("");
        }
        private string GetString(MySqlDataReader reader, int colIndex)
        {
            // Checks if the field is empty and if it is it returns an empty string
            // Stops dr.read() from returning false when it reaches field with null value
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }
        private void tabcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabcontrol.SelectedIndex == 0)
            {
                tabcontrol.Style = MetroFramework.MetroColorStyle.Lime;
                tabcontrol.Refresh();
                this.Style = MetroFramework.MetroColorStyle.Lime;
                this.Refresh();
            }
            else if (tabcontrol.SelectedIndex == 1)
            {
                tabcontrol.Style = MetroFramework.MetroColorStyle.Brown;
                tabcontrol.Refresh();
                this.Style = MetroFramework.MetroColorStyle.Brown;
                this.Refresh();
            }
            else
            {
                tabcontrol.Style = MetroFramework.MetroColorStyle.Orange;
                tabcontrol.Refresh();
                this.Style = MetroFramework.MetroColorStyle.Orange;
                this.Refresh();
            }
        }        

        // ACCOUNTS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ START

        private void gotoRegister_Click(object sender, EventArgs e)
        {
            // Change z order of the panel stack
            login.SendToBack();
            login.Hide();
            register.BringToFront();
            register.Show();
        }
        private void gotoLogin_Click(object sender, EventArgs e)
        {
            login.BringToFront();
            register.Hide();
            register.SendToBack();
            login.Show();
        }
        private void createUser_AsROOT(string user, string pass, string name)
        {
            connstr = "server=127.0.0.1;uid=root;port=3306;pwd=homebase;";

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
                    cmd.CommandText = "GRANT ALL PRIVILEGES ON `" + user + (char)92 + "_db`.* TO '" + user + "'@'127.0.0.1' WITH GRANT OPTION";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS `" + user + "_db" + @"`.`global` (
                                `ID` INT NOT NULL AUTO_INCREMENT,
                                `Date today` VARCHAR(18) NULL DEFAULT '" + DateTime.Today.ToString("yyyy-MM-dd") + @"',
                                `Name` VARCHAR(36) NULL,
                                `Time elapsed` FLOAT UNSIGNED ZEROFILL NULL,
                                `Characters per second` FLOAT UNSIGNED ZEROFILL NULL,
                                `Typed` BIGINT UNSIGNED ZEROFILL NULL,
                                `Total` BIGINT UNSIGNED ZEROFILL NULL,
                                `Correct` BIGINT UNSIGNED ZEROFILL NULL,
                                `Stray` BIGINT UNSIGNED ZEROFILL NULL,
                                `Incorrect` BIGINT UNSIGNED ZEROFILL NULL,
                                `Time between characters` TEXT NULL,
                                `Typekey-documents completed` INT UNSIGNED ZEROFILL NULL,
                                `Most Recent typekey-document completed` INT UNSIGNED ZEROFILL NULL,
                                PRIMARY KEY (`ID`));";
                    cmd.ExecuteNonQuery();

                }

                connstr = String.Empty;

            }
            catch (MySqlException MSex)
            {
                // RETURN ERROR
            }

            // RETURN CREATION SUCCESS

            // Goto login
            gotoLogin_Click(null, null);
        }
        private void completeRegister_Click(object sender, EventArgs e)
        {
            createUser_AsROOT(Register_userBox.Text, Register_passBox.Text, Register_nameBox.Text);
        }
        private void hidePassword()
        {
            // saves the character and places a hidden one
            password += Login_passBox.Text;
            password = password.Replace("*", "");

            Login_passBox.Text = "";

            Login_passBox.Text = String.Concat(Enumerable.Repeat("*", password.Length));

            Login_passBox.Select(Login_passBox.Text.Length, 0);
        }
        private void completeLogin_Click(object sender, EventArgs e)
        {
            MessageBox.Show(password);
            user = Login_userBox.Text;
            connectMySQL_AsUSER(Login_userBox.Text, password);

        }
        private void connectMySQL_AsUSER(string user, string pass)
        {
            connstr = "server=127.0.0.1;database=" + user + "_db;" + "uid=" + user + ";port=3306;pwd=" + pass + ";";
            try
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    // RETURN LOGIN SUCCESS
                    loggedin = true;
                    overview_show();
                    // See [Design] for Data binding: uid, pwd and checkOnLogin
                    Properties.Settings.Default.pwd = password;
                    Properties.Settings.Default.Save();
                }
            }
            catch (MySqlException MSex)
            {
                // RETURN ERROR
            }
            savetest();
            loadtest();
        }
        private void Login_passBox_KeyDown(object sender, KeyEventArgs e)
        {
            Login_passBox.Text = "";
            if (e.KeyCode == Keys.OemMinus) e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Back) password = "";
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Delete) && login.Visible)
            {
                e.SuppressKeyPress = true;
                user = Login_userBox.Text;
                connectMySQL_AsUSER(user, password);
            }
        }
        private void Login_passBox_KeyUp(object sender, KeyEventArgs e)
        {
            hidePassword();
        }
        private void Login_userBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                user = Login_userBox.Text;
                connectMySQL_AsUSER(user, password);
                e.SuppressKeyPress = true;
            }
        }
        private void Overview_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) && login.Visible)
            {
                e.SuppressKeyPress = true;
                user = Login_userBox.Text;
                connectMySQL_AsUSER(user, password);
            }
        }
        // ACCOUNTS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END


        // OVERVIEW ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ START
        private void logoutTile_Click(object sender, EventArgs e)
        {
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Text = "Typist";
            this.Refresh();
            loggedin = false;
            connstr = String.Empty;
            Properties.Settings.Default.pwd = String.Empty;
            Login_passBox.Text = String.Empty;
            Properties.Settings.Default.checkAutoLogin = false;
            Properties.Settings.Default.Save();
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
        private void gengroupTables(string groupName)
        {
            try
            {
                using (var conn = new MySqlConnection(connstr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();

                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS `" + user + "_db" + "`.`" + groupName + @"` (
                                `ID` INT NOT NULL AUTO_INCREMENT,
                                `Date today` VARCHAR(18) NULL DEFAULT '" + DateTime.Today.ToString("yyyy-MM-dd") + @"',
                                `Document name` VARCHAR(36) NULL,
                                `Time elapsed` FLOAT UNSIGNED ZEROFILL NULL,
                                `Characters per second` FLOAT UNSIGNED ZEROFILL NULL,
                                `Typed` BIGINT UNSIGNED ZEROFILL NULL,
                                `Total` BIGINT UNSIGNED ZEROFILL NULL,
                                `Correct` BIGINT UNSIGNED ZEROFILL NULL,
                                `Stray` BIGINT UNSIGNED ZEROFILL NULL,
                                `Incorrect` BIGINT UNSIGNED ZEROFILL NULL,
                                `Time between characters` TEXT NULL,
                                PRIMARY KEY (`ID`));";
                     
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
                                // completed == null
                            }
                            else
                            {

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
            if (loggedin)
            {
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
                    string groupPath = typekey_dataPath + groupSelect.Text + (char)92;
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
                        docPanel.Hide();
                    }
                    else
                    {
                        docPanel.Show();
                        // Load doc data
                    }


                    // Load file names
                    // Load doc global data into groupPanel
                    // Load doc data for docSelect.Index[0] into docPanel 
                    // Change playTile.text to Play or Replay if doc data is completed
                }
            }

        }

        private void load_doc()
        {
            string groupPath = typekey_dataPath + groupSelect.Text + (char)92;
            // Load selected doc, doc data into docPanel 
            // Change playTile.text to Play or Replay if doc data is completed

            string docPath = groupPath + docSelect.Text + ".txt";
            if (File.Exists(groupPath + docSelect.Text + ".txt"))
            {
                StreamReader streamReader = new StreamReader(docPath);
                readbox.Text = streamReader.ReadToEnd();
                streamReader.Close();
            }
        }

        private void docSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_doc();
        }
        // OVERVIEW ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END


        // SETTINGS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ START
        // SETTINGS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END


        // PLAY ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ START
        private double count = 0;
        private const int ptime = 3;
        private int pause = ptime;
        private int ctI = 0;
        private int itI = 0;
        private int rtI = 0;
        private int gtI = 0;
        private double kpersec = 0;
        private double kpermin = 0;
        private double wpersec = 0;
        private double wpermin = 0;
        private double lpersec = 0;
        private double lpermin = 0;
        

        private void readbox_Click(object sender, EventArgs e)
        {
            writebox.Select();
        }

        private void writebox_KeyDown(object sender, KeyEventArgs e)
        {
            // itI incorrectly typed Int
            // ctI correctly typed Int
            // rtI recorrectly typed Int

            if (e.KeyCode== Keys.Back)
            {
                // removes credit for backspacing
                // if character is red then -1 itI else correcting = true

                e.SuppressKeyPress = true;

                if (writebox.TextLength > 0) readbox.SelectionStart = writebox.TextLength - 1;
                else readbox.SelectionStart = 0;
                readbox.SelectionLength = 1;

                // correcting mistakes
                if (readbox.SelectionColor == Color.Red)
                {
                    rtI += 1;

                    // I don't even.
                    if (writebox.TextLength > 1) readbox.SelectionStart = writebox.TextLength - 2;
                    else readbox.SelectionStart = 0;
                    if (readbox.SelectionColor == Color.Red) itI -= 1;
                    if (readbox.SelectionColor == Color.Green) ctI -= 1;
                    if (writebox.TextLength > 1) itI -= 1;  ctI -= 1;

                }
                // mistakenly backspacing
                else if (readbox.SelectionColor == Color.Green)
                {
                    gtI += 1;
                    ctI -= 2;
                }

                // remove last character (backspace function)
                if (writebox.TextLength > 0) writebox.Text = writebox.Text.Remove(writebox.TextLength - 1, 1);
                // select the end of the currently typed characters
                writebox.Select(writebox.TextLength, 0);
            }
        }

        private void view_doc()
        {

        }

        private void save_doc()
        {

        }


        // add a loop to check if the tab is used incorrectly and use red text appropriately
        private void writebox_TextChanged(object sender, EventArgs e)
        {
            int charpos;

            char t = '¬';
            char r = '~';

            charpos = writebox.TextLength - 1;
            
            if (charpos >= 1 && charpos <= readbox.Text.Length - 1)
            {
                // start
                speed.Start();
                t = writebox.Text[charpos];
                r = readbox.Text[charpos];
                
                readbox.SelectionStart = charpos;
                readbox.SelectionLength = 1;
                if (t == r)
                {
                    readbox.SelectionColor = Color.Green;
                    ctI += 1;
                }

                else
                {
                    if (readbox.Text[charpos] == ' ')
                    {
                        readbox.SelectionBackColor = Color.Red;
                        itI += 1;
                    }
                    else
                    {
                        readbox.SelectionColor = Color.Red;
                        itI += 1;
                    }
                }
            }
            else if (charpos == 0)
            {
                t = writebox.Text[0];
                r = readbox.Text[0];

                readbox.SelectionStart = 0;
                readbox.SelectionLength = 1;
                if (t == r)
                {
                    readbox.SelectionColor = Color.Green;
                    ctI += 1;
                }
                else
                {
                    if (readbox.Text[0] == ' ')
                    {
                        readbox.SelectionBackColor = Color.Red;
                        itI += 1;
                    }
                    else
                    {
                        readbox.SelectionColor = Color.Red;
                        itI += 1;
                    }
                    
                }
            }
            else
            {
                t = '¬';
                r = '~';
            }

            if (charpos == readbox.Text.Length - 1 && t == r)
            {
                // if the typist has reached the end
                speed.Stop();
                speed.Enabled = false;
                pause = ptime;
                timer1.Enabled = true;

                //show_results();
                //computetypistdata();
            }

            // Turns characters back to black if the backspace is used.
            readbox.Select(charpos+1, readbox.TextLength);
            readbox.SelectionColor = Color.Black;
            readbox.SelectionBackColor = Color.Transparent;

            //label1.Text = t.ToString();
            //label2.Text = r.ToString();
            //label3.Text = charpos.ToString();
            //correctlytyped.Text = "Correctly typed: " + ctI.ToString();
            //incorrectlytyped.Text = "Incorrectly typed: " + itI.ToString();
            
        }

        private void speed_Tick(object sender, EventArgs e)
        {
            count++;
            timeElapsed.Text = TimeSpan.FromSeconds(count).ToString();
        }
        
        private void show_results()
        {
            /* Hide writebox and readbox
             * Display:
             *  - Keys per second (defualt)
             *  - Words per minute (default)
             *  - Lines per minute (default)
             *  - Incorrect
             *  - Corrected mistakes
             *  - Correct
             *  - Key accuracy
             *  - Longest time inbetween keys
             *  - Shortest time inbetween keys
             *  - Average time inbetween keys
             * 
             * Show finish, finish without saving, replay, replay without saving
            */

            if (ctI < 0) ctI = 0;

            ctI = writebox.TextLength - itI;


            correctKeys.Text = ctI.ToString();
            incorrectKeys.Text = itI.ToString();
            correctedMistakes.Text = (rtI + gtI).ToString();
            //keysPersec.Text = (((writebox.TextLength - itI) / (count / 10)) * 60).ToString() + " ps";

            kpersec = ((writebox.TextLength - itI) / count);
            kpermin = kpersec * 60;

            wpersec = ((writebox.TextLength - itI) / (count / 5));
            wpermin = wpersec * 60;

            //lpersec = lines / count;
            //lpermin = lpersec * 60;
            
            keyLabel.Text = "Keys per second";
            keysPersec.Text = kpersec.ToString("F") + " ps";

            keyLabel.Text = "Words per minute";
            wordsPermin.Text = wpermin.ToString("F") + " pm";

            linesLabel.Text = "Lines per minute";
            //linesPermin.Text = lpermin.ToString("F") + " pm";


            writebox.Hide();
            readbox.Hide();
            results_timeElapsed.Text = timeElapsed.Text;
            timeElapsed.Hide();
            results.Show();

        }

        private void keysPersec_Click(object sender, EventArgs e)
        {
            // Change unit of measure of keys on click
            if (keyLabel.Text == "Keys per second")
            {
                keyLabel.Text = "Keys per minute";
                keysPersec.Text = kpermin.ToString("F") + " pm";
            }
            else
            {
                keyLabel.Text = "Keys per second";
                keysPersec.Text = kpersec.ToString("F") + " ps";
            }
        }
        private void keyLabel_Click(object sender, EventArgs e)
        {
            // Change unit of measure of keys on click
            if (keyLabel.Text == "Keys per second")
            {
                keyLabel.Text = "Keys per minute";
                keysPersec.Text = kpermin.ToString("F") + " pm";
            }
            else
            {
                keyLabel.Text = "Keys per second";
                keysPersec.Text = kpersec.ToString("F") + " ps";
            }
        }

        private void wordsPermin_Click(object sender, EventArgs e)
        {
            // Change unit of measure of words on click
            if (wordsLabel.Text == "words per second")
            {
                wordsLabel.Text = "words per minute";
                wordsPermin.Text = wpermin.ToString("F") + " pm";
            }
            else
            {
                wordsLabel.Text = "words per second";
                wordsPermin.Text = wpersec.ToString("F") + " ps";
            }
        }
        private void wordsLabel_Click(object sender, EventArgs e)
        {
            // Change unit of measure of words on click
            if (wordsLabel.Text == "words per second")
            {
                wordsLabel.Text = "words per minute";
                wordsPermin.Text = wpermin.ToString("F") + " pm";
            }
            else
            {
                wordsLabel.Text = "words per second";
                wordsPermin.Text = wpersec.ToString("F") + " ps";
            }
        }

        private void linesPermin_Click(object sender, EventArgs e)
        {

        }
        private void linesLabel_Click(object sender, EventArgs e)
        {

        }

        private void longestKeys_Click(object sender, EventArgs e)
        {

        }
        private void longtimeLabel_Click(object sender, EventArgs e)
        {

        }

        private void shortestKeys_Click(object sender, EventArgs e)
        {

        }
        private void shorttimeLabel_Click(object sender, EventArgs e)
        {

        }

        private void averagetimeLabel_Click(object sender, EventArgs e)
        {

        }
        private void averageKeys_Click(object sender, EventArgs e)
        {

        }

        private void save()
        {

        }


        private void finishSave_Click(object sender, EventArgs e)
        {
            save();
            results.Hide();
            readbox.Text = "";
            writebox.Text = "";
            timeElapsed.Text = "00:00:00";
            timeElapsed.Show();
            tabcontrol.SelectedIndex = 0;
        }

        private void finishDiscard_Click(object sender, EventArgs e)
        {
            readbox.Text = "";
            writebox.Text = "";
            results.Hide();
            timeElapsed.Text = "00:00:00";
            timeElapsed.Show();
            tabcontrol.SelectedIndex = 0;
        }

        private void replayDiscard_Click(object sender, EventArgs e)
        {
            readbox.Text = "";
            writebox.Text = "";
            results.Hide();
            writebox.Show();
            readbox.Show();
            timeElapsed.Text = "00:00:00";
            timeElapsed.Show();
            load_doc();
        }

        private void replaySave_Click(object sender, EventArgs e)
        {
            save();
            readbox.Text = "";
            writebox.Text = "";
            results.Hide();
            writebox.Show();
            readbox.Show();
            timeElapsed.Text = "00:00:00";
            timeElapsed.Show();
            load_doc();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pause--;
            if (pause == 0)
            {
                show_results();
                timer1.Enabled = false;
            }
                
        }



        /*
        private void computetypistdata()
        {

            // calculations
            charperminute = ((ctI / (count / 10)) * 60);
            wordsperminute = ((ctI / (count / 10)) * 60) / 5;
            // TODO get the average and sum of archive

            // temp REL
            main.todayswordspermin = wordsperminute.ToString(); // wrong
            main.todayscharacterspermin = 

            //main.charactersperminA = charperminute.ToString();
            main.totalcharactersA[0] = (Convert.ToInt32(main.totalcharactersA[0]) + ctI).ToString();
            main.wrongcharacters = (Convert.ToInt32(main.wrongcharacters) + itI).ToString();
            // TODO add the words per minute to the list, then reaverage wordsperminute[0]
            main.wordspermin = wordsperminute.ToString();

            DialogResult result = MessageBox.Show("Correctly typed characters: " + ctI.ToString() 
                + "\n\rIncorrectly typed characters: " + itI.ToString()
                + "\n\rCharacters per minute: " + charperminute.ToString()
                + "\n\rWords per minute: " + wordsperminute.ToString()
                + "\n\r\n\rWould you like save these results?", "Results", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                main.saveprofile();
                main.loadprofile();
            }

            this.Hide();
        }
         */
        // PLAY ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END

    }
}
