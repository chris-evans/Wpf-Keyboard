using System.Windows;
using WindowsInput;

namespace Rife.Keyboard
{
    public class UnicodeKeyboardKey : KeyboardKey
    {
        public static readonly DependencyProperty UnshiftedTextProperty =
            DependencyProperty.RegisterAttached("UnshiftedText", typeof(string), typeof(UnicodeKeyboardKey));

        public static readonly DependencyProperty ShiftedUnicodeTextProperty =
            DependencyProperty.RegisterAttached("ShiftedUnicodeText", typeof(string), typeof(UnicodeKeyboardKey));

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
            ShiftOnCapsLockProperty.OverrideMetadata(typeof(UnicodeKeyboardKey), new FrameworkPropertyMetadata(true));
        }

        protected override void OnClick()
        {
            var capsMatters = ShiftOnCapsLock && IsCapsLocked;
            var setShiftMode = IsShifted ^ capsMatters;

            var sim = new InputSimulator();
            if (setShiftMode && !string.IsNullOrEmpty(ShiftedUnicodeText))
            { sim.Keyboard.TextEntry(ShiftedUnicodeText); }
            else if (!string.IsNullOrEmpty(UnshiftedText))
            { sim.Keyboard.TextEntry(UnshiftedText); }

            base.OnClick();
        }
    }
}