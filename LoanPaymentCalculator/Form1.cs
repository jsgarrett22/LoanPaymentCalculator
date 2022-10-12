using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoanPaymentCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // PURCHASE PRICE VALIDATION
            bool isValid = double.TryParse(txtPurchasePrice.Text, out double purchasePrice);
            if (!isValid)
            {
                ShowError(txtPurchasePrice, "Please enter a numeric purchase price.", "Invalid Data");
                ResetField(txtPurchasePrice);
                return;
            } else if (purchasePrice < 0)
            {
                ShowError(txtPurchasePrice, "Please enter a purchase price greater than 0.", "Invalid Data");
                ResetField(txtPurchasePrice);
                return;
            }

            // DOWN PAYMENT VALIDATION
            isValid = double.TryParse(txtDownPayment.Text, out double downPayment);
            if (!isValid)
            {
                txtDownPayment.BackColor = Color.Salmon;
                MessageBox.Show("Invalid Input.", "Invalid Data");
                txtDownPayment.Text = "";
                txtDownPayment.Focus();
                txtDownPayment.BackColor = Color.White;
                return;
            }
            else if (purchasePrice < 0)
            {
                txtDownPayment.BackColor = Color.Salmon;
                MessageBox.Show("Purchase price must be more than zero.", "Invalid Data");
                txtDownPayment.Text = "";
                txtDownPayment.Focus();
                txtDownPayment.BackColor = Color.White;
                return;
            }

            // INTEREST RATE VALIDATION
            isValid = double.TryParse(txtInterestRate.Text, out double interestRate);
            if (!isValid)
            {
                txtInterestRate.BackColor = Color.Salmon;
                MessageBox.Show("Invalid Input.", "Invalid Data");
                txtInterestRate.Text = "";
                txtInterestRate.Focus();
                txtInterestRate.BackColor = Color.White;
                return;
            }
            else if (purchasePrice < 0)
            {
                txtInterestRate.BackColor = Color.Salmon;
                MessageBox.Show("Purchase price must be more than zero.", "Invalid Data");
                txtInterestRate.Text = "";
                txtInterestRate.Focus();
                txtInterestRate.BackColor = Color.White;
                return;
            }

            // LOAN TERM VALIDATION
            isValid = int.TryParse(txtLoanTerm.Text, out int loanTerm);
            if (!isValid)
            {
                txtLoanTerm.BackColor = Color.Salmon;
                MessageBox.Show("Invalid Input.", "Invalid Data");
                txtLoanTerm.Text = "";
                txtLoanTerm.Focus();
                txtLoanTerm.BackColor = Color.White;
                return;
            }
            else if (purchasePrice < 0)
            {
                txtLoanTerm.BackColor = Color.Salmon;
                MessageBox.Show("Purchase price must be more than zero.", "Invalid Data");
                txtLoanTerm.Text = "";
                txtLoanTerm.Focus();
                txtLoanTerm.BackColor = Color.White;
                return;
            }

            txtFinanceAmt.Text = String.Format("{0, 0:C2}", CalculateAmountToFinance(purchasePrice, downPayment));
            txtMonthlyPayment.Text = String.Format("{0, 0:C2}", CalculateMonthlyPayment(interestRate, CalculateAmountToFinance(purchasePrice, downPayment), loanTerm));
        }

        private double ConvertRateToMonthly(double rate)
        {
            double monthlyRate = rate / 100 / 12;
            return monthlyRate;
        }

        private double CalculateAmountToFinance(double purchasePrice, double downPayment)
        {
            return purchasePrice - downPayment;
        }

        private double CalculateMonthlyPayment(double rate, double principal, int monthsFinanced)
        {
            double monthlyRate = ConvertRateToMonthly(rate);
            double frequency = Math.Pow(1 + monthlyRate, monthsFinanced);
            double monthlyPayment = monthlyRate * principal * (frequency / (frequency - 1));
            return monthlyPayment;
        }

        private void ResetField(TextBox field)
        {
            field.BackColor = Color.Salmon;
            field.Text = "";
            field.Focus();
            field.BackColor = Color.White;
        }

        private void ShowError(TextBox field, string message, string caption)
        {
            field.BackColor = Color.Salmon;
            MessageBox.Show(message, caption);
        }
    }
}
