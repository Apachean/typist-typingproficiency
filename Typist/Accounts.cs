using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MySql.Data.MySqlClient;

namespace Typist
{
    public partial class Accounts : MetroFramework.Forms.MetroForm
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
            string connstr = "Server=localhost;user=root;port3306;password=homebase;";
            using (var conn = new MySqlConnection(connstr))
            using (var cmd = conn.CreateCommand())
            {
                // open, 
                conn.Open();
                // Set the authentication to sha256 passwords
                cmd.CommandText = "CREATE USER '" + user + "'@'localhost' IDENTIFIED BY sha256_password;";
                cmd.CommandText = "SET old_passwords = 2;";
                // Set password
                cmd.CommandText = "SET PASSWORD FOR '" + user + "'@'localhost' = PASSWORD('" + pass + "');";

                // Create user's table data database
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS '" + user + "_db'";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    // User already exists?
                    throw;
                }
            }
        }

        private void connectMySQL_AsUSER(string user, string pass)
        {
            string connstr = "Server=localhost;Database=" + user + "_db;" + "user=" + user + ";port3306;password=" + pass + ";";
            using (var conn = new MySqlConnection(connstr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

            }

        }
    }
}
