using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace phoneDirectory
{
    internal class db
    {
        public string name;
        public string surname;
        public string phoneNo;
        public string id;

        public string email;
        public string adress;

        SQLiteConnection conn = new SQLiteConnection("Data Source=phoneDirection.db; Version=3");
        SQLiteCommand cmd;
        SQLiteDataReader dr;

        public void addContact(string mTb)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd = new SQLiteCommand("select * from contacts",conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToString(dr["number"]) == mTb)
                {
                    MessageBox.Show("this number is registered to a person named " + dr["name"] + " " + dr["surname"]);
                    dr.Close();
                    conn.Close();
                    return;
                }
            }
            dr.Close();

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@phoneno", phoneNo);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@adress", adress);


            cmd.CommandText = ("INSERT INTO contacts(name,surname,number) VALUES(@name,@surname,@phoneno)");
            cmd.ExecuteNonQuery();
            cmd.CommandText = ("select * from contacts where number like @phoneno");
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = dr["id"].ToString();
            }
            dr.Close();
            cmd.Parameters.AddWithValue("@id", id);

            cmd.CommandText = ("INSERT INTO contacts_detail(contactId,email,adress) VALUES(@id, @email, @adress)");
            cmd.ExecuteNonQuery();

            conn.Close();
            MessageBox.Show("Registration done");
        }

        public void deleteContact()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.Connection= conn;
            cmd.Parameters.AddWithValue("@id", id);

            cmd.CommandText =("delete from contacts where id=@id");
            cmd.ExecuteNonQuery();

            cmd.CommandText = ("select id from contacts_detail where contactId like @id");

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = Convert.ToString(dr["id"]);
            }
            dr.Close();
            MessageBox.Show(id);
            cmd.CommandText=("delete from contacts_detail where id="+id);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Deletion has taken place");
        }

        public void updatecontact()
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }            

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@phoneno", phoneNo);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@adress", adress);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.CommandText = ("update contacts set name=@name, surname=@surname, number=@phoneno where id=@id");
            cmd.ExecuteNonQuery();

            cmd.CommandText = ("select id from contacts_detail where contactId like @id");

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = Convert.ToString(dr["id"]);
            }

            dr.Close();
            cmd.CommandText = ("update contacts_detail set email=@email, adress=@adress where id=@id");
            cmd.ExecuteNonQuery();

            MessageBox.Show("Update has taken place");
        }

        public DataTable showdata(string data)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            cmd = new SQLiteCommand(data, conn);
            dr = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dr);
            return table;
            
        }

    }
}
