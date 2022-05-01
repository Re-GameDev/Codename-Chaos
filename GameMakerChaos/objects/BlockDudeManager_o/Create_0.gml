/// @desc Check for existing bricks, blocks, and players in the room

SetupBlockDudeGlobals();

for (var yPos = 0; yPos < global.bdGridSize.y; yPos++)
{
	for (var xPos = 0; xPos < global.bdGridSize.x; xPos++)
	{
		var gridPos = new Vec2(xPos, yPos);
		var screenPos = new Vec2(xPos * global.bdTileSize.x, yPos * global.bdTileSize.y);
		var gridSpace = GetBdGridSpace(gridPos);
		var existingInstance = instance_position(screenPos.x, screenPos.y, BlockDudeParent_o);
		if (existingInstance != noone)
		{
			gridSpace.SetInstance(existingInstance);
			existingInstance.gridPos = gridPos;
			existingInstance.gridSpace = gridSpace;
			with (existingInstance)
			{
				event_perform(ev_other, ev_user0); //Placed into grid event
			}
		}
	}
}

