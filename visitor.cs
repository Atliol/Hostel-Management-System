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
using System.Xml.Linq;

namespace Hostel_Management_Syatem
{
    public partial class visitor : Form
    {
        public visitor()
        {
            InitializeComponent();
            
        }

        private void visitor_Load(object sender, EventArgs e)
        {
            BindGrid();
            MakePanelRound(panel2, 20);
            LoadStudentType();
            MakePanelRound(panel6, 24);
        }
        //datagridview 
        private void BindGrid()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            SqlCommand cmd = new SqlCommand("SELECT vname,sname,Pupose,checkindate,checkoutdate FROM visitor ", conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            dataGridView1.Columns.Clear();

            // Add text columns
            dataGridView1.Columns.Add("vname", "Visitor Name");
            dataGridView1.Columns.Add("sname", "Student Name");
            dataGridView1.Columns.Add("Pupose", "Pupose");
            dataGridView1.Columns.Add("checkindate", "Checkindate ");
            dataGridView1.Columns.Add("checkoutdate", "Checkoutdate ");
          


            // Add rows
            foreach (DataRow row in dt.Rows)
            {
                object[] rowData = new object[6];
                rowData[0] = row["vname"];
                rowData[1] = row["sname"];
                rowData[2] = row["Pupose"];
                rowData[3] = row["checkindate"];
                rowData[4] = row["checkoutdate"];
                dataGridView1.Rows.Add(rowData);
            }

           
        }
        //end
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

        private void home_Click(object sender, EventArgs e)
        {
            Mainform ma = new Mainform();
            ma.Show();
            this.Close();
        }

        private void room_Click(object sender, EventArgs e)
        {
            room rm = new room();
            rm.Show();
            this.Close();
        }

        private void student_Click(object sender, EventArgs e)
        {
            student st = new student();
            st.Show();
            this.Close();
        }

        private void fee_Click(object sender, EventArgs e)
        {
            fee fe = new fee();
            fe.Show();
            this.Close();
        }

        private void about_Click(object sender, EventArgs e)
        {
            about ab = new about();
            ab.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
        //loadstudenttypeအတွက်
        private void LoadStudentType()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter da = new SqlDataAdapter("SELECT sid,sname FROM Student", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            stucom.DataSource = dt;
            stucom.DisplayMember = "sname";
            stucom.ValueMember = "sid";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Check if visitor name is empty
            if (string.IsNullOrWhiteSpace(visitornametxt.Text))
            {
                MessageBox.Show("Visitor နာမည်ထည့်ပေးပါ.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int sidID = Convert.ToInt32(stucom.SelectedValue);
            string stuname = ((DataRowView)stucom.SelectedItem)["sname"].ToString();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "insert into visitor(sid,vname,sname,Pupose,checkindate,checkoutdate)values (@sid,@vname,@sname,@Pupose,@checkindate,@checkoutdate)";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@vname", visitornametxt.Text);
                cmd.Parameters.AddWithValue("@sid", sidID);
                cmd.Parameters.AddWithValue("@sname", stuname);
                cmd.Parameters.AddWithValue("@Pupose", puposetxt.Text);
                cmd.Parameters.AddWithValue("@checkindate", checkindate.Value);
                cmd.Parameters.AddWithValue("@checkoutdate", checkoutdate.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successful!");
                puposetxt.Text = "";
                visitornametxt.Text = "";
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
        //visitor update အတွက်
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if visitor name is empty
            if (string.IsNullOrWhiteSpace(visitornametxt.Text))
            {
                MessageBox.Show("Visitor နာမည်ထည့်ပေးပါ.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            int sidID = Convert.ToInt32(stucom.SelectedValue);
            string stuname = ((DataRowView)stucom.SelectedItem)["sname"].ToString();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");

            string query = "update visitor set vname=@vname ,sid=@sid ,sname=@sname,Pupose=@Pupose ,checkindate=@checkindate , checkoutdate=@checkoutdate where vname=@vname";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@vname", visitornametxt.Text);
                cmd.Parameters.AddWithValue("@sid", sidID);
                cmd.Parameters.AddWithValue("@sname", stuname);
                cmd.Parameters.AddWithValue("@Pupose", puposetxt.Text);
                cmd.Parameters.AddWithValue("@checkindate", checkindate.Value);
                cmd.Parameters.AddWithValue("@checkoutdate", checkoutdate.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("update Successful!");
                puposetxt.Text = "";
                visitornametxt.Text = "";
                
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


        } //visitor update အဆုံး

        //visitor delete
        private void deletebtn_Click(object sender, EventArgs e)
        {

            // Prompt for student name
            string inputName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the Visitor name to delete:",
                "Delete Visitor",
                "");

            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("Visitor name is required to delete.");
                return;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Redmibook 16\Documents\thazinnwehostal.mdf"";Integrated Security=True;Connect Timeout=30");
            string query = "DELETE FROM visitor WHERE vname = @vname";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@vname", inputName);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Delete Successful!");
                    BindGrid(); // Refresh grid
                }
                else
                {
                    MessageBox.Show("No visitor found with that name.");
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

        private void Logout_Click(object sender, EventArgs e)
        {
            logout lou = new logout();
            lou.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure it's not header row
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Example: Fill student name textbox
                visitornametxt.Text = row.Cells["vname"].Value.ToString();

                
                // Other fields (if needed)
                puposetxt.Text = row.Cells["Pupose"].Value.ToString();
                stucom.Text = row.Cells["sname"].Value.ToString();

            }
        }
    }
}
