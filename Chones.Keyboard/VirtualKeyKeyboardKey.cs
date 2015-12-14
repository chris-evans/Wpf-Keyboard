using System.Windows;
using WindowsInput.Native;

namespace Chones.Keyboard
{
    public class VirtualKeyKeyboardKey : KeyboardKey
    {
        public static readonly DependencyProperty VirtualKeyProperty =
          DependencyProperty.RegisterAttached(nameof(VirtualKey), typeof(VirtualKeyCode), typeof(VirtualKeyKeyboardKey));

        public VirtualKeyCode VirtualKey
        {
            get { return (VirtualKeyCode)GetValue(VirtualKeyProperty); }
            set { SetValue(VirtualKeyProperty, value); }
        }

        static VirtualKeyKeyboardKey()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VirtualKeyKeyboardKey), new FrameworkPropertyMetadata(typeof(VirtualKeyKeyboardKey)));
        }
    }
}
