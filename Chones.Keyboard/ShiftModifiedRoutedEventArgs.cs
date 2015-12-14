using System.Windows;

namespace Chones.Keyboard
{
    public delegate void ShiftModifiedRoutedEventHandler(object sender, KeyboardShiftStateModifiedRoutedEventArgs e);

    /// <summary>
    /// Routed Event for a shift state change
    /// </summary>
    public class KeyboardShiftStateModifiedRoutedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Get if shifted
        /// </summary>
        public bool Shifted
        { get; private set; }

        /// <summary>
        /// Get if they shift mode is locked or not
        /// </summary>
        public bool Locked
        { get; private set; }

        /// <summary>
        /// Create a new KeyboardShiftStateModifiedRoutedEvent
        /// </summary>
        /// <param name="routedEvent">The routed event</param>
        /// <param name="source">The source of the routed event</param>
        /// <param name="shifted">get if the we are shifted or not</param>
        /// <param name="locked">if shifted, is the shift a shift lock</param>
        public KeyboardShiftStateModifiedRoutedEventArgs(RoutedEvent routedEvent, object source, bool shifted, bool locked)
            : base(routedEvent, source)
        {
            Shifted = shifted;
            Locked = locked;
        }
    }
}
