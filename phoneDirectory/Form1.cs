using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.OleDb;
using System.IO;

namespace phoneDirectory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SQLiteConnection conn = new SQLiteConnection("Data Source=phoneDirection.db; Version=3");
        db db = new db();
        
        public void clearTextbox()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            maskedTextBox1.Clear();
            deleteFindTb.Clear();
            deleteIDTb.Clear();
            updateIdTb.Clear();
            updateNmbrMtb.Clear();
            updateNmTb.Clear();
            updateSearchTb.Clear();
            updateSnTb.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.name = textBox1.Text;
            db.surname = textBox2.Text;
            db.phoneNo = maskedTextBox1.Text;
            db.email = textBox4.Text;
            db.adress = textBox5.Text;
            db.addContact(maskedTextBox1.Text);
            dataGridView1.DataSource = db.showdata("select * from contacts");
            clearTextbox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.showdata("select * from contacts");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.id = deleteIDTb.Text;
            db.deleteContact();
            dataGridView1.DataSource = db.showdata("select * from contacts");
            clearTextbox();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                MessageBox.Show("Enter the ID of the person to be deleted for deletion");
            }

            else if (tabControl1.SelectedIndex == 2)
            {
                MessageBox.Show("To update, enter the ID of the person to update and enter the information to update");
            }
            clearTextbox();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.showdata("select * from contacts where name like" + "'" + "%" + textBox3.Text  + "%" + "'");
        }

        private void deleteFindTb_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.showdata("select * from contacts where name like" + "'" + "%" + deleteFindTb.Text + "%" + "'");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.showdata("select * from contacts where name like" + "'" + "%" + updateSearchTb.Text + "%" + "'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (updateIdTb.Text == "")
            {
                MessageBox.Show("Please fill in the blanks");
                return;
            }

            db.name = updateNmTb.Text;
            db.surname = updateSnTb.Text;
            db.phoneNo = updateNmbrMtb.Text;
            db.email = textBox6.Text;
            db.adress = textBox7.Text;

            db.updatecontact();

            dataGridView1.DataSource = db.showdata("select * from contacts");
            clearTextbox();

        }
    }
}
