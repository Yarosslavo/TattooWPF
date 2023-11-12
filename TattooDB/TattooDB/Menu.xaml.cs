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
            mainFrame.Content = new ArtistPage();
            
        }

        private void ButnExit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}