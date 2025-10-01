using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hostel_Management_Syatem
{
    public partial class Mainform : Form
    {
        public Mainform()
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
      

        

        private void label3_Click(object sender, EventArgs e)
        {
            student std = new student();
            std.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            room rm = new room();
            rm.Show();
            this.Hide();
        }



        private void Mainform_Load(object sender, EventArgs e)
        {
            ShowRoomCount();
            ShowOnePersonRoomFee();
            ShowComputerStudentCount();
            ShowGTHSStudentCount();
            ShowtwoPersonRoomFee();
            ShowAvailableRooms();
            ShowRemainingCapacity();
            MakePanelRound(panel2, 24);
            MakePanelRound(panel6, 24);
            MakePanelRound(panel7, 24);
            MakePanelRound(panel8, 24);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            logout lou = new logout();
            lou.Show();
            this.Hide();

        }

      
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void fee_Click(object sender, EventArgs e)
        {
            fee fee = new fee();
            fee.Show();
            this.Hide();


        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            about ab = new about();
            ab.Show();
            this.Hide();

        }

        private void home_Click_1(object sender, EventArgs e)
        {
            Mainform mainform = new Mainform();
            mainform.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        //room countအတွက်
        private void ShowRoomCount()
        {
            int roomCount = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string query = "SELECT COUNT(*) FROM room";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    roomCount = (int)cmd.ExecuteScalar();
                }
            }
            nowroom.Text = $"{roomCount}";
        }
        //end
        //computer student count
        private void ShowComputerStudentCount()
        {
            int computerCount = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string query = "SELECT COUNT(*) FROM Student WHERE school = @school";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@school", "Computer");
                    conn.Open();
                    computerCount = (int)cmd.ExecuteScalar();
                }
            }
            computerrn.Text = $"{computerCount}";
        }
        //end
        //GTHS student count
        private void ShowGTHSStudentCount()
        {
            int computerCount = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string query = "SELECT COUNT(*) FROM Student WHERE school = @school";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@school", "GTHS");
                    conn.Open();
                    computerCount = (int)cmd.ExecuteScalar();
                }
            }
            gthsrn.Text = $"{computerCount}";
        }
        //end

        //one person room count
        private void ShowOnePersonRoomFee()
        {
            object feeValue = null;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string query = "SELECT room_fee FROM fee WHERE roomtype = @roomtype";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@roomtype", "1 Person Room");
                    conn.Open();
                    feeValue = cmd.ExecuteScalar();
                }
            }
            operson.Text = feeValue != null ? $" {feeValue} Ks" : "No fee data";
        }//end


        //two person room count
        private void ShowtwoPersonRoomFee()
        {
            object feeValue = null;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string query = "SELECT room_fee FROM fee WHERE roomtype = @roomtype";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@roomtype", "2 Person Room");
                    conn.Open();
                    feeValue = cmd.ExecuteScalar();
                }
            }
            tperson.Text = feeValue != null ? $" {feeValue} Ks" : "No fee data";
        }//end

        private void nowroom_Click(object sender, EventArgs e)
        {

        }

        private void operson_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_3(object sender, EventArgs e)
        {
            visitor vi = new visitor();
            vi.Show();
            this.Hide();
        }

        //Room avail
        private void ShowAvailableRooms()
        {
            List<string> availableRooms = new List<string>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string query = @"
            SELECT room.room_no 
            FROM room
            LEFT JOIN Student ON room.rid = Student.rid
            WHERE Student.rid IS NULL";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            availableRooms.Add(reader["room_no"].ToString());
                        }
                    }
                }
            }
            if (availableRooms.Count > 0)
                label3.Text = string.Join(", ", availableRooms);
            else
                label3.Text = "No available rooms";
        }
        //end

        //room capacity
        private void ShowRemainingCapacity()
        {
            int totalCapacity = 0;
            int totalStudents = 0;

            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                // Get total capacity from room table
                string capacityQuery = "SELECT SUM(capacity) FROM room";
                using (SqlCommand cmd = new SqlCommand(capacityQuery, conn))
                {
                    conn.Open();
                    object capResult = cmd.ExecuteScalar();
                    if (capResult != DBNull.Value && capResult != null)
                        totalCapacity = Convert.ToInt32(capResult);
                    conn.Close();
                }

                // Get total students from Student table
                string studentQuery = "SELECT COUNT(*) FROM Student";
                using (SqlCommand cmd = new SqlCommand(studentQuery, conn))
                {
                    conn.Open();
                    totalStudents = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
            }

            int remainingCapacity = totalCapacity - totalStudents;
            ccpp.Text = $"{remainingCapacity}";
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
