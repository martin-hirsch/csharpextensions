/// <summary>
		/// Read a single character from the input buffer. Unlike Console.Read(), which 
		/// only reads from the buffer when the read operation has terminated (e.g. by
		/// pressing Enter), this method reads as soon as the character has been entered.
		/// </summary>
		/// <returns>The character read by the system</returns>
		public static char ReadChar()
		{
			// Temporarily disable character echo (ENABLE_ECHO_INPUT) and line input
			// (ENABLE_LINE_INPUT) during this operation
			SetConsoleMode(hConsoleInput, 
				(int) (InputModeFlags.ENABLE_PROCESSED_INPUT | 
				InputModeFlags.ENABLE_WINDOW_INPUT | 
				InputModeFlags.ENABLE_MOUSE_INPUT));

			int lpNumberOfCharsRead = 0;
			StringBuilder buf = new StringBuilder(1);

			bool success = ReadConsole(hConsoleInput, buf, 1, ref lpNumberOfCharsRead, 0);
			
			// Reenable character echo and line input
			SetConsoleMode(hConsoleInput, 
				(int) (InputModeFlags.ENABLE_PROCESSED_INPUT | 
				InputModeFlags.ENABLE_ECHO_INPUT |
				InputModeFlags.ENABLE_LINE_INPUT |
				InputModeFlags.ENABLE_WINDOW_INPUT | 
				InputModeFlags.ENABLE_MOUSE_INPUT));
			
			if (success)
				return Convert.ToChar(buf[0]);
		
			throw new ApplicationException("Attempt to call ReadConsole API failed.");
		}
