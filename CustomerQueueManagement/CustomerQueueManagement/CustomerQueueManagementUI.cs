using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerQueueManagement
{
    public partial class CustomerQueueManagementUI : Form
    {

        enum Progress_Status
        {
            Not_Served,
            On_Service,
            Served
        }

        public CustomerQueueManagementUI()
        {
            InitializeComponent();
        }

        string connString = @"Data source = USER-PC\SQLEXPRESS; Database = CustomerQueueDB; Integrated Security = true";

        private void enqueueButton_Click(object sender, EventArgs e)
        {
            SaveComplain();
            ShowCustomerComplain();
        }

        private void ShowCustomerComplain()
        {
            SqlConnection aSqlConnection = new SqlConnection(connString);
            aSqlConnection.Open();
            string commandText = "SELECT * FROM t_customer_complain WHERE progress_status = '" + Progress_Status.Not_Served + "' OR progress_status = '" + Progress_Status.On_Service + "'";
            SqlCommand aSqlCommand = new SqlCommand(commandText, aSqlConnection);

            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();

            List<Customer> customers = new List<Customer>();

            while (aSqlDataReader.Read() == true)
            {
                Customer aCustomer = new Customer();
                aCustomer.serial = Convert.ToInt32(aSqlDataReader["Id"]);
                aCustomer.name = aSqlDataReader["name"].ToString();
                aCustomer.complain = aSqlDataReader["complain"].ToString();
                aCustomer.status = aSqlDataReader["progress_status"].ToString();
                customers.Add(aCustomer);
            }
            aSqlConnection.Close();

            remainingCustomerListView.Items.Clear();

            foreach (Customer aCustomer in customers)
            {
                ListViewItem aListViewItem = new ListViewItem();
                aListViewItem.Text = aCustomer.serial.ToString();
                aListViewItem.SubItems.Add(aCustomer.name);
                aListViewItem.SubItems.Add(aCustomer.complain);
                aListViewItem.SubItems.Add(aCustomer.status);
                remainingCustomerListView.Items.Add(aListViewItem);
            }

            remainingLabel.Text = customers.Count.ToString();
            ShowNoOfCustomerServedToday();
        }

        public void ShowNoOfCustomerServedToday()
        {
            SqlConnection aSqlConnection = new SqlConnection(connString);
            aSqlConnection.Open();
            string commandText = "SELECT COUNT(Id) FROM t_customer_complain WHERE progress_status = '" + Progress_Status.Served +"'";
            SqlCommand aSqlCommand = new SqlCommand(commandText, aSqlConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
            aSqlDataReader.Read();
            int noOfServedCustomer = Convert.ToInt32(aSqlDataReader[0]);
            aSqlConnection.Close();
            servedLabel.Text = noOfServedCustomer.ToString();

        }

        public void SaveComplain()
        {
            SqlConnection aSqlConnection = new SqlConnection(connString);
            aSqlConnection.Open();
            string commandText = "INSERT INTO t_customer_complain VALUES('" + nameTextBox.Text + "','" + complainTextbox.Text + "','" + Progress_Status.Not_Served +"')";
            SqlCommand aSqlCommand = new SqlCommand(commandText, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();
            aSqlConnection.Close();
        }

        private void CustomerQueueManagementUI_Load(object sender, EventArgs e)
        {
            ShowCustomerComplain();
        }
    }
}
