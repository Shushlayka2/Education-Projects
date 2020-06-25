using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrainClass;
using Npgsql;

namespace Railwaydirectory
{
    public partial class Form4 : Form
    {
        Train tr;
        NpgsqlConnection con;
        public Form4(Train tr, NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            this.tr = tr;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string comstring = "select * from railwaysystem.\"Trains\"";
            if ((tr.City != "") || (tr.Day != "") || (tr.Dept != "") || (tr.Arrt != "") || (tr.Wagt != ""))
            {
                comstring += " where";
                if (tr.City != "")
                    comstring += " \"City\" = '" + tr.City + "' and ";
                if (tr.Day != "")
                    comstring += " \"Day\" = '" + tr.Day + "' and ";
                if (tr.Dept != "")
                    comstring += " \"Dept\" = '" + tr.Dept + "' and ";
                if (tr.Arrt != "")
                    comstring += " \"Arrt\" = '" + tr.Arrt + "' and ";
                if (tr.Wagt != "")
                    comstring += " \"Wagt\" = '" + tr.Wagt + "' and ";
                comstring = comstring.Remove(comstring.Length - 5, 5);
                NpgsqlCommand com = new NpgsqlCommand(comstring, con);
                DataTable dt = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
                dataGridView1.Columns[5].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

    }
}
