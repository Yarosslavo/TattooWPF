using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TattooDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Menu(MainFrame);
        }
    }
}