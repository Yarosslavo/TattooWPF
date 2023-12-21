using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TattooDB
{
    public partial class Query : Page
    {
        private Frame mainFrame;
        
        public Query(Frame frame)
        {
            mainFrame = frame;
            InitializeComponent();
        }
        
        private void BtnExit (object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnClear(object sender, RoutedEventArgs e)
        {
            QueryInput.Clear();
            QueryGrid.ItemsSource = new List<object>();
        }

        private void ProcessSQL(object sender, RoutedEventArgs e)
        {
            string expression = QueryInput.Text;
            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                SqlDataAdapter ad = new SqlDataAdapter(expression, connection);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                QueryGrid.ItemsSource = dt.DefaultView;
                connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}