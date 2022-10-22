using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtDisp.Text);
            }catch(System.ArgumentNullException)
            {
                Clipboard.SetText(" ");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            previousOperation = Operation.None;
            txtDisp.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (txtDisp.Text.Length > 0)
            {
                double d;
                if (double.TryParse (txtDisp.Text[txtDisp.Text.Length - 1].ToString(), out d))
                {
                    previousOperation = Operation.None;
                }

                txtDisp.Text = txtDisp.Text.Remove(txtDisp.Text.Length - 1, 1);
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                performCalculations(previousOperation);

            previousOperation = Operation.Div;
            txtDisp.Text += (sender as Button).Text;
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                performCalculations(previousOperation);

            previousOperation = Operation.Mul;
            txtDisp.Text += (sender as Button).Text;
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                performCalculations(previousOperation);

            previousOperation = Operation.Sub;
            txtDisp.Text += (sender as Button).Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                performCalculations(previousOperation);
            
            previousOperation = Operation.Add;
            txtDisp.Text += (sender as Button).Text;
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            if (previousOperation == Operation.None)
            {
                return;
            }
            else
            {
                performCalculations(previousOperation);
            }
        }

        private void performCalculations(Operation previousOperation)
        {
            List<double> lstNums = new List<double>();
            try
            {
                switch (previousOperation)
                {
                    case Operation.Add:
                        lstNums = txtDisp.Text.Split('+').Select(double.Parse).ToList();
                        txtDisp.Text = (lstNums[0] + lstNums[1]).ToString();
                        break;
                    case Operation.Sub:
                        lstNums = txtDisp.Text.Split('-').Select(double.Parse).ToList();
                        txtDisp.Text = (lstNums[0] - lstNums[1]).ToString();
                        break;
                    case Operation.Mul:
                        lstNums = txtDisp.Text.Split('*').Select(double.Parse).ToList();
                        txtDisp.Text = (lstNums[0] * lstNums[1]).ToString();
                        break;
                    case Operation.Div:
                        try
                        {
                            lstNums = txtDisp.Text.Split('/').Select(double.Parse).ToList();
                            txtDisp.Text = (lstNums[0] / lstNums[1]).ToString();
                        }
                        catch (DivideByZeroException)
                        {
                            txtDisp.Text = "ERR0R";
                        }
                        break;
                    default:
                        break;
                }
            }catch( System.ArgumentOutOfRangeException)
            {
                //catch to avoid error for continuous calculation
            }
            catch (System.FormatException)
            {
                Clipboard.SetText(txtDisp.Text);
                var formatE = "Incorrect input format. Last value entered must be a number.";
                var title = "!";
                MessageBox.Show(formatE, title);
            }

        }

        private void BtnNum_Click(object btn, EventArgs e)
        {
            txtDisp.Text += (btn as Button).Text;
        }

        enum Operation
        {
            Add,
            Sub,
            Mul,
            Div,
            None
        }
        static Operation previousOperation = Operation.None;
    }
}
