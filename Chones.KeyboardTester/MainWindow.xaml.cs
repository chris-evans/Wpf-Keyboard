using System.ComponentModel;
using System.Windows;

namespace Chones.KeyboardTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        { InitializeComponent(); }

        private void _btnAlphaKeyboard_Click(object sender, RoutedEventArgs e)
        { _keyboard.Content = this.Resources["EnglishAlpha"]; }

        private void _btnNumericKeyboard_Click(object sender, RoutedEventArgs e)
        { _keyboard.Content = this.Resources["Numeric"]; }
    }
}
