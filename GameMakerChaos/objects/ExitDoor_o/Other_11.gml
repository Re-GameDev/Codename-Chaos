/// @desc Player reached exit

if (self.isWinningDoor)
{
	show_message("You win!");
	room_goto(MainMenu_r);
}
else
{
	var targetRoomEntrance = FindRoomEntranceForRoomNumber(self.targetRoomNumber);
	if (targetRoomEntrance != noone)
	{
		var dude = instance_find(BlockDude_o, 0);
		if (dude != noone)
		{
			show_debug_message("Going to room " + string(self.targetRoomNumber) + " at (" + string(targetRoomEntrance.gridPos.x) + ", " + string(targetRoomEntrance.gridPos.y) + ")");
			dude.gridSpace.MoveInstanceTo(GetBdGridSpace(targetRoomEntrance.gridPos));
			ClearAllPortals();
		}
		else
		{
			show_debug_message("Couldn't find the dude!");
		}
	}
	else
	{
		show_debug_message("Couldn't find entrance door for room " + string(self.targetRoomNumber));
	}
}

