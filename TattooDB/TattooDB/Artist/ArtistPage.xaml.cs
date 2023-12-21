using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class ArtistPage : Page
    {
        private Frame mainFrame;
        private List<Artist> artistList = TattoDBEntities1.Reload().Artist.ToList();
        private int idPosition = 0; 
        private Artist item;
        
        public ArtistPage(Frame frame)
        {
            mainFrame = frame;
            InitializeComponent();
            FillTextBox();
        }
        
        private void FillTextBox()
        { 
            item = artistList[idPosition];
            NameOutput.Text = item.firstName;
            SurnameOutput.Text = item.lastName;
            StyleOutput.Text = item.Style.title;
            ExpirienceOutput.Text = item.expirience;
            RelatedRecord();
        }

        private void btnNext(object sender, RoutedEventArgs e)
        {
            if (idPosition < artistList.Count-1 )
            {
                idPosition++;
                FillTextBox();
            }
        }

        private void btnPrev(object sender, RoutedEventArgs e)
        {
            if (idPosition > 0)
            {
                idPosition--;
                FillTextBox();
            }
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            new ArtistAdd(RlGrid).Show();
            RlGrid.ItemsSource = TattoDBEntities1.Reload().Record.ToList();
            RelatedRecord();
        }
        
        private void btnDelete(object sender, RoutedEventArgs e)
        {
            int id = 0;
            List<Record> selected = TattoDBEntities1.Reload().Record.ToList();

            int rowIndex = RlGrid.SelectedIndex;
            if (rowIndex != -1)
                id = selected[rowIndex].record_id;
            else
                return;
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;");
                connection.Open();

                string query = "DELETE FROM Record WHERE record_id = @id";
                               

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@id", id);
                
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
            RlGrid.ItemsSource = TattoDBEntities1.Reload().Record.ToList();
        }
        
        private void btnChange(object sender, RoutedEventArgs e)
        {
            int id = 0;
            List<Record> selected = TattoDBEntities1.Reload().Record.ToList();

            int rowIndex = RlGrid.SelectedIndex;
            if (rowIndex != -1)
                id = selected[rowIndex].record_id;
            else
                return;
            new ArtistChange(id, RlGrid, mainFrame).Show();
        }
        
        
        private void btnBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RelatedRecord()
        {
            int id = item.artist_id;
            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";
            string sqlExpression = "SELECT * FROM Record WHERE artist_id = @id";

            List<Record> records = TattoDBEntities1.Reload().Record.ToList();
            List<Record> recordRes = new List<Record>();
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sqlExpression, connection);
                connection.Open();

                cmd.Parameters.AddWithValue("@id", id);
                
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    foreach (Record el in records)
                    {
                        if (el.record_id == reader.GetInt32(0))
                            recordRes.Add(el);
                    }
                } 
                
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            RlGrid.ItemsSource = recordRes;
        }
    }
}