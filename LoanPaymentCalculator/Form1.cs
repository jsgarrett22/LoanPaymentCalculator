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
            double.TryParse(txtPurchasePrice.Text, out double purchasePrice);
            double.TryParse(txtDownPayment.Text, out double downPayment);
            double.TryParse(txtInterestRate.Text, out double interestRate);
            int.TryParse(txtLoanTerm.Text, out int loanTerm);
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
    }
}
