using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Information_directory
{
    public partial class Form1 : Form
    {
        bool opened1 = false;
        bool opened2 = false;
        bool opened3 = false;
        bool opened4 = false;
        Form2 newform = new Form2();
        string s1, s2, s3, s4, s5, s6;

        public Form1()
        {
            InitializeComponent();
            newform.Show();
            newform.Size = new Size(newform.Size.Width, 0);
            newform.Visible = false;
        }

        private void Close_panels()
        {
            opened1 = false;
            opened2 = false;
            opened3 = false;
            opened4 = false;
            if (panel1.Height != 10)
            {
                while (panel1.Size.Height > 10)
                {
                    Thread.Sleep(1);
                    panel1.Size = new Size(panel1.Size.Width, panel1.Size.Height - 10);
                    this.Size = new Size(this.Size.Width, this.Size.Height - 10);
                    this.Refresh();
                }
                panel1.Height = 10;
                panel1.Visible = false;
                this.Height = 380;
            }
            if (panel2.Height != 10)
            {
                while (panel2.Size.Height > 10)
                {
                    Thread.Sleep(1);
                    panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height - 10);
                    this.Size = new Size(this.Size.Width, this.Size.Height - 10);
                    this.Refresh();
                }
                panel2.Height = 10;
                panel2.Visible = false;
                this.Height = 380;
            }
            if (panel3.Height != 10)
            {
                while (panel3.Size.Height > 10)
                {
                    Thread.Sleep(1);
                    panel3.Size = new Size(panel3.Size.Width, panel3.Size.Height - 10);
                    this.Size = new Size(this.Size.Width, this.Size.Height - 10);
                    this.Refresh();
                }
                panel3.Height = 10;
                panel3.Visible = false;
                this.Height = 380;
            }
            if (panel4.Height != 10)
            {
                while (panel4.Size.Height > 10)
                {
                    Thread.Sleep(1);
                    panel4.Size = new Size(panel4.Size.Width, panel4.Size.Height - 10);
                    this.Size = new Size(this.Size.Width, this.Size.Height - 10);
                    this.Refresh();
                }
                panel4.Height = 10;
                panel4.Visible = false;
                this.Height = 380;
            }
            if (Application.OpenForms.Count == 2)
            {
                while (newform.Size.Height > 3)
                {
                    Thread.Sleep(1);
                    newform.Size = new Size(newform.Size.Width, newform.Size.Height - 20);
                    newform.Refresh();
                }
                newform.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (!opened1)
            {
                Close_panels();
                label3.Anchor = AnchorStyles.Bottom;
                label4.Anchor = AnchorStyles.Bottom;
                label5.Anchor = AnchorStyles.Bottom;
                panel2.Anchor = AnchorStyles.Bottom;
                panel3.Anchor = AnchorStyles.Bottom;
                panel4.Anchor = AnchorStyles.Bottom;
                this.Height = this.Height + 150;
                panel1.Height = 150;
                panel1.Visible = true;
                opened1 = true;
                s1 = File.ReadAllText(@"..\..\Text\Sport\Running.txt", Encoding.Default);
                s2 = File.ReadAllText(@"..\..\Text\Sport\Tennis.txt", Encoding.Default);
                s3 = File.ReadAllText(@"..\..\Text\Sport\Futbol.txt", Encoding.Default);
                s4 = File.ReadAllText(@"..\..\Text\Sport\Voleyball.txt", Encoding.Default);
                s5 = File.ReadAllText(@"..\..\Text\Sport\Basketball.txt", Encoding.Default);
                s6 = File.ReadAllText(@"..\..\Text\Sport\Cycle.txt", Encoding.Default);
            }
            else
            {
                Close_panels();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (!opened2)
            {
                Close_panels();
                label3.Anchor = AnchorStyles.Top;
                label4.Anchor = AnchorStyles.Bottom;
                label5.Anchor = AnchorStyles.Bottom;
                panel2.Anchor = AnchorStyles.Top;
                panel3.Anchor = AnchorStyles.Bottom;
                panel4.Anchor = AnchorStyles.Bottom;
                this.Height = this.Height + 150;
                panel2.Height = 150;
                panel2.Visible = true;
                opened2 = true;
                s1 = File.ReadAllText(@"..\..\Text\Cars\Audi.txt", Encoding.Default);
                s2 = File.ReadAllText(@"..\..\Text\Cars\Bentley.txt", Encoding.Default);
                s3 = File.ReadAllText(@"..\..\Text\Cars\BMW.txt", Encoding.Default);
                s4 = File.ReadAllText(@"..\..\Text\Cars\Ferrari.txt", Encoding.Default);
                s5 = File.ReadAllText(@"..\..\Text\Cars\Maserati.txt", Encoding.Default);
                s6 = File.ReadAllText(@"..\..\Text\Cars\Porsche.txt", Encoding.Default);
            }
            else
            {
                Close_panels();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (!opened3)
            {
                Close_panels();
                label3.Anchor = AnchorStyles.Top;
                label4.Anchor = AnchorStyles.Top;
                label5.Anchor = AnchorStyles.Bottom;
                panel2.Anchor = AnchorStyles.Top;
                panel3.Anchor = AnchorStyles.Top;
                panel4.Anchor = AnchorStyles.Bottom;
                this.Height = this.Height + 150;
                panel3.Height = 150;
                panel3.Visible = true;
                opened3 = true;
                s1 = File.ReadAllText(@"..\..\Text\Animals\Bear.txt", Encoding.Default);
                s2 = File.ReadAllText(@"..\..\Text\Animals\Fox.txt", Encoding.Default);
                s3 = File.ReadAllText(@"..\..\Text\Animals\Kangaroo.txt", Encoding.Default);
                s4 = File.ReadAllText(@"..\..\Text\Animals\Lion.txt", Encoding.Default);
                s5 = File.ReadAllText(@"..\..\Text\Animals\Tiger.txt", Encoding.Default);
                s6 = File.ReadAllText(@"..\..\Text\Animals\Wolf.txt", Encoding.Default);
            }
            else
            {
                Close_panels();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (!opened4)
            {
                Close_panels();
                label3.Anchor = AnchorStyles.Top;
                label4.Anchor = AnchorStyles.Top;
                label5.Anchor = AnchorStyles.Top;
                panel2.Anchor = AnchorStyles.Top;
                panel3.Anchor = AnchorStyles.Top;
                panel4.Anchor = AnchorStyles.Top;
                this.Height = this.Height + 150;
                panel4.Height = 150;
                panel4.Visible = true;
                opened4 = true;
                s1 = File.ReadAllText(@"..\..\Text\Games\Assassins creed.txt", Encoding.Default);
                s2 = File.ReadAllText(@"..\..\Text\Games\Civilization.txt", Encoding.Default);
                s3 = File.ReadAllText(@"..\..\Text\Games\Counter strike.txt", Encoding.Default);
                s4 = File.ReadAllText(@"..\..\Text\Games\GTA.txt", Encoding.Default);
                s5 = File.ReadAllText(@"..\..\Text\Games\NFS.txt", Encoding.Default);
                s6 = File.ReadAllText(@"..\..\Text\Games\Witcher.txt", Encoding.Default);
            }
            else
            {
                Close_panels();
            }
        }

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = new Bitmap(@"../../Pictures/Спорт/running_passed.jpg");
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = null;
        }

        private void label10_MouseEnter(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = new Bitmap(@"../../Pictures/Спорт/tennis_passed.jpg");
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = null;
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = new Bitmap(@"../../Pictures/Спорт/futbol_passed.jpg");
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = null;
        }

        private void label11_MouseEnter(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = new Bitmap(@"../../Pictures/Спорт/voleyball_passed.jpg");
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = null;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = new Bitmap(@"../../Pictures/Спорт/basketball_passed.jpg");
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = null;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = new Bitmap(@"../../Pictures/Спорт/cycle_passed.jpg");
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox25.BackgroundImage = null;
        }

        private void label14_MouseEnter(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = new Bitmap(@"../../Pictures/Машины/Audi_passed.jpg");
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = null;
        }

        private void label13_MouseEnter(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = new Bitmap(@"../../Pictures/Машины/Bentley_passed.jpg");
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = null;
        }

        private void label15_MouseEnter(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = new Bitmap(@"../../Pictures/Машины/BMW_passed.jpg");
        }

        private void label15_MouseLeave(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = null;
        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = new Bitmap(@"../../Pictures/Машины/Ferrari_passed.jpg");
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = null;
        }

        private void label17_MouseEnter(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = new Bitmap(@"../../Pictures/Машины/Maserati_passed.jpg");
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = null;
        }

        private void label16_MouseEnter(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = new Bitmap(@"../../Pictures/Машины/Porsche_passed.jpg");
        }

        private void label16_MouseLeave(object sender, EventArgs e)
        {
            pictureBox26.BackgroundImage = null;
        }

        private void label20_MouseEnter(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = new Bitmap(@"../../Pictures/Животные/Bear_passed.jpg");
        }

        private void label20_MouseLeave(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = null;
        }

        private void label19_MouseEnter(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = new Bitmap(@"../../Pictures/Животные/Fox_passed.jpg");
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = null;
        }

        private void label21_MouseEnter(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = new Bitmap(@"../../Pictures/Животные/Kangaroo_passed.jpg");
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = null;
        }

        private void label18_MouseEnter(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = new Bitmap(@"../../Pictures/Животные/Lion_passed.jpg");
        }

        private void label18_MouseLeave(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = null;
        }

        private void label23_MouseEnter(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = new Bitmap(@"../../Pictures/Животные/Tiger_passed.jpg");
        }

        private void label23_MouseLeave(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = null;
        }

        private void label22_MouseEnter(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = new Bitmap(@"../../Pictures/Животные/Wolf_passed.jpg");
        }

        private void label22_MouseLeave(object sender, EventArgs e)
        {
            pictureBox27.BackgroundImage = null;
        }

        private void label26_MouseEnter(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = new Bitmap(@"../../Pictures/Компьютерные игры/Asssassins creed_passed.jpg");
        }

        private void label26_MouseLeave(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = null;
        }

        private void label25_MouseEnter(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = new Bitmap(@"../../Pictures/Компьютерные игры/Civilization_passed.jpg");
        }

        private void label25_MouseLeave(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = null;
        }

        private void label27_MouseEnter(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = new Bitmap(@"../../Pictures/Компьютерные игры/Counter Strike_passed.jpg");
        }

        private void label27_MouseLeave(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = null;
        }

        private void label24_MouseEnter(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = new Bitmap(@"../../Pictures/Компьютерные игры/Gta_passed.jpg");
        }

        private void label24_MouseLeave(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = null;
        }

        private void label29_MouseEnter(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = new Bitmap(@"../../Pictures/Компьютерные игры/Need for speed_passed.jpg");
        }

        private void label29_MouseLeave(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = null;
        }

        private void label28_MouseEnter(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = new Bitmap(@"../../Pictures/Компьютерные игры/Witcher_passed.jpg");
        }

        private void label28_MouseLeave(object sender, EventArgs e)
        {
            pictureBox28.BackgroundImage = null;
        }

        private void OpenForm(Image img, String txt)
        {
            newform.Location = new Point(this.Location.X + this.Width - 6, this.Location.Y);
            newform.pictureBox1.Image = img;
            newform.textBox1.Text = txt;
            newform.Visible = true;
            while (newform.Size.Height < this.Size.Height - 10)
            {
                Thread.Sleep(1);
                newform.Size = new Size(newform.Size.Width, newform.Size.Height + 10);
                newform.Refresh();
            }
            newform.textBox1.Text += ' ';
        }

        private void ChangeForm(Image img, String txt)
        {
            newform.pictureBox2.Image = img;
            if (newform.textBox1.Text != txt)
            {
                newform.textBox1.Text = "";
                while ((newform.textBox1.Height != 0) && (newform.textBox1.Width != 0))
                {
                    Thread.Sleep(10);
                    if (newform.textBox1.Height != 0)
                        newform.textBox1.Height = newform.textBox1.Height - 10;
                    newform.textBox1.Width = newform.textBox1.Width - 10;
                    newform.pictureBox2.Location = new Point(newform.pictureBox2.Location.X - 9, newform.pictureBox2.Location.Y);
                    newform.pictureBox1.Location = new Point(newform.pictureBox1.Location.X - 9, newform.pictureBox1.Location.Y);
                    newform.Refresh();
                }
                newform.pictureBox1.Image = img;
                newform.pictureBox1.Location = new Point(62, 25);
                newform.pictureBox2.Location = new Point(325, 25);
                while ((newform.textBox1.Height != 280) && (newform.textBox1.Width != 300))
                {
                    Thread.Sleep(1);
                    if (newform.textBox1.Height != 280)
                        newform.textBox1.Height = newform.textBox1.Height + 10;
                    newform.textBox1.Width = newform.textBox1.Width + 10;
                    this.Refresh();
                }
                newform.textBox1.Text = txt;
                newform.pictureBox1.Image = img;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Спорт/running_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s1);
            else
                ChangeForm(img, s1);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Спорт/tennis_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s2);
            else
                ChangeForm(img, s2);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Спорт/futbol_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s3);
            else
                ChangeForm(img, s3);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Спорт/voleyball_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s4);
            else
                ChangeForm(img, s4);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Спорт/basketball_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s5);
            else
                ChangeForm(img, s5);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Спорт/cycle_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s6);
            else
                ChangeForm(img, s6);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Машины/Audi_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s1);
            else
                ChangeForm(img, s1);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Машины/Bentley_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s2);
            else
                ChangeForm(img, s2);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Машины/BMW_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s3);
            else
                ChangeForm(img, s3);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Машины/Ferrari_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s4);
            else
                ChangeForm(img, s4);
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Машины/Maserati_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s5);
            else
                ChangeForm(img, s5);
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Машины/Porsche_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s6);
            else
                ChangeForm(img, s6);
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Животные/Bear_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s1);
            else
                ChangeForm(img, s1);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Животные/Fox_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s2);
            else
                ChangeForm(img, s2);
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Животные/Kangaroo_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s3);
            else
                ChangeForm(img, s3);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Животные/Lion_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s4);
            else
                ChangeForm(img, s4);
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Животные/Tiger_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s5);
            else
                ChangeForm(img, s5);
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Животные/Wolf_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s6);
            else
                ChangeForm(img, s6);
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Компьютерные игры/Asssassins creed_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s1);
            else
                ChangeForm(img, s1);
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Компьютерные игры/Civilization_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s2);
            else
                ChangeForm(img, s2);
        }

        private void label27_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Компьютерные игры/Counter Strike_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s3);
            else
                ChangeForm(img, s3);
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Компьютерные игры/Gta_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s4);
            else
                ChangeForm(img, s4);
        }

        private void label29_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Компьютерные игры/Need for speed_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s5);
            else
                ChangeForm(img, s5);
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(@"../../Pictures/Компьютерные игры/Witcher_passed.jpg");
            if (newform.Visible == false)
                OpenForm(img, s6);
            else
                ChangeForm(img, s6);
        }
    }
}
