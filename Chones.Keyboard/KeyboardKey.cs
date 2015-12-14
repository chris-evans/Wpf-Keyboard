using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chones.Keyboard
{
    [TemplateVisualState(Name = "Unmodified", GroupName = "ModifiersGroup")]
    [TemplateVisualState(Name = "Shifted", GroupName = "ModifiersGroup")]
    public class KeyboardKey : Control
    {
        public static readonly RoutedEvent KeyboardKeyPressedEvent =
            EventManager.RegisterRoutedEvent(nameof(KeyboardKeyPressed), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(KeyboardKey));

        public static readonly DependencyProperty ShiftedContentProperty =
            DependencyProperty.RegisterAttached(nameof(ShiftedContent), typeof(object), typeof(KeyboardKey));

        public static readonly DependencyProperty UnshiftedContentProperty =
            DependencyProperty.RegisterAttached(nameof(UnshiftedContent), typeof(object), typeof(KeyboardKey));

        public event RoutedEventHandler KeyboardKeyPressed
        {
            add { AddHandler(KeyboardKeyPressedEvent, value); }
            remove { RemoveHandler(KeyboardKeyPressedEvent, value); }
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
        }

        internal void SetShifted()
        { VisualStateManager.GoToState(this, "Shifted", true); }

        internal void UnsetShifted()
        { VisualStateManager.GoToState(this, "Unmodified", true); }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            RaiseEvent(new RoutedEventArgs(KeyboardKeyPressedEvent, this));
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnMouseLeftButtonUp(e);
            RaiseEvent(new RoutedEventArgs(KeyboardKeyPressedEvent, this));
        }
    }
}
