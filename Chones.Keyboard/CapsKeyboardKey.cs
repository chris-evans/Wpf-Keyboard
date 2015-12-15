using System.Windows;

namespace Chones.Keyboard
{
    public class CapsKeyboardKey : VirtualKeyKeyboardKey
    {
        static CapsKeyboardKey()
        { DefaultStyleKeyProperty.OverrideMetadata(typeof(CapsKeyboardKey), new FrameworkPropertyMetadata(typeof(CapsKeyboardKey))); }

        protected override void OnClick()
        {
            var eventArgs = new ModifierChangedRoutedEventArgs(KeyboardKey.CapsModifierChangedProperty, this, !this.IsCapsLocked, true);
            RaiseEvent(eventArgs);
        }
    }
}
