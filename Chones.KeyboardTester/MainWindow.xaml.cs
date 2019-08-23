using System.Windows;

namespace Rife.KeyboardTester
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{ InitializeComponent(); }

		private void _btnAlphaKeyboard_Click(object sender, RoutedEventArgs e)
		{
			Keyboard.KeyboardState = Rife.Keyboard.KeyboardState.AlphaNumeric;
		}

		private void _btnNumericKeyboard_Click(object sender, RoutedEventArgs e)
		{
			Keyboard.KeyboardState = Rife.Keyboard.KeyboardState.Numeric;
		}
	}
}