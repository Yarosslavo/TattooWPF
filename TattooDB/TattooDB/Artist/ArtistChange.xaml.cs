using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class ArtistChange : Window
    {
        private Frame mainFrame;
        private Record currentRecord;
        private DataGrid RlGrid;
        
        private Dictionary<string, int> artistDict = new Dictionary<string, int>();
        private Dictionary<string, int> customerDict = new Dictionary<string, int>();

        
        public ArtistChange(int id, DataGrid RlGrid, Frame frame)
        {
            mainFrame = frame;
            this.RlGrid = RlGrid;
            InitializeComponent();
            
            List<Record> listRecord = TattoDBEntities1.Reload().Record.ToList();
            foreach (var i in listRecord)
            {
                if (i.record_id == id)
                {
                    currentRecord = i;
                    break;
                }
            }

            FillTextBox();
            
            foreach (Tattoo i in TattoDBEntities1.Reload().Tattoo.ToList())
            {
                string name = i.Artist.firstName + " " + i.Artist.lastName;
                artistDict.Add(name, Convert.ToInt32(i.artist_id));
                ArtistList.Items.Add(name);
                if (i.artist_id == currentRecord.Tattoo.tattoo_id)
                {
                    ArtistList.SelectedValue = name;
                }
            }
            
            foreach (Customer i in TattoDBEntities1.Reload().Customer.ToList())
            {
                string nameC = i.firstName + " " + i.lastName;
                customerDict.Add(nameC, i.customer_id);
                CustomerList.Items.Add(nameC);
                if (i.customer_id == currentRecord.Customer.customer_id)
                {
                    CustomerList.SelectedValue = nameC;
                }
            }
        }
        
        private void FillTextBox()
        {
            AppOutput.Text = currentRecord.appointment;
            ArtistList.SelectedItem = currentRecord.Tattoo.artist_id;
            CustomerList.SelectedItem = currentRecord.customer_id;
            
        }
        private void btnConfirm(object sender, RoutedEventArgs e)
        {
            string appointment = AppOutput.Text;
            string artist = ArtistList.SelectedItem.ToString();
            string customer = CustomerList.SelectedItem.ToString();
            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query =
                    "UPDATE Record SET artist_id = @Artist, customer_id = @Customer,  appointment = @Appointment WHERE record_id = @id ";
                               

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@id", currentRecord.record_id);
                command.Parameters.AddWithValue("@Artist", artist);
                command.Parameters.AddWithValue("@Customer", customer);
                command.Parameters.AddWithValue("@Appointment", appointment);
                
                
                    
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            mainFrame.Content = new ArtistPage(mainFrame);
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }
    }
}