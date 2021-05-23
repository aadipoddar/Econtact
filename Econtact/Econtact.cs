using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        contactClass c = new contactClass();

        private void button2_Click(object sender, EventArgs e)
        {
            //get data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;         
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtAddress.Text;
            c.Gender = cmbGender.Text;

            //update data in data base
            bool success = c.Update(c);
            if(success == true)
            {
                //update Successfully
                MessageBox.Show("Contact has been successfully updated.");

                //load data on data griedview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                //call clear method 
                Clear();
            }

            else
            {
                //Failed to update
                MessageBox.Show("Failed to Update Contact.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the contact ID from the application
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);

            bool success = c.Delete(c);

            if(success == true)
            {
                //Successfully Deleted
                MessageBox.Show("Contact Successfully Deleted.");

                //refresh data griedview
                //load data on data griedview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                //call clear method here
                Clear();
            }

            else
            {
                //failed to delete
                MessageBox.Show("Failed to delete Contact. Try Again");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the value from input fields  
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtAddress.Text;
            c.Gender = cmbGender.Text;

            //insert data into database using method we created 
            bool success = c.Insert(c);
            if(success == true)
            {
                //successfully inserted 
                MessageBox.Show("New Contact Successfully Inserted");

                //call the clear method here
                Clear();
            }

            else
            {
                //failed to add Contact
                MessageBox.Show("failed to add New Contact . Try Again.");
            }

            //load data on data griedview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            //load data on data griedview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //method to clear fields
        public void Clear ()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtAddress.Text = "";
            txtBoxContactNumber.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get data from data gridview and load it to the textboxes respectively
            //identify the row on which mouse is clicked

            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call clear method here 
            Clear();
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private void txtBoxSeacrh_TextChanged(object sender, EventArgs e)
        {
            //get the value form text box 
            string keyword = txtBoxSeacrh.Text;


            SqlConnection conn = new SqlConnection();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            dgvContactList.DataSource = dt;
        }
    }
}
