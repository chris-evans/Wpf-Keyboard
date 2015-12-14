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
