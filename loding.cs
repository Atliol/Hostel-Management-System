using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Hostel_Management_Syatem
{
    public partial class loding : Form
    {
        public loding()
        {
            InitializeComponent();
            timer1.Start();
        }

      
        private void loding_Load(object sender, EventArgs e)
        {
           
           
        }
        int startp = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startp += 1;
            myprogress.Value = startp;
            label3.Text = startp + "%";
            if (myprogress.Value == 100)
            {
                myprogress.Value = 0;
                Login obj = new Login();
                obj.Show();
                this.Hide();
                timer1.Stop();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
