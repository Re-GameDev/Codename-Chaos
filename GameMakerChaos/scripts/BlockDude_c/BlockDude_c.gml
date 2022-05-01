
function BdGridSpace(_instance, _gridPos) constructor
{
	instance = _instance;
	gridPos = _gridPos;
	objectId = noone;
	if (instance != noone)
	{
		objectId = instance.object_index;
	}
	
	static Copy = function()
	{
		return new BdGridSpace(self.instance, self.gridPos);
	}
	
	static IsSolid = function()
	{
		return (instance != noone && instance.solid);
	}
	static IsDude = function()
	{
		return (instance != noone && objectId == BlockDude_o);
	}
	static IsBlock = function()
	{
		return (instance != noone && objectId == Block_o);
	}
	static IsBrick = function()
	{
		return (instance != noone && objectId == Brick_o);
	}
	static Clear = function()
	{
		if (instance != noone)
		{
			instance_destroy(instance);
			instance = noone;
			objectId = noone;
		}
	}
	static PopOut = function()
	{
		var result = self.instance;
		if (result != noone)
		{
			result.gridSpace = 0;
		}
		self.instance = noone;
		self.objectId = noone;
		return result;
	}
	static SetInstance = function(_instance)
	{
		self.Clear();
		instance = _instance;
		if (instance != noone)
		{
			objectId = instance.object_index;
			instance.gridPos = self.gridPos.Copy();
			instance.gridSpace = self;
			instance.x = self.gridPos.x * global.bdTileSize.x;
			instance.y = self.gridPos.y * global.bdTileSize.y;
		}
	}
	static MoveInstanceTo = function(otherGridSpace)
	{
		otherGridSpace.Clear();
		otherGridSpace.SetInstance(self.instance);
		self.instance = noone;
		self.objectId = noone;
	}
}

function SetupBlockDudeGlobals()
{
	global.bdGrid = [];
	global.bdTileSize = new Vec2(sprite_get_width(Brick_s), sprite_get_height(Brick_s));
	global.bdGridSize = new Vec2(floor(room_width / global.bdTileSize.x), floor(room_height / global.bdTileSize.y));
	for (var yPos = 0; yPos < global.bdGridSize.y; yPos++)
	{
		for (var xPos = 0; xPos < global.bdGridSize.x; xPos++)
		{
			array_push(global.bdGrid, new BdGridSpace(noone, new Vec2(xPos, yPos)));
		}
	}
}

function GetBdGridSpace(gridPos)
{
	if (gridPos.x < 0) { return 0; }
	if (gridPos.y < 0) { return 0; }
	if (gridPos.x >= global.bdGridSize.x) { return 0; }
	if (gridPos.y >= global.bdGridSize.y) { return 0; }
	return global.bdGrid[(gridPos.y * global.bdGridSize.x) + gridPos.x];
}

function FindRoomAreaByNumber(roomNumber)
{
	var allRoomAreas = FindAllInstancesOf(RoomArea_o);
	for (var rIndex = 0; rIndex < array_length(allRoomAreas); rIndex++)
	{
		if (allRoomAreas[rIndex].roomNumber == roomNumber)
		{
			return allRoomAreas[rIndex];
		}
	}
	return noone;
}
function FindRoomAreaAtGridPos(gridPos)
{
	var allRoomAreas = FindAllInstancesOf(RoomArea_o);
	for (var rIndex = 0; rIndex < array_length(allRoomAreas); rIndex++)
	{
		if (gridPos.x >= allRoomAreas[rIndex].gridPos.x &&
			gridPos.y >= allRoomAreas[rIndex].gridPos.y &&
			gridPos.x < allRoomAreas[rIndex].gridPos.x + allRoomAreas[rIndex].gridSize.x &&
			gridPos.y < allRoomAreas[rIndex].gridPos.y + allRoomAreas[rIndex].gridSize.y)
		{
			return allRoomAreas[rIndex];
		}
	}
	return noone;
}

function FindInstanceInGridArea(objectId, gridAreaTopLeft, gridAreaSize)
{
	var allObjects = FindAllInstancesOf(objectId);
	for (var oIndex = 0; oIndex < array_length(allObjects); oIndex++)
	{
		if (allObjects[oIndex].gridPos.x >= gridAreaTopLeft.x &&
			allObjects[oIndex].gridPos.y >= gridAreaTopLeft.y &&
			allObjects[oIndex].gridPos.x < gridAreaTopLeft.x + gridAreaSize.x &&
			allObjects[oIndex].gridPos.y < gridAreaTopLeft.y + gridAreaSize.y)
		{
			return allObjects[oIndex];
		}
	}
	return noone;
}

function FindRoomExitForRoomNumber(roomNumber)
{
	var roomArea = FindRoomAreaByNumber(roomNumber);
	if (roomArea != noone && roomArea.exitDoor != noone)
	{
		return roomArea.exitDoor;
	}
	return noone;
}
function FindRoomEntranceForRoomNumber(roomNumber)
{
	var roomArea = FindRoomAreaByNumber(roomNumber);
	if (roomArea != noone && roomArea.entranceDoor != noone)
	{
		return roomArea.entranceDoor;
	}
	return noone;
}

//NOTE: We only serialize the block positions and player position, w/ heldBox flag (not bricks or entrance/exit doors)
//      These are the only elements that change and need to be reset or undone
function SerializeBdGridArea(gridAreaTopLeft, gridAreaSize)
{
	var result = "";
	
	var entranceDoor = FindInstanceInGridArea(EntranceDoor_o, gridAreaTopLeft, gridAreaSize);
	for (var yOffset = 0; yOffset < gridAreaSize.y; yOffset++)
	{
		for (var xOffset = 0; xOffset < gridAreaSize.x; xOffset++)
		{
			var gridPos = new Vec2(gridAreaTopLeft.x + xOffset, gridAreaTopLeft.y + yOffset);
			var gridSpace = GetBdGridSpace(gridPos);
			var gridSpaceWorldPos = new Vec2(gridPos.x * global.bdTileSize.x, gridPos.y * global.bdTileSize.y);
			if (gridSpace != 0)
			{
				if (gridSpace.IsBlock())
				{
					show_debug_message("Found block at (" + string(gridPos.x) + ", " + string(gridPos.y) + ")");
					result += "@";
				}
				else if (gridSpace.IsDude())
				{
					if (gridSpace.instance.heldBlock != noone)
					{
						result += "D";
					}
					else
					{
						result += "d";
					}
				}
				else if (gridSpace.IsBrick())
				{
					result += "#";
				}
				else
				{
					if (entranceDoor != noone && entranceDoor.gridPos.x == gridPos.x && entranceDoor.gridPos.y == gridPos.y)
					{
						result += "e";
					}
					else
					{					
						result += ".";
					}
				}
			}
			else
			{
				result += "?";
			}
		}
		result += "\n";
	}
	
	return result;
}

function ApplySerializationToBdGridArea(serialization, gridAreaTopLeft, gridAreaSize)
{
	//Clear the blocks on the grid and find the dude
	var dude = noone;
	for (var yOffset = 0; yOffset < gridAreaSize.y; yOffset++)
	{
		for (var xOffset = 0; xOffset < gridAreaSize.x; xOffset++)
		{
			var gridPos = new Vec2(gridAreaTopLeft.x + xOffset, gridAreaTopLeft.y + yOffset);
			var gridSpace = GetBdGridSpace(gridPos);
			if (gridSpace.IsDude())
			{
				dude = gridSpace.PopOut();
			}
			else if (gridSpace.IsBlock())
			{
				gridSpace.Clear();
			}
		}
	}
	
	var serializationContainsDude = (FindSubstring(serialization, "d") >= 0 || FindSubstring(serialization, "D") >= 0);
	
	for (var yOffset = 0; yOffset < gridAreaSize.y; yOffset++)
	{
		for (var xOffset = 0; xOffset < gridAreaSize.x; xOffset++)
		{
			var gridPos = new Vec2(gridAreaTopLeft.x + xOffset, gridAreaTopLeft.y + yOffset);
			var gridSpace = GetBdGridSpace(gridPos);
			var gridSpaceWorldPos = new Vec2(gridPos.x * global.bdTileSize.x, gridPos.y * global.bdTileSize.y);
			var serializedChar = StringCharAt(serialization, (yOffset * (gridAreaSize.x+1)) + xOffset);
			if (serializedChar == "d" || serializedChar == "D")
			{
				if (dude == noone)
				{
					dude = instance_create_layer(gridSpaceWorldPos.x, gridSpaceWorldPos.y, "PlayerAndDoors", BlockDude_o);
				}
				gridSpace.SetInstance(dude);
				with (dude) { event_perform(ev_other, ev_user0); }
				if (dude.heldBlock == noone && serializedChar == "D")
				{
					dude.heldBlock = instance_create_layer(gridSpaceWorldPos.x, gridSpaceWorldPos.y, "BricksAndBlocks", Block_o);
				}
				else if (dude.heldBlock != noone && serializedChar == "d")
				{
					instance_destroy(dude.heldBlock);
					dude.heldBlock = noone;
				}
			}
			else if (serializedChar == "@")
			{
				show_debug_message("Putting block at (" + string(gridPos.x) + ", " + string(gridPos.y) + ")");
				var newBlock = instance_create_layer(gridSpaceWorldPos.x, gridSpaceWorldPos.y, "BricksAndBlocks", Block_o);
				gridSpace.SetInstance(newBlock);
				with (newBlock) { event_perform(ev_other, ev_user0); }
			}
			else if (serializedChar == "e" && !serializationContainsDude)
			{
				if (dude == noone)
				{
					dude = instance_create_layer(gridSpaceWorldPos.x, gridSpaceWorldPos.y, "PlayerAndDoors", BlockDude_o);
				}
				if (dude.heldBlock != noone)
				{
					instance_destroy(dude.heldBlock);
					dude.heldBlock = noone;
				}
				dude.isFacingLeft = false;
				gridSpace.SetInstance(dude);
				with (dude) { event_perform(ev_other, ev_user0); }
			}
		}
	}
}

