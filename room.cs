using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hostel_Management_Syatem
{
    public partial class room : Form
    {
        public room()
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
        //loadroomtypeအတွက်
        private void LoadRoomType()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter da = new SqlDataAdapter("SELECT rtid,roomtype FROM roomtype", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            roomcom.DataSource = dt;
            roomcom.DisplayMember = "roomtype";
            roomcom.ValueMember = "rtid";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //data view 
        private void BindGrid()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            SqlCommand cmd = new SqlCommand("SELECT room_no,roomtype,capacity FROM room", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["room_no"].HeaderText = "Room No";
            dataGridView1.Columns["roomtype"].HeaderText = "Room Type";
            dataGridView1.Columns["capacity"].HeaderText = "Capacity";
         
        }

        private void room_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'thazinnwehostalDataSet.room' table. You can move, or remove it, as needed.
            
            LoadRoomType();
            BindGrid();
            MakePanelRound(panel2, 20);
            MakePanelRound(panel6, 24);
            MakePanelRound(panel3, 15);
        }

        

        private void ronno_TextChanged(object sender, EventArgs e)
        {

        }

        //room insertအတွက်
        private void button1_Click(object sender, EventArgs e)
        {
            int roomNo;
            if(!int.TryParse(roomnotxt.Text,out roomNo))
            {
                MessageBox.Show("0,1,2...သာထည့်ပါ", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int capacity;
            if (!int.TryParse(capacitytxt.Text, out capacity))
            {
                MessageBox.Show("1,2သာထည့်ပါ", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int roomTypeID = Convert.ToInt32(roomcom.SelectedValue);
            string roomtype = ((DataRowView)roomcom.SelectedItem)["roomtype"].ToString();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "insert into room(rtid,room_no,capacity,roomtype)values (@rtid,@room_no,@capacity,@roomtype)";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@room_no", roomNo);
                cmd.Parameters.AddWithValue("@rtid", roomTypeID);
                cmd.Parameters.AddWithValue("@roomtype", roomtype);
                cmd.Parameters.AddWithValue("@capacity", capacity);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successful!");
                roomnotxt.Text = "";
                capacitytxt.Text = "";
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    
        //room insert အဆုံး
        

        private void home_Click_1(object sender, EventArgs e)
        {
            Mainform mainform = new Mainform();
            mainform.Show();
            this.Hide();
        }

        private void student_Click(object sender, EventArgs e)
        {
            student std = new student();
            std.Show();
            this.Hide();
        }

        private void fee_Click(object sender, EventArgs e)
        {
            fee fee = new fee();
            fee.Show();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
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

        //room update အတွက်
        private void updatebtn_Click(object sender, EventArgs e)
        {
            int roomNo;
            if (!int.TryParse(roomnotxt.Text, out roomNo))
            {
                MessageBox.Show("0,1,2...သာထည့်ပါ", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int capacity;
            if (!int.TryParse(capacitytxt.Text, out capacity))
            {
                MessageBox.Show("1,2သာထည့်ပါ", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int roomTypeID = Convert.ToInt32(roomcom.SelectedValue);
            string roomtype = ((DataRowView)roomcom.SelectedItem)["roomtype"].ToString();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "update room set rtid=@rtid, room_no=@room_no, capacity=@capacity, roomtype=@roomtype where room_no=@room_no";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@room_no", roomNo);
                cmd.Parameters.AddWithValue("@rtid", roomTypeID);
                cmd.Parameters.AddWithValue("@roomtype", roomtype);
                cmd.Parameters.AddWithValue("@capacity", capacity);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successful!");
                roomnotxt.Text = "";
                capacitytxt.Text = "";
                BindGrid(); // Refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        //room update အဆုံး




        private void roomcom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //room delete အတွက်
        private void deletebtn_Click(object sender, EventArgs e)
        {

            // Prompt for student name
            string inputName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the Room No to delete:",
                "Delete Room",
                "");

            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("Room No is required to delete.");
                return;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");
            string query = "DELETE FROM room WHERE room_no = @room_no";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@room_no", inputName);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Delete Successful!");
                    BindGrid(); // Refresh grid
                }
                else
                {
                    MessageBox.Show("Room No ရှာမတွေ့ပါ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


       

        }
        //room delete အဆုံး
        private void label5_Click(object sender, EventArgs e)
        {
            visitor vi = new visitor();
            vi.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //add new roomtype
        

        private void button1_Click_2(object sender, EventArgs e)
        {

            // Prompt for student name
            string inputName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the Room Type :",
                "ADD Room Type",
                "");

            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("Room Type required");
                return;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "insert into room(roomtype)values (@roomtype)";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
          
                cmd.Parameters.AddWithValue("@roomtype", inputName);
                

                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        //end

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure it's not header row
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Example: Fill student name textbox
                roomnotxt.Text = row.Cells["room_no"].Value.ToString();

                // Other fields (if needed)
                capacitytxt.Text = row.Cells["capacity"].Value.ToString();
                roomcom.Text = row.Cells["roomtype"].Value.ToString();
              
            }
        }
       
    }
}
