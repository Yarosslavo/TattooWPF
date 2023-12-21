using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class CustomerAdd : Window
    {
        private DataGrid CustomerGrid;
        public CustomerAdd(DataGrid CustomerGrid)
        {
            InitializeComponent();
            MedList.Items.Add("yes");
            MedList.Items.Add("no");
            this.CustomerGrid = CustomerGrid;
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
                
                string query = "INSERT INTO Customer ( firstName, lastName, email, phone, age, medCertifivate )" +
                               "VALUES ( @Name, @Surname, @Email, @Phone, @Age, @Med)";

                SqlCommand command = new SqlCommand(query, connection);
                
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
