using System.Windows;

namespace Chones.Keyboard
{
    public class UnicodeKeyboardKey : KeyboardKey
    {
        public static readonly DependencyProperty UnshiftedTextProperty =
            DependencyProperty.RegisterAttached(nameof(UnshiftedText), typeof(string), typeof(UnicodeKeyboardKey));

        public static readonly DependencyProperty ShiftedUnicodeTextProperty =
            DependencyProperty.RegisterAttached(nameof(ShiftedUnicodeText), typeof(string), typeof(UnicodeKeyboardKey));

        public string UnshiftedText
        {
            get { return (string)GetValue(UnshiftedTextProperty); }
            set { SetValue(UnshiftedTextProperty, value); }
        }

        public string ShiftedUnicodeText
        {
            get { return (string)GetValue(ShiftedUnicodeTextProperty); }
            set { SetValue(ShiftedUnicodeTextProperty, value); }
        }
        static UnicodeKeyboardKey()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UnicodeKeyboardKey), new FrameworkPropertyMetadata(typeof(UnicodeKeyboardKey)));
        }
    }
}
