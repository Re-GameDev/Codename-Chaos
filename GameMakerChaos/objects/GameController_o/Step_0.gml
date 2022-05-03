/// @desc Handles buttons like Escape

if (keyboard_check_pressed(vk_escape) && !global.enteringCheatCode)
{
	var roomName = string_lower(room_get_name(room));
	var isRpgMap = FindSubstring(roomName, "rpg");
	isRpgMap = (isRpgMap >= 0);
	
	//TODO: Remove BlockDude_r once it has a start menu
	//TODO: Remove isRpgMap once that game has a start menu
	if (room == Options_r || room == GamePick_r || room == BlockDude_r || isRpgMap)
	{
		//Destroy persistent objects from various game modes
		instance_destroy(RpgController_o);
		
		room_goto(MainMenu_r);
	}
}

