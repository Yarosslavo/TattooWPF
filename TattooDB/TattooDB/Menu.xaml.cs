using System.Windows;
using System.Windows.Controls;

namespace TattooDB
{
    public partial class Menu : Page
    {
        private Frame mainFrame;
        
        public Menu(Frame frame)
        {
            mainFrame = frame;
            InitializeComponent();
        }

        private void BtnArtist_OnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ArtistPage(mainFrame);
        }

        private void BtnCustomer(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new CustomerPage(mainFrame);
        }
        
        private void BtnTattoo(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new TattooPage(mainFrame);
        }

        private void BtnInk(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new InkPage(mainFrame);
        }
        private void BtnQuery_OnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Query(mainFrame);
        }

        private void BtnFilter_OnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Filter(mainFrame);
        }

        private void ButnExit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}