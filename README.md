# WpfKeyboard

I have been trying to find a good on screen keyboard that I could use in a locked down kiosk based environment.  Many of the keyboards I did find were built to run out of process and also had their own keyboard designers built in.  Although this worked I always found myself working aroudn limitations this created.
  *  The keyboard ran out of process requiring IPC to hide/show the keyboard.  This was prone to breaking
  *  The keyboard came with it's own designer, but was limited in it's styling capabilities
  *  Internationalization support was generisized and didn't meat my requirements
  *  Keyboards were typically ties to virtual keys, causing default shifting behavior.  This made it difficult to design a keyboard              exactly as required
  *  Styling the keyboard to match desired design of staff designers was difficult.


The goals I am after with this keyboard are:
  *  Custom control that can be hosting directly in a custom WPF application
  *  Ability to use Visual Studio or Blend designer to easily make new keyboard layouts (with minimal developer intervention required)
  *  A small wrapper that can also allow for hosting the keyboard out of process (This is very secondary)


Example Usage (As of right now)
The API is changing a lot as this development effort is currently starting.  Example usage is below.  The below example would ideally seperate out the keyboard content in a seperate file, but the idea is below.  The below keyboard keys would take on default styling (which is a bit ugly at the moment).  This example would provide a numeric keypad keyboard that would allow for touch based input of 0-9.  This would easily extend to provide other keyboard keys.

<Grid>
    <kbrd:Keyboard Grid.Row="2" x:Name="_keyboard" Content="{Binding {StaticResource Numeric}}"></kbrd:Keyboard>
</Grid>

 <Grid x:Key="Numeric">
   <Grid.RowDefinitions>
       <RowDefinition Height="*" />
       <RowDefinition Height="*" />
       <RowDefinition Height="*" />
       <RowDefinition Height="*" />
   </Grid.RowDefinitions>
   <Grid.ColumnDefinitions>
       <ColumnDefinition Width="*" />
       <ColumnDefinition Width="*" />
       <ColumnDefinition Width="*" />
   </Grid.ColumnDefinitions>

   <kbrd:UnicodeKeyboardKey UnshiftedContent="7" UnshiftedText="7" Grid.Row="0" Grid.Column="0"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="8" UnshiftedText="8" Grid.Row="0" Grid.Column="1"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="9" UnshiftedText="9" Grid.Row="0" Grid.Column="2"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="4" UnshiftedText="7" Grid.Row="1" Grid.Column="0"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="5" UnshiftedText="8" Grid.Row="1" Grid.Column="1"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="6" UnshiftedText="9" Grid.Row="1" Grid.Column="2"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="1" UnshiftedText="7" Grid.Row="2" Grid.Column="0"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="2" UnshiftedText="8" Grid.Row="2" Grid.Column="1"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="3" UnshiftedText="9" Grid.Row="2" Grid.Column="2"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="0" UnshiftedText="7" Grid.Row="3" Grid.Column="0" Grid.ColumnSpan="2"/>
   <kbrd:UnicodeKeyboardKey UnshiftedContent="." UnshiftedText="." Grid.Row="3" Grid.Column="2"/>
 </Grid>

There is also a ShiftKeyboardKey that can be added to a keyboard that will "shift" context switch all keyboard keys when it is toggled on and off.  Every keyboard key has both Shifted and Unshifted content that can be set.  This content will automatically show and hide depending on the context of the shift key.  Below is a couple xaml lines showing what those keys look like

<StackPanel x:Key="OneCharacterKeyboardWithShift" Orientation="Horizontal">
    <kbrd:UnicodeKeyboardKey UnshiftedContent="1" ShiftedContent="!" UnshiftedText="1" ShiftedUnicodeText="!" />
    <kbrd:ShiftKeyboardKey UnshiftedContent="SHIFT" ShiftedContent="SHIFTED" />
</StackPanel>

There are currently three types of keys that can be used.
ShiftKeyboardKey - provides shift context switching of the entire keyboard
UnicodeKeyboardKey - provides the ability to emulate unicode input (in addition, strings of text can be input like ".com")
VirtualKeyKeyboardKey - provides virtual key input.  When shift context is in effect, the virtual key will behave like shift context for the current local on the windows machine.
