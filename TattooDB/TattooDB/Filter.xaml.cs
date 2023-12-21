using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class Filter : Page
    {
        private Dictionary<string, int> styleDict = new Dictionary<string, int>();
        private Frame mainFrame;
        public Filter(Frame frame)
        {
            mainFrame = frame;
            InitializeComponent();
            
            FilterList.Items.Add("All");
            List<Style> StyleList = TattoDBEntities1.Reload().Style.ToList();
            foreach (Style i in StyleList)
            {
                string style = i.title;
                styleDict.Add(style, i.style_id);
                FilterList.Items.Add(style);
            }

            FilterList.SelectionChanged += DataFilter;
            FilterList.SelectedIndex = 0;
        }

        private void btnBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DataFilter(object sender, RoutedEventArgs e)
        {
            List<Artist> ArtistList = new List<Artist>();

            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";
             
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query;
                int id = 0;

                if (FilterList.SelectedIndex == 0)
                {
                    query = "SELECT * FROM Artist";
                }
                else
                {
                   query = "SELECT * FROM Artist WHERE style_id = @id ";
                   id = styleDict[FilterList.Text];
                }
                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foreach (Artist i in TattoDBEntities1.Reload().Artist.ToList())
                    {
                        if (i.artist_id == reader.GetInt32(0))
                        {
                            ArtistList.Add(i);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            FilterGrid.ItemsSource = ArtistList;
        }
    }
}
