using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class InkPage : Page
    {
        private Frame mainFrame;
        public InkPage(Frame frame)
        {
            mainFrame = frame;
            InitializeComponent();
            InkGrid.ItemsSource = TattoDBEntities1.GetContent().Ink.ToList();
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            new InkAdd(InkGrid).Show();
            InkGrid.ItemsSource = TattoDBEntities1.Reload().Ink.ToList();
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
             int id = 0;
             List<Ink> selected = TattoDBEntities1.GetContent().Ink.ToList();
            
             int rowIndex = InkGrid.SelectedIndex;
             if (rowIndex != -1)
                 id = selected[rowIndex].ink_id;
             else
                 return;
             try
             {
                 SqlConnection connection = new SqlConnection("Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;");
                 connection.Open();
            
                 string query = "DELETE FROM Ink WHERE ink_id = @id";
                                           
            
                 SqlCommand command = new SqlCommand(query, connection);
                            
                 command.Parameters.AddWithValue("@id", id);
                            
                 command.ExecuteNonQuery();
             }
             catch (Exception exception)
             {
                 Console.WriteLine(exception);
             }
                        
             InkGrid.ItemsSource = TattoDBEntities1.Reload().Ink.ToList();
        }

        private void btnChange(object sender, RoutedEventArgs e)
        {
            int id = 0;
            List<Ink> selected = TattoDBEntities1.GetContent().Ink.ToList();

            int rowIndex = InkGrid.SelectedIndex;
            if (rowIndex != -1)
                id = selected[rowIndex].ink_id;
            else
                return;
            new InkChange(id,InkGrid).Show();
        }
        
        private void btnBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}