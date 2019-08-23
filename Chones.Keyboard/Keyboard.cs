using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Rife.Keyboard
{
	[TemplatePart(Name = ContentPresenterPart, Type = typeof(ContentControl))]
	public class Keyboard : ContentControl
	{
		#region Dependency Properties
		public static readonly DependencyProperty IsShiftLockedProperty =
			DependencyProperty.RegisterAttached("IsShiftLocked", typeof(bool), typeof(Keyboard));

		public static readonly DependencyProperty IsShiftedProperty =
			DependencyProperty.RegisterAttached("IsShifted", typeof(bool), typeof(Keyboard), new PropertyMetadata(OnIsShiftedChanged));

		public static readonly DependencyProperty IsCapsLockedProperty =
			DependencyProperty.RegisterAttached("IsCapsLocked", typeof(bool), typeof(Keyboard), new PropertyMetadata(OnIsCapsLockedChanged));

		public static readonly DependencyProperty KeyboardStateProperty =
			DependencyProperty.RegisterAttached("KeyboardState", typeof(KeyboardState), typeof(Keyboard), new PropertyMetadata(KeyboardState.None, OnKeyboardStateChanged));

		public static readonly DependencyProperty AlphaNumericStyleProperty =
			DependencyProperty.RegisterAttached("AlphaNumericStyle", typeof(Style), typeof(Keyboard));

		public static readonly DependencyProperty NumericStyleProperty =
			DependencyProperty.RegisterAttached("NumericStyle", typeof(Style), typeof(Keyboard));
		#endregion

		public const string ContentPresenterPart = "PART_ContentPresenter";

		private ContentPresenter _contentControl;

		#region Properties
		public bool IsShifted
		{
			get { return (bool)GetValue(IsShiftedProperty); }
			set { SetValue(IsShiftedProperty, value); }
		}

		public bool IsShiftLocked
		{
			get { return (bool)GetValue(IsShiftLockedProperty); }
			set { SetValue(IsShiftLockedProperty, value); }
		}

		public bool IsCapsLocked
		{
			get { return (bool)GetValue(IsCapsLockedProperty); }
			set { SetValue(IsCapsLockedProperty, value); }
		}

		public KeyboardState KeyboardState
		{
			get { return (KeyboardState)GetValue(KeyboardStateProperty); }
			set { SetValue(KeyboardStateProperty, value); }
		}

		public Style AlphaNumericStyle
		{
			get { return (Style)GetValue(AlphaNumericStyleProperty); }
			set { SetValue(AlphaNumericStyleProperty, value); }
		}

		public Style NumericStyle
		{
			get { return (Style)GetValue(NumericStyleProperty); }
			set { SetValue(NumericStyleProperty, value); }
		}
		#endregion

		static Keyboard()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(typeof(Keyboard)));

			EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.ShiftModifierChangedProperty, new ModifierChangedRoutedEventHandler(OnShiftModified));
			EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.CapsModifierChangedProperty, new ModifierChangedRoutedEventHandler(OnCapsModified));
			EventManager.RegisterClassHandler(typeof(Keyboard), KeyboardKey.ClickEvent, new RoutedEventHandler(OnKeyClicked));
		}

		public Keyboard()
		{
			Focusable = false;
			IsTabStop = false;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				_contentControl = GetTemplateChild(ContentPresenterPart) as ContentPresenter;
				SetKeyboardStyle();
			}
		}

		private void SetKeyboardStyle()
		{
			if (_contentControl == null)
			{ return; }

			Style style = null;

			switch (KeyboardState)
			{
				case KeyboardState.AlphaNumeric:
					style = AlphaNumericStyle;
					break;
				case KeyboardState.Numeric:
					style = NumericStyle;
					break;
			}

			_contentControl.Style = style;
		}

		private static void OnShiftModified(object sender, ModifierChangedRoutedEventArgs e)
		{
			var keyboard = (Keyboard)sender;
			keyboard.IsShifted = e.Applied;
			keyboard.IsShiftLocked = e.Locked;
			e.Handled = true;
		}

		private static void OnCapsModified(object sender, ModifierChangedRoutedEventArgs e)
		{
			var keyboard = (Keyboard)sender;
			keyboard.IsCapsLocked = e.Applied;
			e.Handled = true;
		}

		private static void OnKeyClicked(object sender, RoutedEventArgs e)
		{
			// we will only react to changing shift behavior if the key pressed
			// was a keyboard key
			if (e.OriginalSource is KeyboardKey)
			{
				var keyboard = (Keyboard)sender;
				if (!keyboard.IsShiftLocked && keyboard.IsShifted)
				{ keyboard.IsShifted = false; }
			}
		}

		private static void OnIsShiftedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var isShifted = (bool)e.NewValue;
			var allKeys = FindVisualChildren<KeyboardKey>(obj);
			foreach (var key in allKeys)
			{ key.IsShifted = isShifted; }
		}

		private static void OnIsCapsLockedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var isCapsLocked = (bool)e.NewValue;
			var allKeys = FindVisualChildren<KeyboardKey>(obj);
			foreach (var key in allKeys)
			{ key.IsCapsLocked = isCapsLocked; }
		}

		private static void OnKeyboardStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var keyboard = obj as Keyboard;
			keyboard.SetKeyboardStyle();
		}

		private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					var child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{ yield return (T)child; }

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{ yield return childOfChild; }
				}
			}
		}
	}
}