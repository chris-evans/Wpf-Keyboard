using System.Windows;

namespace Chones.Keyboard
{
    public class ShiftKeyboardKey : KeyboardKey
    {
        static ShiftKeyboardKey()
        { DefaultStyleKeyProperty.OverrideMetadata(typeof(ShiftKeyboardKey), new FrameworkPropertyMetadata(typeof(ShiftKeyboardKey))); }

        protected override void OnClick()
        {
            if (IsShifted)
            { RaiseEvent(new RoutedEventArgs(KeyboardKey.ShiftDeactivatedEvent, this)); }
            else
            { RaiseEvent(new RoutedEventArgs(KeyboardKey.ShiftActivatedEvent, this)); }
            
            base.OnClick();
        }
    }
}
