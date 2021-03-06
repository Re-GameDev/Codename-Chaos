function InitGlobalVars()
{
	global.TargetFramerate = 60;
	global.TargetFrametime = (1000 / global.TargetFramerate);
	global.ProgramTime = 0;
	global.ElapsedMs = 0;
	global.TimeScale = 0;
	global.mouseClickHandled = false;
	global.cursorSet = false;
	global.masterVolume = 0.8;
	global.musicVolume = 1.0;
	global.soundEffectVolume = 1.0;
	global.currentMusic = noone;
	global.currentMusicId = noone;
	global.enteringCheatCode = false;
	global.debugModeEnabled = false;
	
	InitButtonPromptGlobals();
}

function HandleEnteredCheat(cheatStr)
{
	var isRpgRoom = (FindSubstring(string_lower(room_get_name(room)), "rpg") >= 0);
	
	if (cheatStr == "DEBUG")
	{
		//TODO: Display this message the player somehow
		show_debug_message((global.debugModeEnabled ? "Disabling" : "Enabling") + " debug mode!");
		global.debugModeEnabled = !global.debugModeEnabled;
	}
	else if (cheatStr == "PORTAL")
	{
		show_debug_message((global.hasPortalGun ? "Taking away" : "Giving") + " portal gun!");
		global.hasPortalGun = !global.hasPortalGun;
	}
	else if (room == BlockDude_r && string_length(cheatStr) == 3)
	{
		var allRoomAreas = FindAllInstancesOf(RoomArea_o);
		for (var rIndex = 0; rIndex < array_length(allRoomAreas); rIndex++)
		{
			if (allRoomAreas[rIndex].cheatCode == cheatStr && allRoomAreas[rIndex].entranceDoor != noone)
			{
				var dude = instance_find(BlockDude_o, 0);
				if (dude != noone)
				{
					show_debug_message("Going to room " + string(allRoomAreas[rIndex].roomNumber) + " at (" + string(allRoomAreas[rIndex].entranceDoor.gridPos.x) + ", " + string(allRoomAreas[rIndex].entranceDoor.gridPos.y) + ")");
					dude.gridSpace.MoveInstanceTo(GetBdGridSpace(allRoomAreas[rIndex].entranceDoor.gridPos));
					ClearAllPortals();
				}
				else
				{
					show_debug_message("Couldn't find the dude!");
				}
			}
		}
	}
	else
	{
		var isUnknown = true;
		var rpgController = instance_find(RpgController_o, 0);
		if (rpgController != noone && RpgHandleEnteredCheat(cheatStr))
		{
			isUnknown = false;
		}
		if (isUnknown) { show_debug_message("Unkown cheat"); }
	}
}

