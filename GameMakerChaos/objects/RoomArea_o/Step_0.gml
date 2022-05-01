/// @desc Handle first step

if (self.firstUpdate)
{
	self.gridPos = new Vec2(floor(x / global.bdTileSize.x), floor(y / global.bdTileSize.y));
	var gridBottomRight = new Vec2(ceil((x + sprite_width) / global.bdTileSize.x), ceil((y + sprite_height) / global.bdTileSize.y));
	self.gridSize = new Vec2(gridBottomRight.x - self.gridPos.x, gridBottomRight.y - self.gridPos.y);
	
	self.entranceDoor = FindInstanceInGridArea(EntranceDoor_o, self.gridPos, self.gridSize);
	if (self.entranceDoor != noone)
	{
		self.entranceDoor.roomNumber = self.roomNumber;
	}
	else
	{
		show_debug_message("Failed to find entrance door in room " + string(self.roomNumber));
	}
	
	self.exitDoor = FindInstanceInGridArea(ExitDoor_o, self.gridPos, self.gridSize);
	if (self.exitDoor != noone)
	{
		//show_debug_message("Setting exit door to " + string(self.roomNumber) + " -> " + string(self.roomNumber + 1));
		self.exitDoor.roomNumber = self.roomNumber;
		self.exitDoor.targetRoomNumber = self.roomNumber + 1;
	}
	else
	{
		show_debug_message("Failed to find exit door in room " + string(self.roomNumber));
	}
	
	show_debug_message("Serializing reset state for room " + string(self.roomNumber) + " at (" + string(self.gridPos.x) + ", " + string(self.gridPos.y) + ", " + string(self.gridSize.x) + ", " + string(self.gridSize.y) + ")...");
	self.resetState = SerializeBdGridArea(self.gridPos, self.gridSize);
	show_debug_message("Serialization:\n" + self.resetState);
	
	self.firstUpdate = false;
}

