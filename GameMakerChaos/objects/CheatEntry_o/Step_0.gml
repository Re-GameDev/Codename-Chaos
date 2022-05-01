/// @desc Handle cheat entry

var justOpened = false;
if (!self.showingEntryBox)
{
	global.enteringCheatCode = false;
}

if (!self.showingEntryBox && keyboard_check_pressed(vk_insert))
{
	self.showingEntryBox = true;
	global.enteringCheatCode = true;
	justOpened = true;
	self.enteredStr = "";
}

if (self.showingEntryBox)
{
	if (keyboard_check_pressed(vk_enter) || keyboard_check_pressed(vk_escape) || (keyboard_check_pressed(vk_insert) && !justOpened))
	{
		HandleEnteredCheat(self.enteredStr);
		self.showingEntryBox = false;
		// Wait till next frame to set the global back to false so escape and
		// other keys don't get handled by things the frame the input box goes away
	}
	if (keyboard_check_pressed(vk_backspace))
	{
		if (string_length(self.enteredStr) > 0)
		{
			self.enteredStr = StringPart(self.enteredStr, 0, string_length(self.enteredStr) - 1);
		}
	}
	
	for (var cIndex = 0; cIndex < 26; cIndex++)
	{
		var char = chr(ord("A") + cIndex);
		if (keyboard_check_pressed(ord(char)))
		{
			self.enteredStr += char;
		}
	}
}


