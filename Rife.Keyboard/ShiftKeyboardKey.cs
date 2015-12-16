using System.Windows;

namespace Rife.Keyboard
{
    public class ShiftKeyboardKey : KeyboardKey
    {
        public static readonly DependencyProperty IsShiftLockingProperty = DependencyProperty.RegisterAttached("IsShiftLocking", typeof(object), typeof(ShiftKeyboardKey),
                new PropertyMetadata(false));

        public bool IsShiftLocking
        {
            get { return (bool)GetValue(IsShiftLockingProperty); }
            set { SetValue(IsShiftLockingProperty, value); }
        }

        static ShiftKeyboardKey()
        { DefaultStyleKeyProperty.OverrideMetadata(typeof(ShiftKeyboardKey), new FrameworkPropertyMetadata(typeof(ShiftKeyboardKey))); }

        protected override void OnClick()
        {
            var eventArgs = new ModifierChangedRoutedEventArgs(KeyboardKey.ShiftModifierChangedProperty, this, !this.IsShifted, IsShiftLocking);
            RaiseEvent(eventArgs);
        }
    }
}