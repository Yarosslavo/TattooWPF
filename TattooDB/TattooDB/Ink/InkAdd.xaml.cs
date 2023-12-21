using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class InkAdd : Window
    {
        private DataGrid InkGrid;
        private Dictionary<string, int> inkDict = new Dictionary<string, int>();
        public InkAdd(DataGrid InkGrid)
        {
            InitializeComponent();

            foreach (InkType i in TattoDBEntities1.Reload().InkType.ToList())
            {
                string type = i.title;
                inkDict.Add( type, i.inkType_id);
                TypeList.Items.Add(type);
            }
            AvList.Items.Add("yes");
            AvList.Items.Add("no");
            this.InkGrid = InkGrid;
        }

        private void btnConfirm(object sender, RoutedEventArgs e)
        {
            int type = inkDict[TypeList.Text];
            string colors = ColorOutput.Text;
            int price = Convert.ToInt32(PriceOutput.Text);
            string aviability = AvList.SelectedItem.ToString();
            string connectionString = "Data Source=DESKTOP-S1RINUD;Initial Catalog=TattooDB;Integrated Security=true;";
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                string query = "INSERT INTO Ink ( inkType_id, color, price, aviability)" +
                               "VALUES ( @Type, @Color, @Price, @Aviability)";

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@Color", colors);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Aviability", aviability);
                    
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            InkGrid.ItemsSource = TattoDBEntities1.Reload().Ink.ToList();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        } 
    }
}
