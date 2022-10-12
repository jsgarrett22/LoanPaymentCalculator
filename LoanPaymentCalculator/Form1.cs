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
            double purchasePrice;
            double downPayment;
            int loanTerm;
            double interestRate;
            
            try
            {
                purchasePrice = double.Parse(txtPurchasePrice.Text);
                downPayment = double.Parse(txtDownPayment.Text);
                interestRate = double.Parse(txtInterestRate.Text);
                loanTerm = int.Parse(txtLoanTerm.Text);
                txtFinanceAmt.Text = String.Format("{0, 0:C2}", CalculateAmountToFinance(purchasePrice, downPayment));
                txtMonthlyPayment.Text = String.Format("{0, 0:C2}", CalculateMonthlyPayment(interestRate, CalculateAmountToFinance(purchasePrice, downPayment), loanTerm));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double ConvertRateToMonthly(double rate)
        {
            double monthlyRate = rate / 100 / 12;
            return monthlyRate;
        }

        public double CalculateAmountToFinance(double purchasePrice, double downPayment)
        {
            return purchasePrice - downPayment;
        }

        public double CalculateMonthlyPayment(double rate, double principal, int monthsFinanced)
        {
            double monthlyRate = ConvertRateToMonthly(rate);
            double frequency = Math.Pow(1 + monthlyRate, monthsFinanced);
            double monthlyPayment = monthlyRate * principal * (frequency / (frequency - 1));
            return monthlyPayment;
        }
    }
}
