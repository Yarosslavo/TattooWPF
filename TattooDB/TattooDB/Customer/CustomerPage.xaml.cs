using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class CustomerPage : Page
    {
        private Frame mainFrame;
        public CustomerPage(Frame frame)
        {
            InitializeComponent();
            CustomerGrid.ItemsSource = TattoDBEntities1.GetContent().Customer.ToList();
            mainFrame = frame;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            new CustomerAdd(CustomerGrid).Show();
            CustomerGrid.ItemsSource = TattoDBEntities1.Reload().Customer.ToList();
        }
        private void btnDelete(object sender, RoutedEventArgs e)
        {
            int id = 0;
            List<Customer> selected = TattoDBEntities1.GetContent().Customer.ToList();

            int rowIndex = CustomerGrid.SelectedIndex;
            if (rowIndex != -1)
                id = selected[rowIndex].customer_id;
            else
                return;
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;");
                connection.Open();

                string query = "DELETE FROM Customer WHERE customer_id = @id";
                               

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@id", id);
                
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
            CustomerGrid.ItemsSource = TattoDBEntities1.Reload().Customer.ToList();
        }
        private void btnChange(object sender, RoutedEventArgs e)
        {
            int id = 0;
            List<Customer> selected = TattoDBEntities1.GetContent().Customer.ToList();

            int rowIndex = CustomerGrid.SelectedIndex;
            if (rowIndex != -1)
                id = selected[rowIndex].customer_id;
            else
                return;
            new CustomerChange(id,CustomerGrid).Show();
        }

        private void btnBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}