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
            } else if (purchasePrice <= 0)
            {
                ShowError(txtPurchasePrice, "Please enter a purchase price greater than 0.", "Invalid Data");
                ResetField(txtPurchasePrice);
                return;
            }

            // DOWN PAYMENT VALIDATION
            isValid = double.TryParse(txtDownPayment.Text, out double downPayment);
            if (!isValid)
            {
                ShowError(txtDownPayment, "Please enter a numeric purchase price.", "Invalid Data");
                ResetField(txtDownPayment);
                return;
            }
            else if (downPayment < 0)
            {
                ShowError(txtDownPayment, "Please enter a down payment greater than 0.", "Invalid Data");
                ResetField(txtDownPayment);
                return;
            }

            // INTEREST RATE VALIDATION
            isValid = double.TryParse(txtInterestRate.Text, out double interestRate);
            if (!isValid)
            {
                ShowError(txtInterestRate, "Please enter a numeric interest rate.", "Invalid Data");
                ResetField(txtInterestRate);
                return;
            }
            else if (interestRate <= 0)
            {
                ShowError(txtInterestRate, "Please enter an interest rate greater than 0.0.", "Invalid Data");
                ResetField(txtInterestRate);
                return;
            }

            // LOAN TERM VALIDATION
            isValid = int.TryParse(txtLoanTerm.Text, out int loanTerm);
            if (!isValid)
            {
                ShowError(txtLoanTerm, "Please enter a numeric loan term.", "Invalid Data");
                ResetField(txtLoanTerm);
                return;
            }
            else if (loanTerm <= 0)
            {
                ShowError(txtLoanTerm, "Please enter a loan term greater than at least 1 month.", "Invalid Data");
                ResetField(txtLoanTerm);
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
