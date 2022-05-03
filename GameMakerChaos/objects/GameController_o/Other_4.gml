/// @desc Manage Music

show_debug_message("Now in room: " + room_get_name(room));

if (room == MainMenu_r || room == Options_r || room == GamePick_r)
{
	PlayMusic(FloatingCities_m);
}
else
{
	StopMusic();
}

