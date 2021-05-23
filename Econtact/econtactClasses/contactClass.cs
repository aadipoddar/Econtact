using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        //Getter Setter Properties
        //Acts as Data Carrier in our Application
        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;


        //Selecting Data from Database
        public DataTable Select()
        {
            //Step 1 : Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //Step 2 : Writing SQL Querry
                string sql = "SELECT * FROM tbl_contact";

                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creating  SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }

            catch(Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }

            return dt;
        }


        //Inserting Data into Database
        public bool Insert(contactClass c)
        {
            //Creating Default return type  and setting its value to false
            bool isSuccess = false;

            //Step 1 :   connect Database 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Step 2 : Create a SQL Querry to insert Data
                string sql = "INSERT INTO tbl_contact(FirstName ,  LastName , ContactNo , Address , Gender) VALUES (@FirstName ,  @LastName , @ContactNo , @Address , @Gender)";

                //Creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create Parameter to add data 
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                // connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //if the querry runs successfully then the values of rows will be greater than zero else value will be 0
                if(rows>0)
                {
                    isSuccess = true;
                }

                else
                {
                    isSuccess = false;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }

            return isSuccess;
        }


        //method to update data in database from our application
        public bool Update(contactClass c)
        {
            //create a default return type and set its default value to false 
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            
            try
            {
                //sql to update data in our data base
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName , LastName=@LastName , ContactNo=@ContactNo , Address=@Address , Gender=@Gender WHERE ContactID=@ContactID";

                //creating sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Genders", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);

                //open Database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the querry runs successfully then the value of rows will be greater than zero else its value will be zero 
                if(rows>0)
                {
                    isSuccess = true;
                }

                else
                {
                    isSuccess = false;
                }
            }

            catch(Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }

            return isSuccess;
        }


        //method to delete data from database
        public bool Delete(contactClass c)
        {
            //create a default returnvalue and set its value to false
            bool isSuccess = false;

            // create sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //create sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //open connection 
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the querry runs successfully then the value of rows will be greater than zero else its value will be zero
                if(rows>0)
                {
                    isSuccess = true;
                }

                else
                {
                    isSuccess = false;
                }
                
            }

            catch (Exception ex)
            {

            }

            finally
            {
                //close connnection
                conn.Close();
            }

            return isSuccess;
        }
    }
}
