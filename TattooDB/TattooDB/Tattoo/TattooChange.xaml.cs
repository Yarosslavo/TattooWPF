using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class TattooChange : Window
    {
        private Dictionary<string, int> artistDict = new Dictionary<string, int>();
        private Dictionary<string, int> placeDict = new Dictionary<string, int>();
        
        private Tattoo currentTattoo;
        
        private DataGrid TattooGrid;
        public TattooChange(int id, DataGrid TattooGrid)
        {
            this.TattooGrid = TattooGrid;
            InitializeComponent(); 
            
            List<Tattoo> listTattoo = TattoDBEntities1.Reload().Tattoo.ToList();
            foreach (var i in listTattoo)
            {
                if (i.tattoo_id == id)
                {
                    currentTattoo = i;
                    break;
                }
            }
            
            FillTextBox();
            foreach (Artist i in TattoDBEntities1.Reload().Artist.ToList())
            {
                string name = i.firstName + " " + i.lastName;
                artistDict.Add(name, i.artist_id);
                ArtistList.Items.Add(name);
                if (i.artist_id == currentTattoo.Artist.artist_id)
                {
                    ArtistList.SelectedValue = name;
                }
            }

            foreach (Place i in TattoDBEntities1.Reload().Place.ToList())
            {
                string place = i.title;
                placeDict.Add(place, i.place_id);
                PlaceList.Items.Add(place);
                if (i.place_id == currentTattoo.Place.place_id)
                {
                    PlaceList.SelectedValue = place;
                }
            }
        }

        private void FillTextBox()
        {
            PriceOutput.Text = currentTattoo.price.ToString();
            ComplexityOutput.Text = currentTattoo.complexity;
            SizeOutput.Text = currentTattoo.size;
            ArtistList.SelectedItem = currentTattoo.artist_id;
            PlaceList.SelectedItem = currentTattoo.place_id;
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

                string query =
                    "UPDATE Tattoo SET artist_id = @Name, place_id = @Place, complexity =  @Complexity, price = @Price, size = @Size WHERE tattoo_id = @id ";
                               

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@id", currentTattoo.tattoo_id);
                command.Parameters.AddWithValue("@Name", artist);
                command.Parameters.AddWithValue("@Place", place);
                command.Parameters.AddWithValue("@Complexity", complexity);
                command.Parameters.AddWithValue("@Price", price);
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