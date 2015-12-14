using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.ShiftActivatedEvent, new RoutedEventHandler(OnShiftActivated));
            EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.ShiftDeactivatedEvent, new RoutedEventHandler(OnShiftDeactivated));
        }

        public Keyboard()
        {
            Focusable = false;
            IsTabStop = false;
        }

        internal static void OnShiftActivated(object sender, RoutedEventArgs e)
        { 
            var keyboard = (Keyboard)sender;
            keyboard.IsShifted = true;
            e.Handled = true;
        }

        internal static void OnShiftDeactivated(object sender, RoutedEventArgs e)
        {
            var keyboard = (Keyboard)sender;
            keyboard.IsShifted = false;
            e.Handled = true;
        }

        private static void IsShiftedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var isShifted = (bool)e.NewValue;
            var allKeys = FindVisualChildren<KeyboardKey>(obj);
            foreach (var key in allKeys)
            { key.IsShifted = isShifted; }
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
