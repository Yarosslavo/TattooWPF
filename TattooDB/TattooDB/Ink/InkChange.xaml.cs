using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TattooDB
{
    public partial class InkChange : Window
    {
        private Ink currentInk;
        public DataGrid InkGrid;
        private Dictionary<string, int> inkDict = new Dictionary<string, int>();
        public InkChange(int id, DataGrid InkGrid)
        {
            this.InkGrid = InkGrid;
            InitializeComponent();
            
            List<Ink> listInk = TattoDBEntities1.Reload().Ink.ToList();
            foreach (var i in listInk)
            {
                if (i.ink_id == id)
                {
                    currentInk = i;
                    break;
                }
            }

            FillTextBox();
            
            foreach (InkType i in TattoDBEntities1.Reload().InkType.ToList())
            {
                string type = i.title;
                inkDict.Add( type, i.inkType_id);
                TypeList.Items.Add(type);
                if (i.inkType_id == currentInk.InkType.inkType_id)
                {
                    TypeList.SelectedValue = type;
                }
            }
            AvList.Items.Add("yes");
            AvList.Items.Add("no");
        }

        private void FillTextBox()
        {
            ColorOutput.Text = currentInk.color;
            PriceOutput.Text = currentInk.price.ToString();
            TypeList.SelectedItem = currentInk.inkType_id;
            AvList.SelectedItem = currentInk.aviability;
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

                string query =
                    "UPDATE Ink SET inkType_id = @Type, color = @Color,  price = @Price, aviability = @Aviability WHERE ink_id = @id ";
                               

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@id", currentInk.ink_id);
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