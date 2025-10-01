using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hostel_Management_Syatem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void logbtn_Click(object sender, EventArgs e)
        {



            string correctUsername = "admin";
            string correctPassword = "1234";

            // User input
            string username = this.username.Text;
            string password = this.password.Text;

            if (username == correctUsername && password == correctPassword)
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Mainform main = new Mainform();
                main.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Show password
                password.UseSystemPasswordChar = false;
            }
            else
            {
                // Hide password
                password.UseSystemPasswordChar = true;
            }

        }
    }
}
