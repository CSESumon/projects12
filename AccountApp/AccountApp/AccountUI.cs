using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountApp
{
    public partial class AccountUI : Form
    {
        private Account anAccount = new Account();
        private double amount;

        public AccountUI()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            anAccount.name = nameTextBox.Text;
            anAccount.number = numberTextBox.Text;
            MessageBox.Show("Account has been created");
        }

        private void depositButton_Click(object sender, EventArgs e)
        {
            amount = Convert.ToDouble(amountTextBox.Text);
            anAccount.Deposit(amount);
            MessageBox.Show("Deposited");
        }

        private void withdrawButton_Click(object sender, EventArgs e)
        {
            amount = Convert.ToDouble(amountTextBox.Text);
            anAccount.Withdraw(amount);
            MessageBox.Show("Withdraw");
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(anAccount.GetReport());
        }
    }
}
