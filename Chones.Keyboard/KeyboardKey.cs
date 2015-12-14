using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chones.Keyboard
{
    [TemplateVisualState(Name = "Unshifted", GroupName = "ModifiersGroup")]
    [TemplateVisualState(Name = "Shifted", GroupName = "ModifiersGroup")]
    public class KeyboardKey : Button
    {
        /// <summary>
        /// Event indicating that the shift status has been activated
        /// </summary>
        public static readonly RoutedEvent ShiftModifiedEvent =
            EventManager.RegisterRoutedEvent(nameof(ShiftModified), RoutingStrategy.Bubble, typeof(ShiftModifiedRoutedEventHandler), typeof(KeyboardKey));

        public static readonly DependencyProperty IsShiftedProperty =
            DependencyProperty.RegisterAttached(nameof(IsShifted), typeof(bool), typeof(KeyboardKey),
                new PropertyMetadata(false, new PropertyChangedCallback(OnIsShiftedChanged)));

        public static readonly DependencyProperty ShiftedContentProperty =
            DependencyProperty.RegisterAttached(nameof(ShiftedContent), typeof(object), typeof(KeyboardKey));

        public static readonly DependencyProperty UnshiftedContentProperty =
            DependencyProperty.RegisterAttached(nameof(UnshiftedContent), typeof(object), typeof(KeyboardKey));

        public event ShiftModifiedRoutedEventHandler ShiftModified
        {
            add { AddHandler(ShiftModifiedEvent, value); }
            remove { RemoveHandler(ShiftModifiedEvent, value); }
        }

        public bool IsShifted
        {
            get { return (bool)GetValue(IsShiftedProperty); }
            set { SetValue(IsShiftedProperty, value); }
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
            Content = IsShifted ? ShiftedContent : UnshiftedContent;
            base.OnTemplateChanged(oldTemplate, newTemplate);
        }

        private static void OnIsShiftedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var keyboardKey = (KeyboardKey)obj;

            if ((bool) e.NewValue)
            {
                keyboardKey.Content = keyboardKey.ShiftedContent;
                VisualStateManager.GoToState(keyboardKey, "Shifted", true);
            }
            else
            {
                keyboardKey.Content = keyboardKey.UnshiftedContent;
                VisualStateManager.GoToState(keyboardKey, "Unshifted", true);
            }
        }
    }
}
