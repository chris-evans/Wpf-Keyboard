using System.Windows;
using WindowsInput;
using WindowsInput.Native;

namespace Rife.Keyboard
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

        protected override void OnClick()
        {
            var modifier = (VirtualKeyCode)(0);

            var sim = new InputSimulator();
            if (IsShifted) modifier = VirtualKeyCode.SHIFT;
            if (IsCapsLocked) modifier = VirtualKeyCode.CAPITAL;
            if (modifier != 0)
            { sim.Keyboard.ModifiedKeyStroke(modifier, VirtualKey); }
            else
            { sim.Keyboard.KeyPress(VirtualKey); }

            base.OnClick();
        }
    }
}
