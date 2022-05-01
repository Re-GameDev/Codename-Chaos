/// @desc Handles buttons like Escape

if (keyboard_check_pressed(vk_escape))
{
	if (room == Options_r)
	{
		room_goto(MainMenu_r);
	}
}

