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

namespace Hostel_Management_Syatem
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
        }

        private void MakePanelRound(Panel panel, int radius)
        {

            Rectangle bounds = new Rectangle(0, 0, panel.Width, panel.Height);
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);

            Rectangle arc = new Rectangle(bounds.Location, size);
            // top left
            path.AddArc(arc, 180, 90);

            // top right
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            panel.Region = new Region(path);

        }
        private void about_Load(object sender, EventArgs e)
        {
            MakePanelRound(panel2, 20);
            MakePanelRound(panel3, 20);
            MakePanelRound(panel5, 20);
            MakePanelRound(panel8, 24);
            MakePanelRound(panel7, 20);
            MakePanelRound(panel4, 20);
            MakePanelRound(panel6, 24);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void room_Click(object sender, EventArgs e)
        {
            room room= new room();
            room.Show();
            this.Hide();

        }

        private void student_Click(object sender, EventArgs e)
        {
            student student = new student();
            student.Show();
            this.Hide();
        }

        private void home_Click(object sender, EventArgs e)
        {
            Mainform mainform = new Mainform();
            mainform.Show();
            this.Hide();
        }

        private void fee_Click(object sender, EventArgs e)
        {
            fee fee = new fee();
            fee.Show();
            this.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            about ab = new about();
            ab.Show();
            this.Hide();


        }

        private void logout_Click(object sender, EventArgs e)
        {
            logout lou = new logout();
            lou.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            visitor vi = new visitor();
            vi.Show();
            this.Close();

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
