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

namespace Typist
{
    public partial class Accounts : MetroForm
    {
        public Accounts()
        {
            InitializeComponent();
        }

        private void Accounts_Load(object sender, EventArgs e)
        {
            if (autoLogin.Checked) connectMySQL_AsUSER(Login_userBox.Text, Login_passBox.Text);
        }

        private void gotoRegister_Click(object sender, EventArgs e)
        {
            // Change z order of the panel stack
            login.SendToBack();
            register.BringToFront();
        }

        private void gotoLogin_Click(object sender, EventArgs e)
        {
            login.BringToFront();
            register.SendToBack();
        }

        private void createUser_AsROOT(string user, string pass)
        {
            // TODO: Disallow use of the character '_' so there can be no error with create database
            // TODO: MetroUI MessageBox
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
                        MessageBox.Show(e.Message);
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
                }

            }
            catch (MySqlException MSex)
            {
                MessageBox.Show(MSex.Message);
            }

            MessageBox.Show("Created.");

            // Goto login
            login.BringToFront();
            register.SendToBack();

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
                    MessageBox.Show("Success");
                    // See [Design] for Data binding: uid, pwd and checkOnLogin
                    Properties.Settings.Default.Save();
                }
            }
            catch (MySqlException MSex)
            {
                MessageBox.Show(MSex.Message);
            }

        }

        private void completeLogin_Click(object sender, EventArgs e)
        {
            connectMySQL_AsUSER(Login_userBox.Text, Login_passBox.Text);

        }

        private void completeRegister_Click(object sender, EventArgs e)
        {
            createUser_AsROOT(Register_userBox.Text, Register_passBox.Text);
        }
    }
}
