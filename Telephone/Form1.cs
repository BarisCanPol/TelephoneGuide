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
using Telephone.DAL.ORM.Context;
using Telephone.DAL.ORM.Entity;

namespace Telephone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProjectContext db = new ProjectContext();

        public void TextEraser()  //Eraser part 
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is MaskedTextBox)
                {
                    item.Text = "";
                }

            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is MaskedTextBox)
                {
                    item.Text = "";
                }

            }
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }

            }
            foreach (Control item in groupBox4.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
              
            }
        }
      
        public void TakeList()  // linq to entity to show datas
        {
            dataGridView1.DataSource = db.AppUsers.Where(x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Update).ToList();
            dataGridView1.Refresh();
            
        }

        public void AddUser()   //adding new 
        {
            AppUser user = new AppUser();
            user.FirstName = txtAddFirstName.Text;
            user.LastName = txtAddLastName.Text;
            user.Phone = mkdAddPhone.Text;
            db.AppUsers.Add(user);           
            db.SaveChanges();
            TakeList();
            TextEraser();
            

        }
        int id;
        public void UpdateUser()   //update part
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            AppUser user = db.AppUsers.Where(x => x.ID == id).FirstOrDefault();
            user.FirstName = txtUpdateFirstName.Text;
            user.LastName = txtUpdateLastName.Text;
            user.Phone = mkdUpdatePhone.Text;
            db.SaveChanges();
            TakeList();            
            TextEraser();

        }
        public void DeleteUser()

        {
            int id1 = Convert.ToInt32(txtDeleteID.Text);
            AppUser user = db.AppUsers.Where(x => x.ID == id1).FirstOrDefault();
            user.Status = DAL.ORM.Enum.Status.Delete;               
            db.SaveChanges();
            TakeList();
            TextEraser();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TakeList();
            
            // text change part
            var source = new AutoCompleteStringCollection();   
            source.AddRange(  db.AppUsers.Select(x=> x.FirstName).ToArray());

            txtFindFullName.AutoCompleteCustomSource = source;
                txtFindFullName.AutoCompleteMode =
                       AutoCompleteMode.SuggestAppend;
                txtFindFullName.AutoCompleteSource =
                       AutoCompleteSource.CustomSource;
            Visible = true;
        
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUser();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUpdateFirstName.Text = dataGridView1.CurrentRow.Cells["FirstName"].Value.ToString();
            txtUpdateLastName.Text = dataGridView1.CurrentRow.Cells["LastName"].Value.ToString();
            mkdUpdatePhone.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
            txtDeleteID.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateUser();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string findin = txtFindFullName.Text;
            dataGridView1.DataSource = db.AppUsers.Where(x => x.FirstName == findin).ToList();
        }


        


    }
}
