/// @desc Handles buttons like Escape

if (keyboard_check_pressed(vk_escape) && !global.enteringCheatCode)
{
	//TODO: Remove BlockDude_r once it has a start menu
	if (room == Options_r || room == GamePick_r || room == BlockDude_r)
	{
		room_goto(MainMenu_r);
	}
}

