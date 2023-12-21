using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class CustomerChange : Window
    {
        private Customer currentCustomer;
        private DataGrid CustomerGrid;
        public CustomerChange(int id, DataGrid CustomerGrid)
        {
            
            this.CustomerGrid = CustomerGrid;
            InitializeComponent();
            MedList.Items.Add("yes");
            MedList.Items.Add("no");
            List<Customer> listCust = TattoDBEntities1.GetContent().Customer.ToList();
            foreach (var i in listCust)
            {
                if (i.customer_id == id)
                {
                    currentCustomer = i;
                    break;
                }
            }
            FillTextBox();
        }

        private void FillTextBox()
        {
            NameOutput.Text = currentCustomer.firstName;
            SNameOutput.Text = currentCustomer.lastName;
            EmailOutput.Text = currentCustomer.email;
            PhoneOutput.Text = currentCustomer.phone;
            AgeOutput.Text = currentCustomer.age.ToString();
            MedList.SelectedItem = currentCustomer.medCertifivate;
            
        }

        private void btnConfirm(object sender, RoutedEventArgs e)
        {
            string name = NameOutput.Text;
            string surname = SNameOutput.Text;
            string email = EmailOutput.Text;
            string phone = PhoneOutput.Text;
            int age = Convert.ToInt32(AgeOutput.Text);
            string med = MedList.SelectedItem.ToString();
            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query =
                    "UPDATE Customer SET firstName = @Name, lastName = @Surname, email =  @Email, phone = @Phone, age = @Age, medCertifivate = @Med WHERE customer_id = @id ";
                               

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@id", currentCustomer.customer_id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Surname", surname);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Med", med);
                    
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            CustomerGrid.ItemsSource = TattoDBEntities1.Reload().Customer.ToList();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }
    }
}