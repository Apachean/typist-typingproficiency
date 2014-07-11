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
    }
}
