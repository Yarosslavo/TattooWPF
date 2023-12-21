using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class ArtistAdd : Window
    {
        private Dictionary<string, int> artistDict = new Dictionary<string, int>();
        private Dictionary<string, int> customerDict = new Dictionary<string, int>();

        private DataGrid RlGrid;
        public ArtistAdd(DataGrid RlGrid)
        {
            InitializeComponent();
            foreach (Artist i in TattoDBEntities1.Reload().Artist.ToList())
            {
                string name = i.firstName + " " + i.lastName;
                artistDict.Add(name, i.artist_id);
                ArtistList.Items.Add(name);
            }
            
            foreach (Customer i in TattoDBEntities1.Reload().Customer.ToList())
            {
                string nameC = i.firstName + " " + i.lastName;
                customerDict.Add(nameC, i.customer_id);
                CustomerList.Items.Add(nameC);
            }

            this.RlGrid = RlGrid;
        }

        private void btnConfirm(object sender, RoutedEventArgs e)
        {
            int artist = artistDict[ArtistList.Text];
            int customer = customerDict[CustomerList.Text];
            string appointment = AppOutput.Text;
            
            
            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                string query = "INSERT INTO Record ( appointment, artist_id, customer_id  " +
                               "VALUES ( @Appointment, @Artist, @Customer,)";

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@Appointment", appointment);
                command.Parameters.AddWithValue("@Artist", artist);
                command.Parameters.AddWithValue("@Customer", customer);
                    
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            RlGrid.ItemsSource = TattoDBEntities1.Reload().Record.ToList();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }
    }
}