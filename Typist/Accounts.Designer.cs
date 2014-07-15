namespace Typist
{
    partial class Accounts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.login = new MetroFramework.Controls.MetroPanel();
            this.metroCheckBox1 = new MetroFramework.Controls.MetroCheckBox();
            this.spinnerLogin = new MetroFramework.Controls.MetroProgressSpinner();
            this.gotoRegister = new MetroFramework.Controls.MetroLink();
            this.completeLogin = new MetroFramework.Controls.MetroButton();
            this.Login_userBox = new MetroFramework.Controls.MetroTextBox();
            this.Login_passBox = new MetroFramework.Controls.MetroTextBox();
            this.register = new MetroFramework.Controls.MetroPanel();
            this.spinnerRegister = new MetroFramework.Controls.MetroProgressSpinner();
            this.Register_nameBox = new MetroFramework.Controls.MetroTextBox();
            this.gotoLogin = new MetroFramework.Controls.MetroLink();
            this.completeRegister = new MetroFramework.Controls.MetroButton();
            this.Register_userBox = new MetroFramework.Controls.MetroTextBox();
            this.Register_passBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.login.SuspendLayout();
            this.register.SuspendLayout();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.login.Controls.Add(this.metroCheckBox1);
            this.login.Controls.Add(this.spinnerLogin);
            this.login.Controls.Add(this.gotoRegister);
            this.login.Controls.Add(this.completeLogin);
            this.login.Controls.Add(this.Login_userBox);
            this.login.Controls.Add(this.Login_passBox);
            this.login.CustomBackground = false;
            this.login.HorizontalScrollbar = false;
            this.login.HorizontalScrollbarBarColor = false;
            this.login.HorizontalScrollbarHighlightOnWheel = false;
            this.login.HorizontalScrollbarSize = 0;
            this.login.Location = new System.Drawing.Point(23, 78);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(296, 314);
            this.login.Style = MetroFramework.MetroColorStyle.White;
            this.login.StyleManager = null;
            this.login.TabIndex = 0;
            this.login.Theme = MetroFramework.MetroThemeStyle.Light;
            this.login.VerticalScrollbar = false;
            this.login.VerticalScrollbarBarColor = false;
            this.login.VerticalScrollbarHighlightOnWheel = false;
            this.login.VerticalScrollbarSize = 0;
            // 
            // metroCheckBox1
            // 
            this.metroCheckBox1.AutoSize = true;
            this.metroCheckBox1.CustomBackground = false;
            this.metroCheckBox1.CustomForeColor = true;
            this.metroCheckBox1.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.metroCheckBox1.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.metroCheckBox1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.metroCheckBox1.Location = new System.Drawing.Point(13, 284);
            this.metroCheckBox1.Name = "metroCheckBox1";
            this.metroCheckBox1.Size = new System.Drawing.Size(89, 19);
            this.metroCheckBox1.Style = MetroFramework.MetroColorStyle.Lime;
            this.metroCheckBox1.StyleManager = null;
            this.metroCheckBox1.TabIndex = 8;
            this.metroCheckBox1.Text = "Auto login";
            this.metroCheckBox1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroCheckBox1.UseStyleColors = true;
            this.metroCheckBox1.UseVisualStyleBackColor = true;
            // 
            // spinnerLogin
            // 
            this.spinnerLogin.Backwards = true;
            this.spinnerLogin.CustomBackground = false;
            this.spinnerLogin.Location = new System.Drawing.Point(143, 33);
            this.spinnerLogin.Maximum = 100;
            this.spinnerLogin.Name = "spinnerLogin";
            this.spinnerLogin.Size = new System.Drawing.Size(16, 16);
            this.spinnerLogin.Style = MetroFramework.MetroColorStyle.Teal;
            this.spinnerLogin.StyleManager = null;
            this.spinnerLogin.TabIndex = 7;
            this.spinnerLogin.Theme = MetroFramework.MetroThemeStyle.Light;
            this.spinnerLogin.Value = 33;
            // 
            // gotoRegister
            // 
            this.gotoRegister.CustomBackground = false;
            this.gotoRegister.CustomForeColor = true;
            this.gotoRegister.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.gotoRegister.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.gotoRegister.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.gotoRegister.Location = new System.Drawing.Point(207, 288);
            this.gotoRegister.Name = "gotoRegister";
            this.gotoRegister.Size = new System.Drawing.Size(75, 23);
            this.gotoRegister.Style = MetroFramework.MetroColorStyle.Blue;
            this.gotoRegister.StyleManager = null;
            this.gotoRegister.TabIndex = 6;
            this.gotoRegister.Text = "REGISTER";
            this.gotoRegister.Theme = MetroFramework.MetroThemeStyle.Light;
            this.gotoRegister.UseStyleColors = false;
            this.gotoRegister.Click += new System.EventHandler(this.gotoRegister_Click);
            // 
            // completeLogin
            // 
            this.completeLogin.Highlight = false;
            this.completeLogin.Location = new System.Drawing.Point(13, 221);
            this.completeLogin.Name = "completeLogin";
            this.completeLogin.Size = new System.Drawing.Size(270, 45);
            this.completeLogin.Style = MetroFramework.MetroColorStyle.Blue;
            this.completeLogin.StyleManager = null;
            this.completeLogin.TabIndex = 5;
            this.completeLogin.Text = "LOGIN";
            this.completeLogin.Theme = MetroFramework.MetroThemeStyle.Light;
            this.completeLogin.Click += new System.EventHandler(this.completeLogin_Click);
            // 
            // Login_userBox
            // 
            this.Login_userBox.CustomBackground = false;
            this.Login_userBox.CustomForeColor = true;
            this.Login_userBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Login_userBox.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.Login_userBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Login_userBox.Location = new System.Drawing.Point(13, 122);
            this.Login_userBox.Multiline = true;
            this.Login_userBox.Name = "Login_userBox";
            this.Login_userBox.SelectedText = "";
            this.Login_userBox.Size = new System.Drawing.Size(270, 36);
            this.Login_userBox.Style = MetroFramework.MetroColorStyle.Blue;
            this.Login_userBox.StyleManager = null;
            this.Login_userBox.TabIndex = 4;
            this.Login_userBox.Text = "Username";
            this.Login_userBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Login_userBox.UseStyleColors = false;
            // 
            // Login_passBox
            // 
            this.Login_passBox.CustomBackground = false;
            this.Login_passBox.CustomForeColor = true;
            this.Login_passBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Login_passBox.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.Login_passBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Login_passBox.Location = new System.Drawing.Point(13, 164);
            this.Login_passBox.Multiline = true;
            this.Login_passBox.Name = "Login_passBox";
            this.Login_passBox.SelectedText = "";
            this.Login_passBox.Size = new System.Drawing.Size(270, 36);
            this.Login_passBox.Style = MetroFramework.MetroColorStyle.Blue;
            this.Login_passBox.StyleManager = null;
            this.Login_passBox.TabIndex = 3;
            this.Login_passBox.Text = "Password";
            this.Login_passBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Login_passBox.UseStyleColors = false;
            // 
            // register
            // 
            this.register.BackColor = System.Drawing.Color.IndianRed;
            this.register.Controls.Add(this.spinnerRegister);
            this.register.Controls.Add(this.Register_nameBox);
            this.register.Controls.Add(this.gotoLogin);
            this.register.Controls.Add(this.completeRegister);
            this.register.Controls.Add(this.Register_userBox);
            this.register.Controls.Add(this.Register_passBox);
            this.register.CustomBackground = false;
            this.register.HorizontalScrollbar = false;
            this.register.HorizontalScrollbarBarColor = false;
            this.register.HorizontalScrollbarHighlightOnWheel = false;
            this.register.HorizontalScrollbarSize = 0;
            this.register.Location = new System.Drawing.Point(23, 78);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(296, 314);
            this.register.Style = MetroFramework.MetroColorStyle.Blue;
            this.register.StyleManager = null;
            this.register.TabIndex = 1;
            this.register.Theme = MetroFramework.MetroThemeStyle.Light;
            this.register.VerticalScrollbar = false;
            this.register.VerticalScrollbarBarColor = false;
            this.register.VerticalScrollbarHighlightOnWheel = false;
            this.register.VerticalScrollbarSize = 0;
            // 
            // spinnerRegister
            // 
            this.spinnerRegister.Backwards = true;
            this.spinnerRegister.CustomBackground = false;
            this.spinnerRegister.Location = new System.Drawing.Point(143, 33);
            this.spinnerRegister.Maximum = 100;
            this.spinnerRegister.Name = "spinnerRegister";
            this.spinnerRegister.Size = new System.Drawing.Size(16, 16);
            this.spinnerRegister.Style = MetroFramework.MetroColorStyle.Teal;
            this.spinnerRegister.StyleManager = null;
            this.spinnerRegister.TabIndex = 3;
            this.spinnerRegister.Theme = MetroFramework.MetroThemeStyle.Light;
            this.spinnerRegister.Value = 33;
            // 
            // Register_nameBox
            // 
            this.Register_nameBox.CustomBackground = false;
            this.Register_nameBox.CustomForeColor = true;
            this.Register_nameBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Register_nameBox.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.Register_nameBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Register_nameBox.Location = new System.Drawing.Point(13, 80);
            this.Register_nameBox.Multiline = true;
            this.Register_nameBox.Name = "Register_nameBox";
            this.Register_nameBox.SelectedText = "";
            this.Register_nameBox.Size = new System.Drawing.Size(270, 36);
            this.Register_nameBox.Style = MetroFramework.MetroColorStyle.Blue;
            this.Register_nameBox.StyleManager = null;
            this.Register_nameBox.TabIndex = 11;
            this.Register_nameBox.Text = "Name";
            this.Register_nameBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Register_nameBox.UseStyleColors = false;
            // 
            // gotoLogin
            // 
            this.gotoLogin.CustomBackground = false;
            this.gotoLogin.CustomForeColor = true;
            this.gotoLogin.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.gotoLogin.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.gotoLogin.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.gotoLogin.Location = new System.Drawing.Point(207, 288);
            this.gotoLogin.Name = "gotoLogin";
            this.gotoLogin.Size = new System.Drawing.Size(75, 23);
            this.gotoLogin.Style = MetroFramework.MetroColorStyle.Blue;
            this.gotoLogin.StyleManager = null;
            this.gotoLogin.TabIndex = 10;
            this.gotoLogin.Text = "LOGIN";
            this.gotoLogin.Theme = MetroFramework.MetroThemeStyle.Light;
            this.gotoLogin.UseStyleColors = false;
            this.gotoLogin.Click += new System.EventHandler(this.gotoLogin_Click);
            // 
            // completeRegister
            // 
            this.completeRegister.Highlight = false;
            this.completeRegister.Location = new System.Drawing.Point(13, 221);
            this.completeRegister.Name = "completeRegister";
            this.completeRegister.Size = new System.Drawing.Size(270, 45);
            this.completeRegister.Style = MetroFramework.MetroColorStyle.Blue;
            this.completeRegister.StyleManager = null;
            this.completeRegister.TabIndex = 9;
            this.completeRegister.Text = "REGISTER";
            this.completeRegister.Theme = MetroFramework.MetroThemeStyle.Light;
            this.completeRegister.Click += new System.EventHandler(this.completeRegister_Click);
            // 
            // Register_userBox
            // 
            this.Register_userBox.CustomBackground = false;
            this.Register_userBox.CustomForeColor = true;
            this.Register_userBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Register_userBox.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.Register_userBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Register_userBox.Location = new System.Drawing.Point(13, 122);
            this.Register_userBox.Multiline = true;
            this.Register_userBox.Name = "Register_userBox";
            this.Register_userBox.SelectedText = "";
            this.Register_userBox.Size = new System.Drawing.Size(270, 36);
            this.Register_userBox.Style = MetroFramework.MetroColorStyle.Blue;
            this.Register_userBox.StyleManager = null;
            this.Register_userBox.TabIndex = 8;
            this.Register_userBox.Text = "Username";
            this.Register_userBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Register_userBox.UseStyleColors = false;
            // 
            // Register_passBox
            // 
            this.Register_passBox.CustomBackground = false;
            this.Register_passBox.CustomForeColor = true;
            this.Register_passBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Register_passBox.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.Register_passBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Register_passBox.Location = new System.Drawing.Point(13, 164);
            this.Register_passBox.Multiline = true;
            this.Register_passBox.Name = "Register_passBox";
            this.Register_passBox.SelectedText = "";
            this.Register_passBox.Size = new System.Drawing.Size(270, 36);
            this.Register_passBox.Style = MetroFramework.MetroColorStyle.Blue;
            this.Register_passBox.StyleManager = null;
            this.Register_passBox.TabIndex = 7;
            this.Register_passBox.Text = "Password";
            this.Register_passBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Register_passBox.UseStyleColors = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.CustomBackground = false;
            this.metroLabel1.CustomForeColor = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel1.Location = new System.Drawing.Point(142, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(59, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.StyleManager = null;
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Typist";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel1.UseStyleColors = false;
            // 
            // Accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 432);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.register);
            this.Controls.Add(this.login);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Accounts";
            this.ShadowType = MetroFramework.Forms.ShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Load += new System.EventHandler(this.Accounts_Load);
            this.login.ResumeLayout(false);
            this.login.PerformLayout();
            this.register.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel login;
        private MetroFramework.Controls.MetroPanel register;
        private MetroFramework.Controls.MetroLink gotoRegister;
        private MetroFramework.Controls.MetroButton completeLogin;
        private MetroFramework.Controls.MetroTextBox Login_userBox;
        private MetroFramework.Controls.MetroTextBox Login_passBox;
        private MetroFramework.Controls.MetroTextBox Register_nameBox;
        private MetroFramework.Controls.MetroLink gotoLogin;
        private MetroFramework.Controls.MetroButton completeRegister;
        private MetroFramework.Controls.MetroTextBox Register_userBox;
        private MetroFramework.Controls.MetroTextBox Register_passBox;
        private MetroFramework.Controls.MetroProgressSpinner spinnerRegister;
        private MetroFramework.Controls.MetroProgressSpinner spinnerLogin;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}

