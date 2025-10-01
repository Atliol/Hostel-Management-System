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
    public partial class fee : Form
    {
        public fee()
        {
            InitializeComponent();
        }

        private void fee_Load(object sender, EventArgs e)
        {
            BindGrid();
            LoadRoomType();
            MakePanelRound(panel2, 20);
            MakePanelRound(panel6, 24);
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
        //loadroomtype end


        private void hometxt_Click(object sender, EventArgs e)
        {
            Mainform mainform = new Mainform();
            mainform.Show();
            this.Hide();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            fee fee = new fee();
            fee.Show();
            this.Hide();

        }

        private void student_Click_1(object sender, EventArgs e)
        {
            student std = new student();
            std.Show();
            this.Hide();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            about ab = new about();
            ab.Show();
            this.Hide();
        }

        private void logout_Click_1(object sender, EventArgs e)
        {
            logout lou = new logout();
            lou.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        //fee insert start
        private void addbtn_Click(object sender, EventArgs e)
        {

            int roomfee;
            if (!int.TryParse(roomfeetxt.Text, out roomfee))
            {
                MessageBox.Show("ဂဏန်းသာထည့်ပါ", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            

            int roomTypeID = Convert.ToInt32(roomcom.SelectedValue);
            string roomType = ((DataRowView)roomcom.SelectedItem)["roomtype"].ToString();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "insert into fee(rtid,room_fee,roomtype)values (@rtid,@room_fee,@roomtype)";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@room_fee", roomfee);
                cmd.Parameters.AddWithValue("@rtid", roomTypeID);
                cmd.Parameters.AddWithValue("@roomtype", roomType);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successful!");
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
        //fee insert end
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
       

        //datagridview 
        private void BindGrid()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            SqlCommand cmd = new SqlCommand("SELECT fid,rtid,room_fee,roomtype FROM fee ", conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            dataGridView1.Columns.Clear();

            // Add text columns
            dataGridView1.Columns.Add("fid", "Fee ID");
            dataGridView1.Columns.Add("roomtype", "Room Type");
            dataGridView1.Columns.Add("room_fee", "Room Fee");
            


            // Add rows
            foreach (DataRow row in dt.Rows)
            {
                object[] rowData = new object[3];
                rowData[0] = row["fid"];
                rowData[1] = row["roomtype"];
                rowData[2] = row["room_fee"];
               
                dataGridView1.Rows.Add(rowData);
            }


        }
        //end

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            visitor vi = new visitor();
            vi.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //fee update အတွက်
        private void updatebtn_Click(object sender, EventArgs e)
        {

            int roomfee;
            if (!int.TryParse(roomfeetxt.Text, out roomfee))
            {
                MessageBox.Show("ဂဏန်းသာထည့်ပါ", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            int roomTypeID = Convert.ToInt32(roomcom.SelectedValue);
            string roomType = ((DataRowView)roomcom.SelectedItem)["roomtype"].ToString();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "update fee set rtid=@rtid ,room_fee=@room_fee ,roomtype=@roomtype where roomtype=@roomtype";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@room_fee", roomfee);
                cmd.Parameters.AddWithValue("@rtid", roomTypeID);
                cmd.Parameters.AddWithValue("@roomtype", roomtype);
              
                cmd.ExecuteNonQuery();
                MessageBox.Show("update Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }//end

        private void button1_Click(object sender, EventArgs e)
        {
            // Prompt for student name
            string inputName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter School :",
                "ADD School",
                "");

            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("School required");
                return;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "insert into School(SchoolName)values (@SchoolName)";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@SchoolName", inputName);


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

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
    
