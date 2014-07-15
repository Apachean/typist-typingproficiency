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

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    // Give user access to the database created for them
                    // Char 92 is '\'
                    cmd.CommandText = "GRANT ALL PRIVILEGES ON `" + user + (char) 92 + "_db`.* TO '" + user + "'@'127.0.0.1' WITH GRANT OPTION";

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

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
