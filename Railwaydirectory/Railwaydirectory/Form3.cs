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
    public partial class Form3 : Form
    {
        Form1 parent;
        public Form3(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Form3_Load(object sender, EventArgs e) // заполняем combobox строками из таблицы
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

        private void button1_Click(object sender, EventArgs e) // передаем "отцу" номер выбронной строки
        {
            parent.selectnumber = comboBox1.SelectedIndex;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
