/// @desc Handle input and auto destroy

if (self.data != 0 && !global.enteringCheatCode)
{
	if (keyboard_check_pressed(vk_enter) || keyboard_check_pressed(vk_space))
	{
		if (self.hasSeenInteractBtnReleased)
		{
			self.data.Close();
		}
	}
	if (!keyboard_check(vk_enter) && !keyboard_check(vk_enter))
	{
		self.hasSeenInteractBtnReleased = true;
	}
	
	if (self.data.wasClosed && self.data.GetOpenAmount() <= 0)
	{
		if (global.currentDialogue == self.id)
		{
			show_debug_message("Clearing currentDialogue global");
			global.currentDialogue = noone;
			global.showingDialogue = false;
		}
		instance_destroy();
	}
}
