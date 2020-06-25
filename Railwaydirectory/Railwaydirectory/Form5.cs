using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Railwaydirectory
{
    public partial class Form5 : Form
    {
        Form1 parent;
        public Form5(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.parent.dataGridView1.Rows.Count - 1; i++)
            {
                string s = "";
                for (int j = 0; j < this.parent.dataGridView1.Columns.Count - 1; j++)
                {
                    string locals = this.parent.dataGridView1.Rows[i].Cells[j].Value.ToString();
                    string[] ars = locals.Split(' ');
                    locals = "";
                    int l = 0;
                    while ((l < ars.Length) && (ars[l] != ""))
                    {
                        if (locals != "")
                            locals += ' ';
                        locals += ars[l];
                        l++;
                    }
                    s += locals;
                    if (j < this.parent.dataGridView1.Columns.Count - 2)
                        s += ", ";
                }
                comboBox1.Items.Add(s);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            string[] sp;
            sp = s.Split(',');
            for (int i = 1; i < sp.Length; i++)
                sp[i] = sp[i].Remove(0, 1);
            textBox1.ReadOnly = false;
            textBox1.Text = sp[0];
            textBox2.ReadOnly = false;
            textBox2.Text = sp[1];
            textBox3.ReadOnly = false;
            textBox3.Text = sp[2];
            textBox4.ReadOnly = false;
            textBox4.Text = sp[3];
            textBox5.ReadOnly = false;
            textBox5.Text = sp[4];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.tr.City = textBox1.Text;
            parent.tr.Day = textBox2.Text;
            parent.tr.Dept = textBox3.Text;
            parent.tr.Arrt = textBox4.Text;
            parent.tr.Wagt = textBox5.Text;
            parent.selectnumber = comboBox1.SelectedIndex;
            this.DialogResult = DialogResult.OK;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
