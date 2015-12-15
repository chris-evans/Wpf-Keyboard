using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Chones.Keyboard
{
    [TemplateVisualState(Name = "NotShifted", GroupName = "ShiftedGroup")]
    [TemplateVisualState(Name = "Shifted", GroupName = "ShiftedGroup")]
    [TemplateVisualState(Name = "CapsLocked", GroupName = "CapsLockedGroup")]
    [TemplateVisualState(Name = "NoCapsLocked", GroupName = "CapsLockedGroup")]
    public class KeyboardKey : Button
    {
        /// <summary>
        /// Event indicating that the shift status has been activated
        /// </summary>
        public static readonly RoutedEvent ShiftModifierChangedProperty =
            EventManager.RegisterRoutedEvent(nameof(ShiftModifierChanged), RoutingStrategy.Bubble, typeof(ModifierChangedRoutedEventHandler), typeof(KeyboardKey));

        public static readonly RoutedEvent CapsModifierChangedProperty =
            EventManager.RegisterRoutedEvent(nameof(CapsModifierChanged), RoutingStrategy.Bubble, typeof(ModifierChangedRoutedEventHandler), typeof(KeyboardKey));

        public static readonly DependencyProperty IsShiftedProperty =
            DependencyProperty.RegisterAttached(nameof(IsShifted), typeof(bool), typeof(KeyboardKey),
                new PropertyMetadata(false, new PropertyChangedCallback(OnIsShiftedChanged)));

        public static readonly DependencyProperty IsCapsLockedProperty =
            DependencyProperty.RegisterAttached(nameof(IsCapsLocked), typeof(bool), typeof(KeyboardKey),
                new PropertyMetadata(false, new PropertyChangedCallback(OnIsCapsLockedChanged)));

        public static readonly DependencyProperty ShiftOnCapsLockProperty =
            DependencyProperty.RegisterAttached(nameof(ShiftOnCapsLock), typeof(bool), typeof(KeyboardKey),
                new PropertyMetadata(false));

        public static readonly DependencyProperty ShiftedContentProperty =
            DependencyProperty.RegisterAttached(nameof(ShiftedContent), typeof(object), typeof(KeyboardKey));

        public static readonly DependencyProperty UnshiftedContentProperty =
            DependencyProperty.RegisterAttached(nameof(UnshiftedContent), typeof(object), typeof(KeyboardKey));

        public event ModifierChangedRoutedEventHandler ShiftModifierChanged
        {
            add { AddHandler(ShiftModifierChangedProperty, value); }
            remove { RemoveHandler(ShiftModifierChangedProperty, value); }
        }

        public event ModifierChangedRoutedEventHandler CapsModifierChanged
        {
            add { AddHandler(CapsModifierChangedProperty, value); }
            remove { RemoveHandler(CapsModifierChangedProperty, value); }
        }

        public bool IsShifted
        {
            get { return (bool)GetValue(IsShiftedProperty); }
            set { SetValue(IsShiftedProperty, value); }
        }

        public bool IsCapsLocked
        {
            get { return (bool)GetValue(IsCapsLockedProperty); }
            set { SetValue(IsCapsLockedProperty, value); }
        }

        public bool ShiftOnCapsLock
        {
            get { return (bool)GetValue(ShiftOnCapsLockProperty); }
            set { SetValue(ShiftOnCapsLockProperty, true); }
        }

        public object ShiftedContent
        {
            get { return (object)GetValue(ShiftedContentProperty); }
            set { SetValue(ShiftedContentProperty, value); }
        }

        public object UnshiftedContent
        {
            get { return (object)GetValue(UnshiftedContentProperty); }
            set { SetValue(UnshiftedContentProperty, value); }
        }

        static KeyboardKey()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyboardKey), new FrameworkPropertyMetadata(typeof(KeyboardKey)));
        }

        public KeyboardKey()
        {
            Focusable = false;
            IsTabStop = false;
            IsShifted = false;
        }

        protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
        {
            DetermineAndApplyContent();
            base.OnTemplateChanged(oldTemplate, newTemplate);
        }

        private static void OnIsShiftedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var keyboardKey = (KeyboardKey)obj;

            if ((bool) e.NewValue)
            { VisualStateManager.GoToState(keyboardKey, "Shifted", true);  }
            else
            { VisualStateManager.GoToState(keyboardKey, "NotShifted", true); }

            keyboardKey.DetermineAndApplyContent();
        }

        private static void OnIsCapsLockedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var keyboardKey = (KeyboardKey)obj;

            if ((bool)e.NewValue)
            { VisualStateManager.GoToState(keyboardKey, "CapsLocked", true); }
            else
            { VisualStateManager.GoToState(keyboardKey, "NoCapsLocked", true); }

            keyboardKey.DetermineAndApplyContent();
        }

        private void DetermineAndApplyContent()
        {
            var capsMatters = ShiftOnCapsLock && IsCapsLocked;
            var setShiftMode = IsShifted ^ capsMatters;
            Content = setShiftMode ? ShiftedContent : UnshiftedContent;
        }
    }
}
