using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp
{
    class Account
    {
        public string number;
        public string name;
        public double balance;

        public string Deposit(double amount)
        {
            balance += amount;
            return "Deposited";
        }

        public string Withdraw(double amount)
        {
            balance -= amount;
            return "Withdrawn";
        }

        public string GetReport()
        {
            return name + ", your account no is " + number + " and it's balance is : " + balance;
        }
    }
}
