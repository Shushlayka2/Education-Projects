using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrainClass;

namespace Railwaydirectory
{
    public partial class Form2 : Form
    {
        Form1 parent;
        public Form2(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Train newtrain= new Train();
            newtrain.City = textBox1.Text;
            newtrain.Day = textBox2.Text;
            newtrain.Dept = textBox3.Text;
            newtrain.Arrt = textBox4.Text;
            newtrain.Wagt = textBox5.Text;
            this.parent.tr = newtrain;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
