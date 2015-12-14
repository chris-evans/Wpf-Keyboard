using System.Windows;

namespace Chones.Keyboard
{
    public class UnicodeKeyboardKey : KeyboardKey
    {
        public static readonly DependencyProperty UnmodifiedUnitcodeTextProperty =
            DependencyProperty.RegisterAttached(nameof(UnmodifiedUnicodeText), typeof(string), typeof(UnicodeKeyboardKey));

        public static readonly DependencyProperty ShiftedUnicodeTextProperty =
            DependencyProperty.RegisterAttached(nameof(ShiftedUnicodeText), typeof(string), typeof(UnicodeKeyboardKey));

        public string UnmodifiedUnicodeText
        {
            get { return (string)GetValue(UnmodifiedUnitcodeTextProperty); }
            set { SetValue(UnmodifiedUnitcodeTextProperty, value); }
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
