using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WindowsInput;
using WindowsInput.Native;

namespace Chones.Keyboard
{
    public class Keyboard : ContentControl
    {
        public static readonly DependencyProperty IsShiftedProperty =
            DependencyProperty.RegisterAttached(nameof(IsShifted), typeof(bool), typeof(Keyboard), new PropertyMetadata(new PropertyChangedCallback(IsShiftedChanged)));

        public bool IsShifted
        {
            get { return (bool)GetValue(IsShiftedProperty); }
            set { SetValue(IsShiftedProperty, value); }
        }

        static Keyboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(typeof(Keyboard)));
            EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.KeyboardKeyPressedEvent, new RoutedEventHandler(OnKeyboardKeyPressed));
        }

        public Keyboard()
        {
            Focusable = false;
            IsTabStop = false;
        }

        internal static void OnKeyboardKeyPressed(object sender, RoutedEventArgs e)
        {
            var keyboard = (Keyboard)sender;

            var applied = keyboard.ApplyForUnicode(e.OriginalSource);
            if (!applied) keyboard.ApplyForVirtualKey(e.OriginalSource);
            if (!applied) keyboard.ApplyForShift(e.OriginalSource);

            if (applied)
            { e.Handled = true; }

        }

        private static void IsShiftedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var isShifted = (bool)e.NewValue;
            var allKeys = FindVisualChildren<KeyboardKey>(obj);
            foreach (var key in allKeys)
            {
                if (isShifted)
                { key.SetShifted(); }
                else
                { key.UnsetShifted(); }
            }
        }

        private bool ApplyForShift(object originalSource)
        {
            var applied = false;
            var keyboardKey = originalSource as ShiftKeyboardKey;
            if (keyboardKey != null)
            {
                IsShifted = !IsShifted;
                applied = true;
            }

            return applied;
        }

        private bool ApplyForUnicode(object originalSource)
        {
            var applied = false;
            var keyboardKey = originalSource as UnicodeKeyboardKey;
            if (keyboardKey != null)
            {
                var sim = new InputSimulator();
                if (IsShifted && !string.IsNullOrEmpty(keyboardKey.ShiftedUnicodeText))
                { sim.Keyboard.TextEntry(keyboardKey.ShiftedUnicodeText); }
                else if (!string.IsNullOrEmpty(keyboardKey.UnshiftedText))
                { sim.Keyboard.TextEntry(keyboardKey.UnshiftedText); }
                applied = true;
            }

            return applied;
        }

        private bool ApplyForVirtualKey(object originalSource)
        {
            var applied = false;
            var keyboardKey = originalSource as VirtualKeyKeyboardKey;
            if (keyboardKey != null)
            {
                var sim = new InputSimulator();
                if (IsShifted)
                { sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, keyboardKey.VirtualKey); }
                else
                { sim.Keyboard.KeyPress(keyboardKey.VirtualKey); }
                applied = true;
            }

            return applied;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    { yield return (T)child; }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    { yield return childOfChild; }
                }
            }
        }

    }
}
