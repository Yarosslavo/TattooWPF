using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class TattooAdd : Window
    {
        private Dictionary<string, int> artistDict = new Dictionary<string, int>();
        private Dictionary<string, int> placeDict = new Dictionary<string, int>();
        
        private DataGrid TattooGrid;
        public TattooAdd(DataGrid TattooGrid)
        {
            
            InitializeComponent();

            foreach (Artist i in TattoDBEntities1.Reload().Artist.ToList())
            {
                string name = i.firstName + " " + i.lastName;
                artistDict.Add(name, i.artist_id);
                ArtistList.Items.Add(name);
            }
            
            foreach (Place i in TattoDBEntities1.Reload().Place.ToList())
            {
                string place =  i.title;
                placeDict.Add( place, i.place_id);
                PlaceList.Items.Add(place);
            }
            
            this.TattooGrid = TattooGrid;
        }

        private void btnConfirm(object sender, RoutedEventArgs e)
        {
            int artist = artistDict[ArtistList.Text];
            int place = placeDict[PlaceList.Text];
           
            int price = Convert.ToInt32(PriceOutput.Text);
            string complexity = ComplexityOutput.Text;
            string size = SizeOutput.Text;
            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                string query = "INSERT INTO Tattoo ( artist_id, price, complexity, place_id, size )" +
                               "VALUES ( @Name, @Price, @Complexity, @Tilte, @Size)";

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@Name", artist);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Complexity", complexity);
                command.Parameters.AddWithValue("@Tilte", place);
                command.Parameters.AddWithValue("@Size", size);
                
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            TattooGrid.ItemsSource = TattoDBEntities1.Reload().Tattoo.ToList();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }


    }
}
