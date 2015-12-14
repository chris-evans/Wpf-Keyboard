using System.Windows;
using WindowsInput.Native;

namespace Chones.Keyboard
{
    public class ShiftKeyboardKey : KeyboardKey
    {
        public VirtualKeyCode VirtualKey
        { get { return VirtualKeyCode.SHIFT; } }

        static ShiftKeyboardKey()
        { DefaultStyleKeyProperty.OverrideMetadata(typeof(ShiftKeyboardKey), new FrameworkPropertyMetadata(typeof(ShiftKeyboardKey))); }
    }
}
