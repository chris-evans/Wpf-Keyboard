using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Rife.Keyboard
{
    public class Keyboard : ContentControl
    {
        public static readonly DependencyProperty IsShiftLockedProperty =
            DependencyProperty.RegisterAttached(nameof(IsShiftLocked), typeof(bool), typeof(Keyboard));

        public static readonly DependencyProperty IsShiftedProperty =
            DependencyProperty.RegisterAttached(nameof(IsShifted), typeof(bool), typeof(Keyboard), new PropertyMetadata(new PropertyChangedCallback(IsShiftedChanged)));

        public static readonly DependencyProperty IsCapsLockedProperty =
            DependencyProperty.RegisterAttached(nameof(IsCapsLocked), typeof(bool), typeof(Keyboard), new PropertyMetadata(new PropertyChangedCallback(IsCapsLockedChanged)));

        public bool IsShifted
        {
            get { return (bool)GetValue(IsShiftedProperty); }
            set { SetValue(IsShiftedProperty, value); }
        }

        public bool IsShiftLocked
        {
            get { return (bool)GetValue(IsShiftLockedProperty); }
            set { SetValue(IsShiftLockedProperty, value); }
        }

        public bool IsCapsLocked
        {
            get { return (bool)GetValue(IsCapsLockedProperty); }
            set { SetValue(IsCapsLockedProperty, value); }
        }

        static Keyboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(typeof(Keyboard)));
            EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.ShiftModifierChangedProperty, new ModifierChangedRoutedEventHandler(OnShiftModified));
            EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.CapsModifierChangedProperty, new ModifierChangedRoutedEventHandler(OnCapsModified));
            EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.ClickEvent, new RoutedEventHandler(OnKeyClicked));
        }

        public Keyboard()
        {
            Focusable = false;
            IsTabStop = false;
        }

        private static void OnShiftModified(object sender, ModifierChangedRoutedEventArgs e)
        { 
            var keyboard = (Keyboard)sender;
            keyboard.IsShifted = e.Applied;
            keyboard.IsShiftLocked = e.Locked;
            e.Handled = true;
        }

        private static void OnCapsModified(object sender, ModifierChangedRoutedEventArgs e)
        {
            var keyboard = (Keyboard)sender;
            keyboard.IsCapsLocked = e.Applied;
            e.Handled = true;
        }

        private static void OnKeyClicked(object sender, RoutedEventArgs e)
        {
            // we will only react to changing shift behavior if the key pressed
            // was a keyboard key
            if (e.OriginalSource is KeyboardKey)
            {
                var keyboard = (Keyboard)sender;
                if (!keyboard.IsShiftLocked && keyboard.IsShifted)
                { keyboard.IsShifted = false; }
            }
        }

        private static void IsShiftedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var isShifted = (bool)e.NewValue;
            var allKeys = FindVisualChildren<KeyboardKey>(obj);
            foreach (var key in allKeys)
            { key.IsShifted = isShifted; }
        }

        private static void IsCapsLockedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var isCapsLocked = (bool)e.NewValue;
            var allKeys = FindVisualChildren<KeyboardKey>(obj);
            foreach (var key in allKeys)
            { key.IsCapsLocked = isCapsLocked; }
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
