using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class TattooPage : Page
    {
        private Frame mainFrame;
        public TattooPage(Frame frame)
        {
            InitializeComponent();
            TattooGrid.ItemsSource = TattoDBEntities1.GetContent().Tattoo.ToList();
            mainFrame = frame;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            new TattooAdd(TattooGrid).Show();
            TattooGrid.ItemsSource = TattoDBEntities1.Reload().Tattoo.ToList();
        }
        
         private void btnDelete(object sender, RoutedEventArgs e) 
         {
             int id = 0;
             List<Tattoo> selected = TattoDBEntities1.GetContent().Tattoo.ToList();

             int rowIndex = TattooGrid.SelectedIndex;
             if (rowIndex != -1)
                 id = selected[rowIndex].tattoo_id;
             else
                 return;
             try
             {
                 SqlConnection connection = new SqlConnection("Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;");
                 connection.Open();

                 string query = "DELETE FROM Tattoo WHERE tattoo_id = @id";
                               

                 SqlCommand command = new SqlCommand(query, connection);
                
                 command.Parameters.AddWithValue("@id", id);
                
                 command.ExecuteNonQuery();
             }
             catch (Exception exception)
             {
                 Console.WriteLine(exception);
             }
            
             TattooGrid.ItemsSource = TattoDBEntities1.Reload().Tattoo.ToList();
         }

         private void btnChange(object sender, RoutedEventArgs e)
         {
             int id = 0;
             List<Tattoo> selected = TattoDBEntities1.GetContent().Tattoo.ToList();

             int rowIndex = TattooGrid.SelectedIndex;
             if (rowIndex != -1)
                 id = selected[rowIndex].tattoo_id;
             else
                 return;
             new TattooChange(id,TattooGrid).Show();
         }
         
         private void btnBack(object sender, RoutedEventArgs e) 
         {
             NavigationService.GoBack();
         }
         
    }
}