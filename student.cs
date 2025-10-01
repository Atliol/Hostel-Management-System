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
    public partial class student : Form
    {
        public student()
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

        private DataTable roomInfoTable;

        private void LoadRoomType()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            // Load all room info
            SqlDataAdapter da = new SqlDataAdapter("SELECT rid, room_no, rtid, roomtype,capacity FROM room", conn);
            roomInfoTable = new DataTable();
            da.Fill(roomInfoTable);

            // Bind room numbers
            roomnocom.DataSource = roomInfoTable.DefaultView;
            roomnocom.DisplayMember = "room_no";
            roomnocom.ValueMember = "rid";

            // Bind room types (initially all, will filter later)
            roomcom.DataSource = roomInfoTable.DefaultView;
            roomcom.DisplayMember = "roomtype";
            roomcom.ValueMember = "rtid";

            // School ComboBox (unchanged)
            SqlDataAdapter das = new SqlDataAdapter("SELECT schid,SchoolName FROM School", conn);
            DataTable dts = new DataTable();
            das.Fill(dts);
            schoolcom.DataSource = dts;
            schoolcom.DisplayMember = "SchoolName";
            schoolcom.ValueMember = "schid";
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void student_Load(object sender, EventArgs e)
        {
            LoadRoomType();
            BindGrid();
            MakePanelRound(panel2, 20);
         
            MakePanelRound(panel6, 24);

            if (studentPictureBox.Image != null)
            {
                byte[] photoBytes = ImageToByteArray(studentPictureBox.Image);
                // You can now use photoBytes as needed
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Mainform mainform = new Mainform();
            mainform.Show();
            this.Hide();
        }

        private void room_Click(object sender, EventArgs e)
        {
            room room = new room();
            room.Show();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        //student insert အတွက်
        private void button2_Click(object sender, EventArgs e)
        {

            int roomTypeID = Convert.ToInt32(roomcom.SelectedValue);
            string roomType = ((DataRowView)roomcom.SelectedItem)["roomtype"].ToString();
            int roomnoID = Convert.ToInt32(roomnocom.SelectedValue);
            string roomno = ((DataRowView)roomnocom.SelectedItem)["room_no"].ToString();
            int schoolID = Convert.ToInt32(schoolcom.SelectedValue);
            string schoolname = ((DataRowView)schoolcom.SelectedItem)["SchoolName"].ToString();

            // Get room capacity
            int roomCapacity = 1;
            DataRow[] roomRows = roomInfoTable.Select($"rid = {roomnoID}");
            if (roomRows.Length > 0)
            {
                roomCapacity = Convert.ToInt32(roomRows[0]["capacity"]);
            }

            // Count current students in the room
            int currentStudentCount = 0;
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string countQuery = "SELECT COUNT(*) FROM Student WHERE rid = @rid";
                using (SqlCommand countCmd = new SqlCommand(countQuery, con))
                {
                    countCmd.Parameters.AddWithValue("@rid", roomnoID);
                    con.Open();
                    currentStudentCount = (int)countCmd.ExecuteScalar();
                }
            }

            // Check capacity
            if (currentStudentCount >= roomCapacity)
            {
                MessageBox.Show("ယခုအခန်းတွင် ကျောင်းသားပြည့်နေပါပြီ, နောက်တစ်ခန်းကိုရွေးပေးပါ");
                return;
            }

            byte[] photoBytes = null;
            if (studentPictureBox.Image != null)
            {
                photoBytes = ImageToByteArray(studentPictureBox.Image);
            }


            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "insert into Student(rtid,sname,school,room_no,remark,schid,NRC,class,rid,roomtype,photo)values (@rtid,@sname,@school,@room_no,@remark,@schid,@NRC,@class,@rid,@roomtype,@photo)";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                
                cmd.Parameters.AddWithValue("@room_no", roomno);
                cmd.Parameters.AddWithValue("@rid", roomnoID);
                cmd.Parameters.AddWithValue("@rtid", roomTypeID);
                cmd.Parameters.AddWithValue("@roomtype", roomType);
                cmd.Parameters.AddWithValue("@sname", studentnametxt.Text);
                cmd.Parameters.AddWithValue("@schid", schoolID);
                cmd.Parameters.AddWithValue("@school", schoolname);
                cmd.Parameters.AddWithValue("@remark", remarktxt.Text);
                cmd.Parameters.AddWithValue("@NRC", nrctxt.Text);
                cmd.Parameters.AddWithValue("@class", classtxt.Text);
                cmd.Parameters.AddWithValue("@photo", (object)photoBytes ?? DBNull.Value);



                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successful!");
                studentnametxt.Text = "";
                classtxt.Text = "";
                remarktxt.Text = "";
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
        //insert အဆုံး
        private void label6_Click(object sender, EventArgs e)
        {
            visitor vi = new visitor();
            vi.Show();
            this.Hide();
        }
        //data view
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure it's not header row
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                studentnametxt.Text = row.Cells["sname"].Value.ToString();
                classtxt.Text = row.Cells["class"].Value.ToString();
                remarktxt.Text = row.Cells["remark"].Value.ToString();
                nrctxt.Text = row.Cells["NRC"].Value.ToString();
                schoolcom.Text = row.Cells["school"].Value.ToString();
                roomcom.Text = row.Cells["roomtype"].Value.ToString();
                roomnocom.Text = row.Cells["room_no"].Value.ToString();

                // Set image directly from cell value
                var cellValue = row.Cells["photo"].Value;
                if (cellValue != null && cellValue is Image)
                {
                    studentPictureBox.Image = (Image)cellValue;
                }
                else
                {
                    studentPictureBox.Image = null;
                }
            }
        }
        //end
        private void BindGrid()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("SELECT sname,class,school,room_no,NRC,roomtype,remark,photo FROM Student", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.Columns.Clear();

            // Add text columns
            dataGridView1.Columns.Add("sname", "Name");
            dataGridView1.Columns.Add("class", "Class");
            dataGridView1.Columns.Add("school", "School");
            dataGridView1.Columns.Add("room_no", "Room No");
            dataGridView1.Columns.Add("NRC", "NRC NO");
            dataGridView1.Columns.Add("roomtype", "Room Type");
            dataGridView1.Columns.Add("remark", "Ph No");

            //Add image column
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "photo";
            imgCol.HeaderText = "Photo";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns.Add(imgCol);
            

            // Add rows
            foreach (DataRow row in dt.Rows)
            {
                object[] rowData = new object[8];
                rowData[0] = row["sname"];
                rowData[1] = row["class"];
                rowData[2] = row["school"];
                rowData[3] = row["room_no"];
                rowData[4] = row["NRC"];
                rowData[5] = row["roomtype"];
                rowData[6] = row["remark"];
                // Convert photo bytes to Image
                if (row["photo"] != DBNull.Value && row["photo"] is byte[] imgBytes && imgBytes.Length > 0)
                {
                    try
                    {
                        using (var ms = new System.IO.MemoryStream(imgBytes))
                        {
                            rowData[7] = Image.FromStream(ms);
                        }
                    }
                    catch
                    {
                        rowData[7] = null; // or a default image
                    }
                }
                else
                {
                    rowData[7] = null;
                }
                
                dataGridView1.Rows.Add(rowData);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //Browse and Load Photo
        private void imagebtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    studentPictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
        }
        //end
        //Add a helper method: 
        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void roomcom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roomnocom.SelectedValue == null) return;

            // Get selected room's rtid
            DataRowView selectedRoom = roomnocom.SelectedItem as DataRowView;
            if (selectedRoom == null) return;

            int selectedRtid = Convert.ToInt32(selectedRoom["rtid"]);

            // Filter roomcom to only show the matching roomtype
            DataView dv = new DataView(roomInfoTable);
            dv.RowFilter = $"rtid = {selectedRtid}";
            roomcom.DataSource = dv;
            roomcom.DisplayMember = "roomtype";
            roomcom.ValueMember = "rtid";
        }

        //student update အတွက်
        private void button1_Click(object sender, EventArgs e)
        {

            int roomTypeID = Convert.ToInt32(roomcom.SelectedValue);
            string roomType = ((DataRowView)roomcom.SelectedItem)["roomtype"].ToString();
            int roomnoID = Convert.ToInt32(roomnocom.SelectedValue);
            string roomno = ((DataRowView)roomnocom.SelectedItem)["room_no"].ToString();
            int schoolID = Convert.ToInt32(schoolcom.SelectedValue);
            string schoolname = ((DataRowView)schoolcom.SelectedItem)["SchoolName"].ToString();

            // Get room capacity
            int roomCapacity = 1;
            DataRow[] roomRows = roomInfoTable.Select($"rid = {roomnoID}");
            if (roomRows.Length > 0)
            {
                roomCapacity = Convert.ToInt32(roomRows[0]["capacity"]);
            }

            // Count current students in the room
            int currentStudentCount = 0;
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30"))
            {
                string countQuery = "SELECT COUNT(*) FROM Student WHERE rid = @rid";
                using (SqlCommand countCmd = new SqlCommand(countQuery, con))
                {
                    countCmd.Parameters.AddWithValue("@rid", roomnoID);
                    con.Open();
                    currentStudentCount = (int)countCmd.ExecuteScalar();
                }
            }

            // Check capacity
            if (currentStudentCount >= roomCapacity)
            {
                MessageBox.Show("ယခုအခန်းတွင် ကျောင်းသားပြည့်နေပါပြီ, နောက်တစ်ခန်းကိုရွေးပေးပါ");
                return;
            }

            byte[] photoBytes = null;
            if (studentPictureBox.Image != null)
            {
                photoBytes = ImageToByteArray(studentPictureBox.Image);
            }


            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = " update Student set rtid = @rtid ,room_no = @room_no ,rid = @rid, roomtype = @roomtype , sname = @sname ,schid = @schid ,school = @school, remark = @remark ,NRC = @NRC ,class = @class, photo = @photo where sname = @sname ";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@room_no", roomno);
                cmd.Parameters.AddWithValue("@rid", roomnoID);
                cmd.Parameters.AddWithValue("@rtid", roomTypeID);
                cmd.Parameters.AddWithValue("@roomtype", roomType);
                cmd.Parameters.AddWithValue("@sname", studentnametxt.Text);
                cmd.Parameters.AddWithValue("@schid", schoolID);
                cmd.Parameters.AddWithValue("@school", schoolname);
                cmd.Parameters.AddWithValue("@remark", remarktxt.Text);
                cmd.Parameters.AddWithValue("@NRC", nrctxt.Text);
                cmd.Parameters.AddWithValue("@class", classtxt.Text);
                cmd.Parameters.AddWithValue("@photo", (object)photoBytes ?? DBNull.Value);



                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successful!");
                studentnametxt.Text = "";
                classtxt.Text = "";
                remarktxt.Text = "";
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
        //end

        //student delete အတွက်

        private void deletebtn_Click(object sender, EventArgs e)
        {
           
        
            // Prompt for student name
            string inputName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the student name to delete:",
                "Delete Student",
                "");

            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("Student name is required to delete.");
                return;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");
            string query = "DELETE FROM Student WHERE sname = @sname";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@sname", inputName);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Delete Successful!");
                    BindGrid(); // Refresh grid
                }
                else
                {
                    MessageBox.Show("No student found with that name.");
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
        //end
    }
}
