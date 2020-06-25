using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using TrainClass;

namespace Railwaydirectory
{
    public partial class Form1 : Form
    {
        public NpgsqlConnection con;
        public Form1()
        {
            InitializeComponent();
            string ConnectionString = "Server=localhost;Port=5432;User=postgres;Password=asehan57;Database=postgres;";
            con = new NpgsqlConnection(ConnectionString);
            con.Open();
        }

        private int amount; // кол-во строк в таблице

        public void RefreshTable()
        {
            DataTable newdt = new DataTable();
            string com = "select *  from RailwaySystem.\"Trains\"";
            NpgsqlCommand sqlt = new NpgsqlCommand(com, con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqlt);
            da.Fill(newdt);
            dataGridView1.DataSource = newdt;
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshTable();
            amount = dataGridView1.Rows.Count;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[0].HeaderText = "Город";
            dataGridView1.Columns[1].HeaderText = "День недели";
            dataGridView1.Columns[2].HeaderText = "Время отправления";
            dataGridView1.Columns[3].HeaderText = "Время прибытия";
            dataGridView1.Columns[4].HeaderText = "Тип поезда";
        }

        public Train tr; 

        private void button1_Click(object sender, EventArgs e) // Кнопка Добавить
        {
            tr = new Train();
            Form2 newform = new Form2(this);
            newform.ShowDialog();
            if (newform.DialogResult == DialogResult.OK)
            {

                string comstring = "insert into RailwaySystem.\"Trains\" values ('" + tr.City + "','" + tr.Day + "','" + tr.Dept + "','" + tr.Arrt + "','" + tr.Wagt + "','"+amount+"')";
                NpgsqlCommand com = new NpgsqlCommand(comstring, con);
                com.ExecuteNonQuery();
                amount++;
                RefreshTable();
            }
        }

        public int selectnumber;
        private void button2_Click(object sender, EventArgs e) // кнопка Удалить
        {
            selectnumber = -1;
            Form3 newform = new Form3(this);
            newform.ShowDialog();
            if (newform.DialogResult == DialogResult.OK)
            {
                selectnumber++;
                string comstring1 = "delete from RailwaySystem.\"Trains\" where \"ID\" = "+selectnumber.ToString()+"";
                string comstring2 = "update railwaysystem.\"Trains\" set \"ID\" = \"ID\" - 1 where \"ID\" > "+selectnumber.ToString()+"";
                NpgsqlCommand com1 = new NpgsqlCommand(comstring1, con);
                NpgsqlCommand com2 = new NpgsqlCommand(comstring2, con);
                com1.ExecuteNonQuery();
                com2.ExecuteNonQuery();
                amount--;
                RefreshTable();
            }
        }

        private void button5_Click(object sender, EventArgs e) // Изменить
        {
            tr = new Train();
            selectnumber = -1;
            Form5 newfortm5 = new Form5(this);
            newfortm5.ShowDialog();
            selectnumber++;
            string comstring = "update railwaysystem.\"Trains\" set \"City\" = '" + tr.City + "', \"Day\" = '" + tr.Day + "', \"Dept\" = '" + tr.Dept + "', \"Arrt\" = '" + tr.Arrt + "', \"Wagt\" = '" + tr.Wagt + "' where \"ID\" = " + selectnumber.ToString() + "";
            NpgsqlCommand com = new NpgsqlCommand(comstring, con);
            com.ExecuteNonQuery();
            RefreshTable();
        }

        private void button3_Click(object sender, EventArgs e) // Запрос  
        {
            tr = new Train();
            Form2 newform2 = new Form2(this);
            newform2.Text = "Запрос";
            newform2.button1.Text = "Запросить";
            newform2.ShowDialog();
            if (newform2.DialogResult == DialogResult.OK)
            {
                Form4 newform4 = new Form4(tr, con);
                newform4.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e) // Выход
        {
            con.Close();
            this.Close();
        }

    }
}
