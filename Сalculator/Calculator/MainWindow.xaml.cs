using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Equal.Focus();
        }

        private void n0_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text == "Ошибка!")
                TextBox.Text = "0";
            if (TextBox.Text != "0")
                TextBox.Text += 0;
            Equal.Focus();
        }

        private void n1_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 1;
            Equal.Focus();
        }

        private void n2_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 2;
            Equal.Focus();
        }

        private void n3_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 3;
            Equal.Focus();
        }

        private void n4_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 4;
            Equal.Focus();
        }

        private void n5_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 5;
            Equal.Focus();
        }

        private void n6_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 6;
            Equal.Focus();
        }

        private void n7_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 7;
            Equal.Focus();
        }

        private void n8_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 8;
            Equal.Focus();
        }

        private void n9_Click(object sender, RoutedEventArgs e)
        {
            if ((TextBox.Text == "0") || (TextBox.Text == "Ошибка!"))
                TextBox.Text = "";
            TextBox.Text += 9;
            Equal.Focus();
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            AdditionalTextBox.Text = "";
            TextBox.Text = "0";
            Equal.Focus();
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text != "0")
                TextBox.Text = "-" + TextBox.Text;
            Equal.Focus();
        }

        private void Com_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text == "Ошибка!")
                TextBox.Text = "0";
            TextBox.Text += ",";
            Equal.Focus();
        }

        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            AdditionalTextBox.Text += TextBox.Text + " - ";
            TextBox.Text = "0";
            Equal.Focus();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AdditionalTextBox.Text += TextBox.Text + " + ";
            TextBox.Text = "0";
            Equal.Focus();
        }

        private void Mul_Click(object sender, RoutedEventArgs e)
        {
            AdditionalTextBox.Text += TextBox.Text + " * ";
            TextBox.Text = "0";
            Equal.Focus();
        }

        private void Div_Click(object sender, RoutedEventArgs e)
        {
            AdditionalTextBox.Text += TextBox.Text + " / ";
            TextBox.Text = "0";
            Equal.Focus();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text == "Ошибка!")
                TextBox.Text = "0";
            if (TextBox.Text != "0")
            {
                TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 1, 1);
                if ((TextBox.Text == "-") || (TextBox.Text == ""))
                    TextBox.Text = "0";
            }
            Equal.Focus();
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Exprecion = AdditionalTextBox.Text + TextBox.Text;
                CalculateAll(ref Exprecion, '/');
                CalculateAll(ref Exprecion, '*');
                CalculateAll(ref Exprecion, '-');
                CalculateAll(ref Exprecion, '+');
                TextBox.Text = Exprecion;
            }
            catch
            {
                TextBox.Text = "Ошибка!";
            }
            finally
            {
                AdditionalTextBox.Text = "";
                MainWindow1.Focus();
            }
        }

        private static string CalculateAll(ref string Exprecion, char Operation)
        {
            int index = -1;
            while ((index = Exprecion.IndexOf(" " + Operation + " ")) != -1)
            {
                index++;
                string LeftSide = Exprecion.Substring(0, index - 1);
                int i = -1; int lastindex = -1;
                string LeftValue = LeftSide;
                while ((i = LeftValue.IndexOf(' ')) != -1)
                {
                    LeftValue = LeftValue.Remove(0, i + 1);
                    lastindex += i + 1;
                }
                if (lastindex == -1)
                    LeftSide = "";
                else
                    LeftSide = LeftSide.Substring(0, lastindex + 1);

                string RightSide = Exprecion.Remove(0, index + 2);
                string RightValue = RightSide;
                int j = RightSide.IndexOf(' ');
                if (j == -1)
                    RightSide = "";
                else
                {
                    RightSide = RightSide.Remove(0, j);
                    RightValue = RightValue.Substring(0, j);
                }

                try
                {
                    Int64 TemptAnswer = 0;
                    switch (Operation)
                    {
                        case '*':
                            TemptAnswer = Convert.ToInt64(LeftValue) * Convert.ToInt64(RightValue);
                            break;
                        case '/':
                            throw new Exception();
                        case '+':
                            TemptAnswer = Convert.ToInt64(LeftValue) + Convert.ToInt64(RightValue);
                            break;
                        case '-':
                            TemptAnswer = Convert.ToInt64(LeftValue) - Convert.ToInt64(RightValue);
                            break;
                    }
                    Exprecion = LeftSide + TemptAnswer.ToString() + RightSide;
                }
                catch
                {
                    float TemptAnswer = 0;
                    switch (Operation)
                    {
                        case '*':
                            TemptAnswer = Convert.ToSingle(LeftValue) * Convert.ToSingle(RightValue);
                            break;
                        case '/':
                            if (RightValue == "0")
                                throw new Exception();
                            TemptAnswer = Convert.ToSingle(LeftValue) / Convert.ToSingle(RightValue);
                            break;
                        case '+':
                            TemptAnswer = Convert.ToSingle(LeftValue) + Convert.ToSingle(RightValue);
                            break;
                        case '-':
                            TemptAnswer = Convert.ToSingle(LeftValue) - Convert.ToSingle(RightValue);
                            break;
                    }
                    Exprecion = LeftSide + TemptAnswer.ToString() + RightSide;
                }
            }
            return Exprecion;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
            {
                if (e.Key == Key.D8)
                    Mul_Click(sender, e);
                if (e.Key == Key.OemPlus)
                    Add_Click(sender, e);
            }
            else
            {
                switch (e.Key)
                {
                    case Key.D0:
                        n0_Click(sender, e);
                        break;
                    case Key.D1:
                        n1_Click(sender, e);
                        break;
                    case Key.D2:
                        n2_Click(sender, e);
                        break;
                    case Key.D3:
                        n3_Click(sender, e);
                        break;
                    case Key.D4:
                        n4_Click(sender, e);
                        break;
                    case Key.D5:
                        n5_Click(sender, e);
                        break;
                    case Key.D6:
                        n6_Click(sender, e);
                        break;
                    case Key.D7:
                        n7_Click(sender, e);
                        break;
                    case Key.D8:
                        n8_Click(sender, e);
                        break;
                    case Key.D9:
                        n9_Click(sender, e);
                        break;
                    case Key.NumPad0:
                        n0_Click(sender, e);
                        break;
                    case Key.NumPad1:
                        n1_Click(sender, e);
                        break;
                    case Key.NumPad2:
                        n2_Click(sender, e);
                        break;
                    case Key.NumPad3:
                        n3_Click(sender, e);
                        break;
                    case Key.NumPad4:
                        n4_Click(sender, e);
                        break;
                    case Key.NumPad5:
                        n5_Click(sender, e);
                        break;
                    case Key.NumPad6:
                        n6_Click(sender, e);
                        break;
                    case Key.NumPad7:
                        n7_Click(sender, e);
                        break;
                    case Key.NumPad8:
                        n8_Click(sender, e);
                        break;
                    case Key.NumPad9:
                        n9_Click(sender, e);
                        break;
                    case Key.Oem2:
                        Div_Click(sender, e);
                        break;
                    case Key.Divide:
                        Div_Click(sender, e);
                        break;
                    case Key.Multiply:
                        Mul_Click(sender, e);
                        break;
                    case Key.OemMinus:
                        Sub_Click(sender, e);
                        break;
                    case Key.Subtract:
                        Sub_Click(sender, e);
                        break;
                    case Key.Add:
                        Add_Click(sender, e);
                        break;
                    case Key.OemPlus:
                        Equal_Click(sender, e);
                        break;
                    case Key.Enter:
                        Equal_Click(sender, e);
                        break;
                    case Key.OemComma:
                        Com_Click(sender, e);
                        break;
                    case Key.Back:
                        Del_Click(sender, e);
                        break;
                    case Key.Escape:
                        Clean_Click(sender, e);
                        break;
                }
            }
        }
    }
}
